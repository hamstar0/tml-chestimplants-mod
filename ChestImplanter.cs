using System;
using System.Linq;
using Terraria;
using Terraria.Utilities;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using HamstarHelpers.Helpers.Tiles;
using HamstarHelpers.Helpers.TModLoader;


namespace ChestImplants {
	public partial class ChestImplanter {
		public static bool IsChestEmpty( Chest chest ) {
			for( int i = 0; i < chest.item.Length; i++ ) {
				if( chest.item[i] == null || chest.item[i].IsAir ) { continue; }
				return false;
			}
			return true;
		}

		
		public static bool IsChestMatch( string currentChestType, string matchType ) {
			if( currentChestType.Equals(matchType) ) {
				return true;
			}

			if( matchType.Equals("Vanilla Underground World Chest") ) {
				switch( currentChestType ) {
				case "Chest":
				//case "Locked Gold Chest":
				case "Locked Shadow Chest":
				case "Lihzahrd Chest":
				case "Locked Jungle Chest":
				case "Locked Corruption Chest":
				case "Locked Crimson Chest":
				case "Locked Hallowed Chest":
				case "Locked Frozen Chest":
				case "Locked Green Dungeon Chest":
				case "Locked Pink Dungeon Chest":
				case "Locked Blue Dungeon Chest":
					return false;
				default:
					return true;
				}
			}

			return false;
		}


		////////////////

		public static void ApplyAllImplantsToChest( Chest chest ) {
			var mymod = ChestImplantsMod.Instance;
			var config = ChestImplantsConfig.Instance;

			// Get chest tile and name
			Tile chestTile = Main.tile[ chest.x, chest.y ];
			string chestName;
			if( !TileFrameHelpers.VanillaChestTypeNamesByFrame.TryGetValue(chestTile.frameX / 36, out chestName) ) {
				throw new ModHelpersException( "Could not find chest frame" );
			}
//LogHelpers.Log("chest "+i+" pos:"+mychest.x+","+mychest.y+", frame:"+(mytile.frameX/36)+", wall:"+mytile.wall+" "+(mychest.item[0]!=null?mychest.item[0].Name:"..."));
			
			// Apply random implants
			foreach( ChestImplanterSetDefinition setDef in config.GetRandomImplanterSets() ) {
				ChestImplanter.ApplyRandomImplantsSetToChest( chest, chestName, setDef );
			}

			// Apply guaranteed implants
			foreach( Ref<ChestImplanterDefinition> implantDef in config.AllFromSetChestImplanterDefinitions.Value ) {
				ChestImplanter.ApplyImplantToChest( chest, implantDef.Value, chestName );
			}
			
			foreach( (string name, CustomChestImplanter customStuffer) in mymod.CustomImplanter ) {
				if( ChestImplantsConfig.Instance.DebugModeInfo ) {
					LogHelpers.Log(
						"ApplyAllImplantsToChest CUSTOM "
						+"'"+chest.GetHashCode()+" "+chestName+"'"
						+"- "+name+" at "+chest.x+","+chest.y
					);
				}

				customStuffer.Invoke( chestName, chest );
			}
		}

		////

		private static void ApplyRandomImplantsSetToChest(
					Chest chest,
					string currentChestType,
					ChestImplanterSetDefinition setDef ) {
			ChestImplanterDefinition implantDef = ChestImplanter.GetRandomImplanterFromSet( setDef );
			if( implantDef == null ) {
				return;
			}

			if( ChestImplantsConfig.Instance.DebugModeInfo ) {
				LogHelpers.Log(
					"ApplyConfiguredImplantsToChest RAND "
					+ "'"+chest.GetHashCode()+" "+currentChestType+"' at "+chest.x+","+chest.y
					//+ " - Set total: " + setDef.Value.Count + " - Items of set's pick: " + implantDef?.ItemDefinitions.Count
					+ " - " + string.Join( ", ", implantDef?.ItemDefinitions.Select(
						itemDef => itemDef.ChestItem.ToString()+" ("+(int)(itemDef.ChancePerChest * 100f)+"%)"
					) )
				);
			}

			ChestImplanter.ApplyImplantToChest( chest, implantDef, currentChestType );
		}

