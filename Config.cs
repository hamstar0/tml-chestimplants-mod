using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Classes.UI.ModConfig;


namespace ChestImplants {
	class MyFloatInputElement : FloatInputElement { }




	public partial class ChestImplantsConfig : ModConfig {
		public static ChestImplantsConfig Instance => ModContent.GetInstance<ChestImplantsConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////

		[Header( "Chest implanters define a list of items to add (plus stack size) or remove (negative stack size) from chests."
			+"\n \nUse the random lists options for random implanters to be picked (as opposed to random chance per item)."
			+"\n \nItem implanters can specify their item's likeliness of being implanted/extracted."
			+"\n \nItem implanters choose their quantity between a minimum and maximum value."
			+"\n \n \n " )]
		public bool DebugModeInfo = false;
		
		public bool DebugModeVerboseInfo = false;


		[Label( "Chest implanters list" )]
		public ChestImplanterSetDefinition AllFromSetChestImplanterDefinitions { get; set; } =
			new ChestImplanterSetDefinition();
				/*new List<Ref<ChestImplanterDefinition>> {
					new Ref<ChestImplanterDefinition>( new ChestImplanterDefinition {
						Weight = 1f,
						ChestTypes = new List<Ref<string>>{ new Ref<string>("Gold Chest") },
						ItemDefinitions = new List<ChestImplanterItemDefinition> {
							new ChestImplanterItemDefinition {
								ChestItem = new ItemDefinition( ItemID.MagicMirror ),
								ChancePerChest = 1f,
								MinQuantity = -1,
								MaxQuantity = -1
							}
						}
					} ),
					new Ref<ChestImplanterDefinition>( new ChestImplanterDefinition {
						Weight = 1f,
						ChestTypes = new List<Ref<string>>{ new Ref<string>("Ice Chest") },
						ItemDefinitions = new List<ChestImplanterItemDefinition> {
							new ChestImplanterItemDefinition {
								ChestItem = new ItemDefinition( ItemID.IceMirror ),
								ChancePerChest = 1f,
								MinQuantity = -1,
								MaxQuantity = -1
							}
						}
					} )
				}
			);*/

		[Label( "Chest implanters list (random implanter per chest 1)" )]
		public ChestImplanterSetDefinition RandomPickFromSetChestImplanterDefinitions1 { get; set; } =
			new ChestImplanterSetDefinition();
				/*new List<Ref<ChestImplanterDefinition>> {
					new Ref<ChestImplanterDefinition>( new ChestImplanterDefinition {
						Weight = 1f,
						ChestTypes = new List<Ref<string>> { new Ref<string>("Web Covered Chest") },
						ItemDefinitions = new List<ChestImplanterItemDefinition> {
							new ChestImplanterItemDefinition {
								ChestItem = new ItemDefinition( ItemID.Silk ),
								WallId = WallID.SpiderUnsafe,
								ChancePerChest = 1f,
								MinQuantity = 99,
								MaxQuantity = 99
							}
						}
					} )
				}
			);*/
			
		[Label( "Chest implanters list (random implanter per chest 2)" )]
		public ChestImplanterSetDefinition RandomPickFromSetChestImplanterDefinitions2 { get; set; } = new ChestImplanterSetDefinition();

		[Label( "Chest implanters list (random implanter per chest 3)" )]
		public ChestImplanterSetDefinition RandomPickFromSetChestImplanterDefinitions3 { get; set; } = new ChestImplanterSetDefinition();

		[Label( "Chest implanters list (random implanter per chest 4)" )]
		public ChestImplanterSetDefinition RandomPickFromSetChestImplanterDefinitions4 { get; set; } = new ChestImplanterSetDefinition();

		[Label( "Chest implanters list (random implanter per chest 5)" )]
		public ChestImplanterSetDefinition RandomPickFromSetChestImplanterDefinitions5 { get; set; } = new ChestImplanterSetDefinition();

		[Label( "Chest implanters list (random implanter per chest 6)" )]
		public ChestImplanterSetDefinition RandomPickFromSetChestImplanterDefinitions6 { get; set; } = new ChestImplanterSetDefinition();

		[Label( "Chest implanters list (random implanter per chest 7)" )]
		public ChestImplanterSetDefinition RandomPickFromSetChestImplanterDefinitions7 { get; set; } = new ChestImplanterSetDefinition();

		[Label( "Chest implanters list (random implanter per chest 8)" )]
		public ChestImplanterSetDefinition RandomPickFromSetChestImplanterDefinitions8 { get; set; } = new ChestImplanterSetDefinition();



		////////////////

		public IEnumerable<ChestImplanterSetDefinition> GetRandomImplanterSets() {
			yield return this.Get<ChestImplanterSetDefinition>( nameof(this.RandomPickFromSetChestImplanterDefinitions1) );
			yield return this.Get<ChestImplanterSetDefinition>( nameof(this.RandomPickFromSetChestImplanterDefinitions2) );
			yield return this.Get<ChestImplanterSetDefinition>( nameof(this.RandomPickFromSetChestImplanterDefinitions3) );
			yield return this.Get<ChestImplanterSetDefinition>( nameof(this.RandomPickFromSetChestImplanterDefinitions4) );
			yield return this.Get<ChestImplanterSetDefinition>( nameof(this.RandomPickFromSetChestImplanterDefinitions5) );
			yield return this.Get<ChestImplanterSetDefinition>( nameof(this.RandomPickFromSetChestImplanterDefinitions6) );
			yield return this.Get<ChestImplanterSetDefinition>( nameof(this.RandomPickFromSetChestImplanterDefinitions7) );
			yield return this.Get<ChestImplanterSetDefinition>( nameof(this.RandomPickFromSetChestImplanterDefinitions8) );
		}

		public void ClearRandomImplanterSets() {
			this.Get<ChestImplanterSetDefinition>( nameof(this.RandomPickFromSetChestImplanterDefinitions1) ).Value.Clear();
			this.Get<ChestImplanterSetDefinition>( nameof(this.RandomPickFromSetChestImplanterDefinitions2) ).Value.Clear();
			this.Get<ChestImplanterSetDefinition>( nameof(this.RandomPickFromSetChestImplanterDefinitions3) ).Value.Clear();
			this.Get<ChestImplanterSetDefinition>( nameof(this.RandomPickFromSetChestImplanterDefinitions4) ).Value.Clear();
			this.Get<ChestImplanterSetDefinition>( nameof(this.RandomPickFromSetChestImplanterDefinitions5) ).Value.Clear();
			this.Get<ChestImplanterSetDefinition>( nameof(this.RandomPickFromSetChestImplanterDefinitions6) ).Value.Clear();
			this.Get<ChestImplanterSetDefinition>( nameof(this.RandomPickFromSetChestImplanterDefinitions7) ).Value.Clear();
			this.Get<ChestImplanterSetDefinition>( nameof(this.RandomPickFromSetChestImplanterDefinitions8) ).Value.Clear();
		}
	}
}

