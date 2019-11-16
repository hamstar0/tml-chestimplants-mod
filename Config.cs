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
		public List<ChestImplanterItemDefinition> ItemDefinitions;
	}




	public class ChestImplanterItemDefinition {
		public ItemDefinition ChestItem { get; set; }

		public int WallId { get; set; } = -1;

		[Range( 0f, 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float ChancePerChest { get; set; } = 1f;

		[Range( 0, 999 )]
		[DefaultValue( 1 )]
		public int MinQuantity { get; set; } = 1;

		[Range( 0, 999 )]
		[DefaultValue( 1 )]
		public int MaxQuantity { get; set; } = 1;

		public int Prefix { get; set; } = 0;
	}




	public class ChestImplantsConfig : ModConfig {
		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////

		[DefaultValue(true)]
		public bool DebugModeInfo = true;

		public List<ChestImplanterDefinition> ChestImplanterDefinitions { get; set; } =
			new List<ChestImplanterDefinition> {
				new ChestImplanterDefinition {
					ChestContext = "Web Covered Chest",
					ItemDefinitions = new List<ChestImplanterItemDefinition> {
						new ChestImplanterItemDefinition {
							ChestItem = new ItemDefinition( ItemID.Silk ),
							WallId = WallID.SpiderUnsafe,
							ChancePerChest = 1f,
							MinQuantity = 99,
							MaxQuantity = 99
						}
					}
				},
				new ChestImplanterDefinition {
					ChestContext = "Gold Chest",
					ItemDefinitions = new List<ChestImplanterItemDefinition> {
						new ChestImplanterItemDefinition {
							ChestItem = new ItemDefinition( ItemID.MagicMirror ),
							ChancePerChest = 1f,
							MinQuantity = -1,
							MaxQuantity = -1
						}
					}
				},
				new ChestImplanterDefinition {
					ChestContext = "Ice Chest",
					ItemDefinitions = new List<ChestImplanterItemDefinition> {
						new ChestImplanterItemDefinition {
							ChestItem = new ItemDefinition( ItemID.IceMirror ),
							ChancePerChest = 1f,
							MinQuantity = -1,
							MaxQuantity = -1
						}
					}
				}
			};
	}
}
