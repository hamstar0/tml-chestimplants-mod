using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Tiles;


namespace ChestImplants {
	public partial class ChestImplanter {
		public static void PrependItemToChest( Chest chest, int itemType, int amount, int prefix=0 ) {
			// Shift items up
			for( int i = chest.item.Length - 1; i > 0; i-- ) {
				chest.item[i] = chest.item[i-1];
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

				string chestName;
				if( !TileFrameHelpers.VanillaChestTypeNamesByFrame.TryGetValue( mytile.frameX / 36, out chestName ) ) {
					chestName = "Unknown (modded?) chest";
				}

				LogHelpers.Log(
					" Implanted "+chestName+" ("+chest.x+", "+chest.y+") with "+amount+" "+info.ChestItem.ToString() );
			}
		}

		////

		public static bool ExtractItemFromChest( Chest chest, int itemType, int amount ) {
			// Trim from the first discovered stack of the given item type until the subtraction amount is met
			int foundAt = -1;
			for( int i=0; i<chest.item.Length; i++ ) {
				if( chest.item[i]?.type != itemType ) {
					continue;
				}

				foundAt = i;

				if( chest.item[i].stack > amount ) {
					chest.item[i].stack -= amount;
				} else {
					chest.item[i].active = false;
					chest.item[i] = new Item();
				}
				break;
			}

			// No items found to extract?
			if( foundAt == -1 ) {
				if( ChestImplantsConfig.Instance.DebugModeVerboseInfo ) {
					if( itemType < Main.itemTexture.Length ) {
						LogHelpers.Log( "Could not find item "+ItemID.Search.GetName(itemType)+" to extract "+amount+" of." );
					} else {
						LogHelpers.Log( "Could not find mod item "+itemType+" to extract "+amount+" of." );
					}
				}
				return false;
			}

			// Shift existing chest items down to fill the empty item slot (if empty, due to extraction)
			if( chest.item[foundAt].stack <= 0 ) {
				for( int i = foundAt; i < chest.item.Length - 2; i++ ) {
					chest.item[i] = chest.item[i + 1];
				}
				chest.item[chest.item.Length - 1] = new Item();
			}

			// Debugging's a bitch
			if( ChestImplantsConfig.Instance.DebugModeInfo ) {
				Tile chestTile = Main.tile[chest.x, chest.y];
				/*int chestType = chestTile?.type ?? -1;
				string chestName = chestType < 0
					? "Unknown chest"
					: chestType >= Main.tileTexture.Length
						? TileID.Search.GetName( chestType )
						: "Modded chest";*/
				string chestName;
				if( !TileFrameHelpers.VanillaChestTypeNamesByFrame.TryGetValue( chestTile.frameX / 36, out chestName ) ) {
					chestName = "Unknown (modded?) chest";
				}

				if( itemType < Main.itemTexture.Length ) {
					LogHelpers.Log(
						"Extracted " + amount
						+ " of item " + ItemID.Search.GetName(itemType)
						+ " from chest " + chestName + " at " + chest.x + "," + chest.y
					);
				} else {
					LogHelpers.Log(
						"Extracted " + amount
						+ " of mod item type " + itemType
						+ " from chest " + chestName + " at " + chest.x + "," + chest.y
					);
				}
			}

			return true;
		}
	}
}
