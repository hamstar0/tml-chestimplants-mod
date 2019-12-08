using System;
using System.Collections.Generic;
using System.ComponentModel;
using Terraria.ID;
using Terraria.ModLoader.Config;
using HamstarHelpers.Classes.UI.ModConfig;
using HamstarHelpers.Helpers.DotNET.Extensions;
using HamstarHelpers.Services.Configs;


namespace ChestImplants {
	class MyFloatInputElement : FloatInputElement { }




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



		////////////////

		public override void OverlayChanges( StackableModConfig changes ) {
			var mychanges = (ChestImplantsConfig)changes;

			foreach( (string name, ChestImplanterSetDefinition implanterSet) in mychanges.AllFromSetChestImplanterDefinitions ) {
				this.AllFromSetChestImplanterDefinitions[name] = implanterSet;
			}
			foreach( (string name, ChestImplanterSetDefinition implanterSet) in mychanges.RandomPickFromSetChestImplanterDefinitions ) {
				this.AllFromSetChestImplanterDefinitions[name] = implanterSet;
			}

			mychanges.AllFromSetChestImplanterDefinitions = this.AllFromSetChestImplanterDefinitions;
			mychanges.RandomPickFromSetChestImplanterDefinitions = this.RandomPickFromSetChestImplanterDefinitions;

			base.OverlayChanges( changes );
		}
	}
}

