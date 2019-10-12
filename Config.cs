using HamstarHelpers.Classes.UI.ModConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Terraria.ID;
using Terraria.ModLoader.Config;


namespace ChestImplants {
	public class ChestImplanterDefinition {
		public string ChestContext;
		public HashSet<ChestImplanterItemDefinition> ItemDefinitions;
	}




	public class ChestImplanterItemDefinition {
		public ItemDefinition ChestItem;
		public int WallId;
		[Range(0f, 1f)]
		[CustomModConfigItem( typeof( FloatInputElement ) )]
		public float SpawnChancePerChest;
		public int MinQuantity;
		public int MaxQuantity;
		public int Prefix = 0;
	}




	public class ChestImplantsConfig : ModConfig {
		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////

		[DefaultValue(true)]
		public bool DebugModeInfo = true;

		public HashSet<ChestImplanterDefinition> ChestStuffers =
			new HashSet<ChestImplanterDefinition> {
				new ChestImplanterDefinition {
					ChestContext = "Web Covered Chest",
					ItemDefinitions = new HashSet<ChestImplanterItemDefinition> {
						new ChestImplanterItemDefinition {
							ChestItem = new ItemDefinition( ItemID.Silk ),
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
