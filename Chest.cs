using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.ItemHelpers;
using System.Collections.Generic;
using Terraria;


namespace ChestImplants {
	public class ChestImplantInfo {
		public string ItemByName;
		public float SpawnChancePerChest;
		public int MinQuantity;
		public int MaxQuantity;
		public int Prefix = 0;
	}




	class ChestImplantStuffer {
		public static string GetContext( int frame ) {
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

			Tile mytile = Main.tile[chest.x, chest.y];
			string context = ChestImplantStuffer.GetContext( mytile.frameX / 36 );
//LogHelpers.Log("chest "+i+" pos:"+mychest.x+","+mychest.y+", frame:"+(mytile.frameX/36)+", wall:"+mytile.wall+" "+(mychest.item[0]!=null?mychest.item[0].Name:"..."));

			if( mymod.Config.Stuffers.ContainsKey( context ) ) {
				var stuffer = mymod.Config.Stuffers[context];

				if( stuffer.ContainsKey( -1 ) ) {
					ChestImplantStuffer.Implant( chest, stuffer[-1] );
				} else if( stuffer.ContainsKey( mytile.wall ) ) {
					ChestImplantStuffer.Implant( chest, stuffer[mytile.wall] );
				}
			}

			foreach( var custom_stuffer in mymod.CustomStuffers ) {
				custom_stuffer( context, mytile.wall, chest );
			}
		}


		////////////////

		public static void Implant( Chest chest, ChestImplantInfo info ) {
			if( Main.rand.NextFloat() >= info.SpawnChancePerChest ) {
				return;
			}

			var mymod = ChestImplantsMod.Instance;
			int added_amount = (int)( Main.rand.NextFloat() * (float)(info.MaxQuantity - info.MinQuantity) );
			int amount = info.MinQuantity + added_amount;
			int id = ItemIdentityHelpers.NamesToIds[ info.ItemByName ];
			
			for( int i=chest.item.Length-1; i>0; i-- ) {
				chest.item[ i-1 ] = chest.item[ i ];
			}

			chest.item[0] = new Item();
			chest.item[0].SetDefaults( id );
			chest.item[0].stack = amount;
			chest.item[0].prefix = (byte)info.Prefix;

			if( mymod.Config.DebugModeInfo ) {
				Tile mytile = Main.tile[chest.x, chest.y];
				string context = ChestImplantStuffer.GetContext( mytile.frameX / 36 );

				LogHelpers.Log( "Stuffed " + context + " ("+chest.x+", "+chest.y+") with " + amount + " " + info.ItemByName );
			}
		}
	}
}
