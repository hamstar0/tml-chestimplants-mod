using HamstarHelpers.Classes.UI.ModConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Terraria.ID;
using Terraria.ModLoader.Config;


namespace ChestImplants {
	class MyFloatInputElement : FloatInputElement { }





	public class ChestImplanterDefinition {
		public string ChestContext;
		public HashSet<ChestImplanterItemDefinition> ItemDefinitions;
	}




	public class ChestImplanterItemDefinition {
		public ItemDefinition ChestItem { get; set; }

		public int WallId { get; set; }

		[Range(0f, 1f)]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float SpawnChancePerChest { get; set; }

		[Range(0, 999)]
		[DefaultValue( 1 )]
		public int MinQuantity { get; set; }

		[Range( 0, 999 )]
		[DefaultValue( 1 )]
		public int MaxQuantity { get; set; }

		public int Prefix { get; set; } = 0;
	}




	public class ChestImplantsConfig : ModConfig {
		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////

		[DefaultValue(true)]
		public bool DebugModeInfo = true;

		public List<ChestImplanterDefinition> ChestStuffers { get; set; } =
			new List<ChestImplanterDefinition> {
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
