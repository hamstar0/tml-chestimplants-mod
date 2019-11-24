using HamstarHelpers.Classes.UI.ModConfig;
using HamstarHelpers.Services.Configs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Terraria.ID;
using Terraria.ModLoader.Config;


namespace ChestImplants {
	class MyFloatInputElement : FloatInputElement { }





	public class ChestImplanterSetDefinition : List<ChestImplanterDefinition> {
		public float TotalWeight() {
			return this?.Sum( def => def.Weight )
					?? 0f;
		}
	}



	public class ChestImplanterDefinition {
		[Range( 0f, 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float Weight { get; set; }
		public HashSet<string> ChestTypes { get; set; }
		public List<ChestImplanterItemDefinition> ItemDefinitions { get; set; }
	}




	public class ChestImplanterItemDefinition {
		public ItemDefinition ChestItem { get; set; }

		[DefaultValue( -1 )]
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

		[DefaultValue( 0 )]
		public int Prefix { get; set; } = 0;
	}




	public class ChestImplantsConfig : StackableModConfig {
		public static ChestImplantsConfig Instance => ModConfigStack.GetMergedConfigs<ChestImplantsConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////

		[DefaultValue( true )]
		public bool DebugModeInfo = true;

		public Dictionary<string, ChestImplanterSetDefinition> AllFromSetChestImplanterDefinitions { get; set; } =
			new Dictionary<string, ChestImplanterSetDefinition> {
				{ "ChestImplantsDefaultSampleNoMirror", new ChestImplanterSetDefinition {
					new ChestImplanterDefinition {
						Weight = 1f,
						ChestTypes = new HashSet<string> { "Gold Chest" },
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
						Weight = 1f,
						ChestTypes = new HashSet<string> { "Ice Chest" },
						ItemDefinitions = new List<ChestImplanterItemDefinition> {
							new ChestImplanterItemDefinition {
								ChestItem = new ItemDefinition( ItemID.IceMirror ),
								ChancePerChest = 1f,
								MinQuantity = -1,
								MaxQuantity = -1
							}
						}
					}
				} }
			};

		public Dictionary<string, ChestImplanterSetDefinition> RandomPickFromSetChestImplanterDefinitions { get; set; } =
			new Dictionary<string, ChestImplanterSetDefinition> {
				{ "ChestImplantsDefaultSampleSilk", new ChestImplanterSetDefinition {
					new ChestImplanterDefinition {
						Weight = 1f,
						ChestTypes = new HashSet<string> { "Web Covered Chest" },
						ItemDefinitions = new List<ChestImplanterItemDefinition> {
							new ChestImplanterItemDefinition {
								ChestItem = new ItemDefinition( ItemID.Silk ),
								WallId = WallID.SpiderUnsafe,
								ChancePerChest = 1f,
								MinQuantity = 99,
								MaxQuantity = 99
							}
						}
					}
				} }
			};
	}
}

