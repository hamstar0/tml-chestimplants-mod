using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader.Config;
using HamstarHelpers.Classes.UI.ModConfig;
using HamstarHelpers.Services.Configs;


namespace ChestImplants {
	class MyFloatInputElement : FloatInputElement { }




	public class ChestImplantsConfig : StackableModConfig {
		public static ChestImplantsConfig Instance => ModConfigStack.GetMergedConfigs<ChestImplantsConfig>();



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

		public override void OverlayChanges( StackableModConfig changes ) {
			var mychanges = (ChestImplantsConfig)changes;

			foreach( Ref<ChestImplanterDefinition> implanter in mychanges.AllFromSetChestImplanterDefinitions.Value ) {
				this.AllFromSetChestImplanterDefinitions.Value.Add( implanter );
			}
			foreach( Ref<ChestImplanterDefinition> implanter in mychanges.RandomPickFromSetChestImplanterDefinitions1.Value ) {
				this.RandomPickFromSetChestImplanterDefinitions1.Value.Add( implanter );
			}
			foreach( Ref<ChestImplanterDefinition> implanter in mychanges.RandomPickFromSetChestImplanterDefinitions2.Value ) {
				this.RandomPickFromSetChestImplanterDefinitions2.Value.Add( implanter );
			}
			foreach( Ref<ChestImplanterDefinition> implanter in mychanges.RandomPickFromSetChestImplanterDefinitions3.Value ) {
				this.RandomPickFromSetChestImplanterDefinitions3.Value.Add( implanter );
			}
			foreach( Ref<ChestImplanterDefinition> implanter in mychanges.RandomPickFromSetChestImplanterDefinitions4.Value ) {
				this.RandomPickFromSetChestImplanterDefinitions4.Value.Add( implanter );
			}
			foreach( Ref<ChestImplanterDefinition> implanter in mychanges.RandomPickFromSetChestImplanterDefinitions5.Value ) {
				this.RandomPickFromSetChestImplanterDefinitions5.Value.Add( implanter );
			}
			foreach( Ref<ChestImplanterDefinition> implanter in mychanges.RandomPickFromSetChestImplanterDefinitions6.Value ) {
				this.RandomPickFromSetChestImplanterDefinitions6.Value.Add( implanter );
			}
			foreach( Ref<ChestImplanterDefinition> implanter in mychanges.RandomPickFromSetChestImplanterDefinitions7.Value ) {
				this.RandomPickFromSetChestImplanterDefinitions7.Value.Add( implanter );
			}
			foreach( Ref<ChestImplanterDefinition> implanter in mychanges.RandomPickFromSetChestImplanterDefinitions8.Value ) {
				this.RandomPickFromSetChestImplanterDefinitions8.Value.Add( implanter );
			}

			mychanges.AllFromSetChestImplanterDefinitions = this.AllFromSetChestImplanterDefinitions;
			mychanges.RandomPickFromSetChestImplanterDefinitions1 = this.RandomPickFromSetChestImplanterDefinitions1;
			mychanges.RandomPickFromSetChestImplanterDefinitions2 = this.RandomPickFromSetChestImplanterDefinitions2;
			mychanges.RandomPickFromSetChestImplanterDefinitions3 = this.RandomPickFromSetChestImplanterDefinitions3;
			mychanges.RandomPickFromSetChestImplanterDefinitions4 = this.RandomPickFromSetChestImplanterDefinitions4;
			mychanges.RandomPickFromSetChestImplanterDefinitions5 = this.RandomPickFromSetChestImplanterDefinitions5;
			mychanges.RandomPickFromSetChestImplanterDefinitions6 = this.RandomPickFromSetChestImplanterDefinitions6;
			mychanges.RandomPickFromSetChestImplanterDefinitions7 = this.RandomPickFromSetChestImplanterDefinitions7;
			mychanges.RandomPickFromSetChestImplanterDefinitions8 = this.RandomPickFromSetChestImplanterDefinitions8;

			base.OverlayChanges( changes );
		}


		////////////////

		public IEnumerable<ChestImplanterSetDefinition> GetRandomImplanterSets() {
			yield return this.RandomPickFromSetChestImplanterDefinitions1;
			yield return this.RandomPickFromSetChestImplanterDefinitions2;
			yield return this.RandomPickFromSetChestImplanterDefinitions3;
			yield return this.RandomPickFromSetChestImplanterDefinitions4;
			yield return this.RandomPickFromSetChestImplanterDefinitions5;
			yield return this.RandomPickFromSetChestImplanterDefinitions6;
			yield return this.RandomPickFromSetChestImplanterDefinitions7;
			yield return this.RandomPickFromSetChestImplanterDefinitions8;
		}

		public void ClearRandomImplanterSets() {
			this.RandomPickFromSetChestImplanterDefinitions1.Value.Clear();
			this.RandomPickFromSetChestImplanterDefinitions2.Value.Clear();
			this.RandomPickFromSetChestImplanterDefinitions3.Value.Clear();
			this.RandomPickFromSetChestImplanterDefinitions4.Value.Clear();
			this.RandomPickFromSetChestImplanterDefinitions5.Value.Clear();
			this.RandomPickFromSetChestImplanterDefinitions6.Value.Clear();
			this.RandomPickFromSetChestImplanterDefinitions7.Value.Clear();
			this.RandomPickFromSetChestImplanterDefinitions8.Value.Clear();
		}
	}
}

