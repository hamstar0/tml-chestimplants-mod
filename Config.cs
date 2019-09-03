using System;
using System.Collections.Generic;
using System.ComponentModel;
using Terraria.ID;
using Terraria.ModLoader.Config;


namespace ChestImplants {
	public class ChestImplantsConfig : ModConfig {
		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////

		[DefaultValue(true)]
		public bool DebugModeInfo = true;

		public Dictionary<string, HashSet<ChestImplantItemDefinition>> Stuffers = new Dictionary<string, HashSet<ChestImplantItemDefinition>> {
			{
				"Web Covered Chest", new HashSet<ChestImplantItemDefinition> {
					new ChestImplantItemDefinition {
						UniqueKey = ItemID.GetUniqueKey( ItemID.Silk ),
						WallId = WallID.SpiderUnsafe,
						SpawnChancePerChest = 1f,
						MinQuantity = 99,
						MaxQuantity = 99,
					}
				}
			}
		};
	}
}
