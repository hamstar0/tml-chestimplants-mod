using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using HamstarHelpers.Helpers.Tiles;
using HamstarHelpers.Helpers.TModLoader;
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

			Tile mytile = Main.tile[ chest.x, chest.y ];
			string currentChestType;
			if( !TileFrameHelpers.VanillaChestTypeNamesByFrame.TryGetValue(mytile.frameX / 36, out currentChestType) ) {
				throw new ModHelpersException( "Could not find chest frame" );
			}
//LogHelpers.Log("chest "+i+" pos:"+mychest.x+","+mychest.y+", frame:"+(mytile.frameX/36)+", wall:"+mytile.wall+" "+(mychest.item[0]!=null?mychest.item[0].Name:"..."));

			foreach( (string defSet, ChestImplanterSetDefinition setDef) in ChestImplantsMod.Config.ChestImplanterDefinitions ) {
				ChestImplanterDefinition implantDef = ChestImplanter.GetRandomImplanterFromSet( setDef );
				if( implantDef == null ) {
					continue;
				}

				bool isMatched = false;
				foreach( string checkChestType in implantDef.ChestTypes ) {
					if( ChestImplanter.IsChestMatch( currentChestType, checkChestType ) ) {
						isMatched = true;
						break;
					}
				}
				if( !isMatched ) {
					continue;
				}

				foreach( ChestImplanterItemDefinition itemDef in implantDef.ItemDefinitions ) {
					if( ChestImplanter.CanChestAcceptImplantItem( mytile, itemDef ) ) {
						ChestImplanter.Implant( chest, itemDef );
					}
				}

				break;
			}
			
			foreach( CustomChestImplanter customStuffer in mymod.CustomImplanter ) {
				customStuffer( currentChestType, chest );
			}
		}

		private static ChestImplanterDefinition GetRandomImplanterFromSet( ChestImplanterSetDefinition setDef ) {
			if( setDef.Count == 0 ) {
				return null;
			}

			UnifiedRandom rand = TmlHelpers.SafelyGetRand();
			float totalWeight = setDef.TotalWeight();
			float randPick = rand.NextFloat() * totalWeight;

			float countedWeights = setDef[0].Weight;

			int i = 0;
			for( i = 1; i < setDef.Count; i++ ) {
				if( countedWeights > randPick ) {
					return setDef[ i-1 ];
				}
				countedWeights += setDef[i].Weight;
			}

			LogHelpers.Warn( "Could not randomly pick from implanter set." );
			return null;
		}


		////////////////

		public static bool CanChestAcceptImplantItem( Tile chestTile, ChestImplanterItemDefinition info ) {
			if( info.WallId != -1 && info.WallId != chestTile.wall ) {
				return false;
			}
			return Main.rand.NextFloat() >= info.ChancePerChest;
		}


		////////////////

		public static void Implant( Chest chest, ChestImplanterItemDefinition info ) {
			int addedAmount = (int)( Main.rand.Next( info.MaxQuantity - info.MinQuantity ) );
			int amount = info.MinQuantity + addedAmount;
			if( amount == 0 ) {
				return;
			}

			int itemType = info.ChestItem.Type;
			if( itemType == 0 ) {
				LogHelpers.Alert( "Invalid item key " + info.ChestItem );
				return;
			}

			if( amount > 0 ) {
				ChestImplanter.PrependItemToChest( chest, itemType, amount, info );
			} else {
				ChestImplanter.ExtractItemFromChest( chest, itemType, -amount );
			}
		}
	}
}