		private static void ApplyImplantToChest( Chest chest, ChestImplanterDefinition implantDef, string currentChestType ) {
			Tile chestTile = Main.tile[chest.x, chest.y];

			// Check if chest type is recognized by the given implanter
			bool isMatched = false;
			foreach( Ref<string> checkChestType in implantDef.ChestTypes ) {
				if( ChestImplanter.IsChestMatch( currentChestType, checkChestType.Value ) ) {
					isMatched = true;
					break;
				}
			}

			// Guess not?
			if( !isMatched ) {
				return;
			}

			// Implant each item (item-implanter willing)
			foreach( ChestImplanterItemDefinition itemImplantDef in implantDef.ItemDefinitions ) {
				bool canImplant = ChestImplanter.CanChestAcceptImplantItem( chestTile, itemImplantDef );

				if( ChestImplantsConfig.Instance.DebugModeVerboseInfo ) {
					LogHelpers.Log( " ApplyImplantToChest "
						+ chest.GetHashCode() + currentChestType
						+ " - " + itemImplantDef.ToCustomString()
						+ " - " + canImplant
						+ " - " + ChestImplanter.GetImplantQuantity( itemImplantDef )
					);
				}

				if( canImplant ) {
					ChestImplanter.Implant( chest, itemImplantDef );
				}
			}
		}

		////

		private static ChestImplanterDefinition GetRandomImplanterFromSet( ChestImplanterSetDefinition setDef ) {
			if( setDef.Value.Count == 0 ) {
				return null;
			}

			UnifiedRandom rand = TmlHelpers.SafelyGetRand();
			float totalWeight = setDef.TotalWeight();
			float randPick = rand.NextFloat() * totalWeight;
			float countedWeights = 0;

			for( int i = 0; i < setDef.Value.Count; i++ ) {
				countedWeights += setDef.Value[i].Value.Weight;

				if( countedWeights > randPick ) {
					return setDef.Value[ i ].Value;
				}
			}

			LogHelpers.Warn( "Could not randomly pick from implanter set. Total weight "+countedWeights+" of "+setDef.Value.Count+" implanters." );
			return null;
		}


		////////////////

		public static bool CanChestAcceptImplantItem( Tile chestTile, ChestImplanterItemDefinition info ) {
			if( info.WallId != -1 && info.WallId != chestTile.wall ) {
				return false;
			}
			return Main.rand.NextFloat() < info.ChancePerChest;
		}


		////////////////

		public static void Implant( Chest chest, ChestImplanterItemDefinition info ) {
			int addedAmount = ChestImplanter.GetImplantQuantity( info );
			if( addedAmount == 0 ) {
				return;
			}

			int itemType = info.ChestItem.Type;
			if( itemType == 0 ) {
				LogHelpers.Alert( "Invalid item key " + info.ChestItem );
				return;
			}
			
			// Add or remove quantity of item, according to implanter spec
			if( addedAmount > 0 ) {
				ChestImplanter.PrependItemToChest( chest, itemType, addedAmount, info );
			} else if( addedAmount < 0 ) {
				ChestImplanter.ExtractItemFromChest( chest, itemType, -addedAmount );
			} else {
				throw new ModHelpersException( "Invalid quantity." );
			}
		}


		private static int GetImplantQuantity( ChestImplanterItemDefinition info ) {
			int range = info.MaxQuantity - info.MinQuantity;
			if( range == 0 ) {
				return info.MinQuantity;
			}

			UnifiedRandom rand = TmlHelpers.SafelyGetRand();
			return info.MinQuantity + rand.Next( range );
		}
	}
}
