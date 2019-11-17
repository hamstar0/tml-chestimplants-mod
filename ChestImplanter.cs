using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Tiles;
using Terraria;


namespace ChestImplants {
	public partial class ChestImplanter {
		public static bool IsChestEmpty( Chest chest ) {
			for( int i = 0; i < chest.item.Length; i++ ) {
				if( chest.item[i] == null || chest.item[i].IsAir ) { continue; }
				return false;
			}
			return true;
		}


		////////////////

		public static void ApplyConfiguredImplantsToChest( Chest chest ) {
			var mymod = ChestImplantsMod.Instance;

			Tile mytile = Main.tile[ chest.x, chest.y ];
			string currentContext;
			if( !TileFrameHelpers.VanillaChestTypeNamesByFrame.TryGetValue(mytile.frameX / 36, out currentContext) ) {
				throw new ModHelpersException( "Could not find chest frame" );
			}
//LogHelpers.Log("chest "+i+" pos:"+mychest.x+","+mychest.y+", frame:"+(mytile.frameX/36)+", wall:"+mytile.wall+" "+(mychest.item[0]!=null?mychest.item[0].Name:"..."));
			
			foreach( ChestImplanterDefinition implantDef in ChestImplantsMod.Config.ChestImplanterDefinitions ) {
				if( !implantDef.ChestTypes.Contains(currentContext) ) {
					continue;
				}

				foreach( ChestImplanterItemDefinition itemDef in implantDef.ItemDefinitions ) {
					if( ChestImplanter.CanChestAcceptImplantItem( mytile, itemDef ) ) {
						ChestImplanter.Implant( chest, itemDef );
					}
				}
			}
			
			foreach( CustomChestImplanter customStuffer in mymod.CustomImplanter ) {
				customStuffer( currentContext, chest );
			}
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
