using HamstarHelpers.Helpers.Debug;
using System.Collections.Generic;
using Terraria;


namespace ChestImplants {
	class ChestImplantStuffer {
		public static string GetChestTypeOfFrame( int frame ) {
			switch( frame ) {
			case 0:
				return "Chest";
			case 1:
				return "Gold Chest";
			case 2:
				return "Locked Gold Chest";
			case 4:
				return "Shadow Chest";
			case 8:
				return "Mushroom Chest";    //?
			case 10:
				return "Rich Mahogany Chest";
			case 11:
				return "Ice Chest";
			case 12:
				return "Living Wood Chest";
			case 13:
				return "Skyware Chest";
			case 15:
				return "Web Covered Chest";
			case 16:
				return "Lihzahrd Chest";
			case 17:
				return "Water Chest";
			case 50:
				return "Granite Chest";
			case 51:
				return "Marble Chest";
			case 23:
				return "Jungle Chest";
			case 24:
				return "Corruption Chest";
			case 25:
				return "Crimson Chest";
			case 26:
				return "Hallowed Chest";
			case 27:
				return "Frozen Chest";
			default:
				throw new KeyNotFoundException( "Frame: " + frame );
			}
		}
		
		////////////////

		public static bool IsChestEmpty( Chest chest ) {
			for( int i = 0; i < chest.item.Length; i++ ) {
				if( chest.item[i] == null || chest.item[i].IsAir ) { continue; }
				return false;
			}
			return true;
		}


		////////////////

		public static void StuffChest( Chest chest ) {
			var mymod = ChestImplantsMod.Instance;

			Tile mytile = Main.tile[ chest.x, chest.y ];
			string currentContext = ChestImplantStuffer.GetChestTypeOfFrame( mytile.frameX / 36 );
//LogHelpers.Log("chest "+i+" pos:"+mychest.x+","+mychest.y+", frame:"+(mytile.frameX/36)+", wall:"+mytile.wall+" "+(mychest.item[0]!=null?mychest.item[0].Name:"..."));
			
			foreach( ChestImplanterDefinition implantDef in ChestImplantsMod.Config.ChestImplanterDefinitions ) {
				if( implantDef.ChestContext != currentContext ) {
					continue;
				}

				foreach( ChestImplanterItemDefinition itemDef in implantDef.ItemDefinitions ) {
					if( itemDef.WallId == -1 || itemDef.WallId == mytile.wall ) {
						if( ChestImplantStuffer.CanChestRandomlyBeImplanted( itemDef ) ) {
							ChestImplantStuffer.Implant( chest, itemDef );
						}
					}
				}
			}
			
			foreach( CustomChestImplanter customStuffer in mymod.CustomImplanter ) {
				customStuffer( currentContext, chest );
			}
		}


		////////////////

		public static bool CanChestRandomlyBeImplanted( ChestImplanterItemDefinition info ) {
			return Main.rand.NextFloat() >= info.ChancePerChest;
		}

		////

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
				ChestImplantStuffer.PrependItemToChest( chest, itemType, amount, info );
			} else {
				ChestImplantStuffer.ExtractItemFromChest( chest, itemType, -amount );
			}
		}


		////////////////

		public static void PrependItemToChest( Chest chest, int itemType, int amount, ChestImplanterItemDefinition info ) {
			// Shift items up
			for( int i=chest.item.Length-1; i>0; i-- ) {
				chest.item[ i-1 ] = chest.item[ i ];
			}

			// Insert new item
			chest.item[0] = new Item();
			chest.item[0].SetDefaults( itemType );
			chest.item[0].stack = amount;
			chest.item[0].prefix = (byte)info.Prefix;

			if( ChestImplantsMod.Config.DebugModeInfo ) {
				Tile mytile = Main.tile[chest.x, chest.y];
				string context = ChestImplantStuffer.GetChestTypeOfFrame( mytile.frameX / 36 );

				LogHelpers.Log( "Stuffed " + context + " ("+chest.x+", "+chest.y+") with " + amount + " " + info.ChestItem.ToString() );
			}
		}

		////

		public static void ExtractItemFromChest( Chest chest, int itemType, int amount ) {
			var chestList = new List<Item>( chest.item );

			int foundAt = -1;
			for( int i=0; i<chestList.Count; i++ ) {
				if( chestList[i].type == itemType ) {
					if( chestList[i].stack > amount ) {
						chestList[i].stack -= amount;
						return;
					} else {
						foundAt = i;
						break;
					}
				}
			}

			if( foundAt == -1 ) {
				//LogHelpers.LogOnce( "Could not find item "+itemType+" to extract "+amount+" of." );
				return;
			}

			for( int i=foundAt; i < chestList.Count - 1; i++ ) {
				chestList[i] = chestList[i + 1];
			}
		}
	}
}
