using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using HamstarHelpers.Helpers.Tiles;
using HamstarHelpers.Helpers.TModLoader;
using System;
using Terraria;
using Terraria.Utilities;


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
			if( currentChestType != matchType ) {
				if( matchType == "Vanilla Underground World Chest" ) {
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
					}
				} else {
					return false;
				}
			}

			return true;
		}


		////////////////

		public static void ApplyConfiguredImplantsToChest( Chest chest ) {
			var mymod = ChestImplantsMod.Instance;
			var config = ChestImplantsConfig.Instance;

			Tile mytile = Main.tile[ chest.x, chest.y ];
			string currentChestType;
			if( !TileFrameHelpers.VanillaChestTypeNamesByFrame.TryGetValue(mytile.frameX / 36, out currentChestType) ) {
				throw new ModHelpersException( "Could not find chest frame" );
			}
//LogHelpers.Log("chest "+i+" pos:"+mychest.x+","+mychest.y+", frame:"+(mytile.frameX/36)+", wall:"+mytile.wall+" "+(mychest.item[0]!=null?mychest.item[0].Name:"..."));
			
			foreach( (string defSet, ChestImplanterSetDefinition setDef) in config.RandomPickFromSetChestImplanterDefinitions ) {
				ChestImplanterDefinition implantDef = ChestImplanter.GetRandomImplanterFromSet( setDef );
				
				if( ChestImplantsConfig.Instance.DebugModeInfo ) {
					LogHelpers.Log( "ApplyConfiguredImplantsToChest RAND "
						+ chest.GetHashCode() + currentChestType + " " + defSet
						+ " - Set total: " + setDef.Count + " - Items of set's pick: " + implantDef?.ItemDefinitions.Count
					);
				}
				if( implantDef != null ) {
					ChestImplanter.ApplyImplantToChest( chest, implantDef, currentChestType );
					break;
				}
			}
			foreach( (string defSet, ChestImplanterSetDefinition setDef) in config.AllFromSetChestImplanterDefinitions ) {
				if( ChestImplantsConfig.Instance.DebugModeInfo ) {
					LogHelpers.Log( "ApplyConfiguredImplantsToChest ALL "
						+ chest.GetHashCode() + currentChestType + " " + defSet
						+ " - Set total: " + setDef.Count
					);
				}
				foreach( ChestImplanterDefinition implantDef in setDef ) {
					ChestImplanter.ApplyImplantToChest( chest, implantDef, currentChestType );
				}
			}
			
			foreach( CustomChestImplanter customStuffer in mymod.CustomImplanter ) {
				if( ChestImplantsConfig.Instance.DebugModeInfo ) {
					LogHelpers.Log( "ApplyConfiguredImplantsToChest CUSTOM "
						+ chest.GetHashCode() + currentChestType + " " + customStuffer );
				}
				customStuffer( currentChestType, chest );
			}
		}

		private static void ApplyImplantToChest( Chest chest, ChestImplanterDefinition implantDef, string currentChestType ) {
			Tile mytile = Main.tile[chest.x, chest.y];

			bool isMatched = false;
			foreach( string checkChestType in implantDef.ChestTypes ) {
				if( ChestImplanter.IsChestMatch( currentChestType, checkChestType ) ) {
					isMatched = true;
					break;
				}
			}

			if( !isMatched ) {
				return;
			}

			foreach( ChestImplanterItemDefinition itemDef in implantDef.ItemDefinitions ) {
				bool canImplant = ChestImplanter.CanChestAcceptImplantItem( mytile, itemDef );
				if( ChestImplantsConfig.Instance.DebugModeInfo ) {
					LogHelpers.Log( " ApplyImplantToChest "
						+ chest.GetHashCode() + currentChestType + " " + itemDef.ToString()
						+ " - " + canImplant
					);
				}
				if( canImplant ) {
					ChestImplanter.Implant( chest, itemDef );
				}
			}
		}

		////

		private static ChestImplanterDefinition GetRandomImplanterFromSet( ChestImplanterSetDefinition setDef ) {
			if( setDef.Count == 0 ) {
				return null;
			}

			UnifiedRandom rand = TmlHelpers.SafelyGetRand();
			float totalWeight = setDef.TotalWeight();
			float randPick = rand.NextFloat() * totalWeight;
			float countedWeights = 0;

			for( int i = 0; i < setDef.Count; i++ ) {
				countedWeights += setDef[i].Weight;

				if( countedWeights > randPick ) {
					return setDef[ i ];
				}
			}

			LogHelpers.Warn( "Could not randomly pick from implanter set. Total weight "+countedWeights+" of "+setDef.Count+" implanters." );
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
			
			if( addedAmount > 0 ) {
				ChestImplanter.PrependItemToChest( chest, itemType, addedAmount, info );
			} else {
				ChestImplanter.ExtractItemFromChest( chest, itemType, -addedAmount );
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
