using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Tiles;
using System.Collections.Generic;
using Terraria;


namespace ChestImplants {
	public partial class ChestImplanter {
		public static void PrependItemToChest( Chest chest, int itemType, int amount, int prefix=0 ) {
			// Shift items up
			for( int i = chest.item.Length - 1; i > 0; i-- ) {
				chest.item[i - 1] = chest.item[i];
			}

			// Insert new item
			chest.item[0] = new Item();
			chest.item[0].SetDefaults( itemType );
			chest.item[0].stack = amount;
			chest.item[0].prefix = (byte)prefix;
		}

		public static void PrependItemToChest( Chest chest, int itemType, int amount, ChestImplanterItemDefinition info ) {
			ChestImplanter.PrependItemToChest( chest, itemType, amount, info.Prefix );

			if( ChestImplantsConfig.Instance.DebugModeInfo ) {
				Tile mytile = Main.tile[chest.x, chest.y];

				string context;
				if( !TileFrameHelpers.VanillaChestTypeNamesByFrame.TryGetValue( mytile.frameX / 36, out context ) ) {
					throw new ModHelpersException( "Could not find chest frame" );
				}

				LogHelpers.Log( "Implanted " + context + " ("+chest.x+", "+chest.y+") with " + amount + " " + info.ChestItem.ToString() );
			}
		}

		////

		public static bool ExtractItemFromChest( Chest chest, int itemType, int amount ) {
			var chestItems = new List<Item>( chest.item );

			int foundAt = -1;
			for( int i=0; i<chestItems.Count; i++ ) {
				if( chestItems[i].type == itemType ) {
					foundAt = i;

					if( chestItems[i].stack > amount ) {
						chestItems[i].stack -= amount;
					}

					break;
				}
			}

			if( foundAt == -1 ) {
				if( ChestImplantsConfig.Instance.DebugModeInfo ) {
					LogHelpers.Log( "Could not find item "+itemType+" to extract "+amount+" of." );
				}
				return false;
			}

			if( chestItems[foundAt].stack <= 0 ) {
				for( int i = foundAt; i < chestItems.Count - 2; i++ ) {
					chestItems[i] = chestItems[i + 1];
				}
				chestItems[chestItems.Count - 1] = new Item();
			}

			if( ChestImplantsConfig.Instance.DebugModeInfo ) {
				LogHelpers.Log( "Extracted " + amount + " of item type " + itemType );
			}
			return true;
		}
	}
}
