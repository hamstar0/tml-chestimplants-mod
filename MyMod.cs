using HamstarHelpers.Helpers.TModLoader.Mods;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;


namespace ChestImplants {
	public delegate void CustomChestImplanter( string context, Chest chest );




	partial class ChestImplantsMod : Mod {
		public static ChestImplantsMod Instance { get; private set; }
		public static ChestImplantsConfig Config => ModContent.GetInstance<ChestImplantsConfig>();


		////////////////

		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-chestimplants-mod";



		////////////////

		public ISet<CustomChestImplanter> CustomImplanter { get; } = new HashSet<CustomChestImplanter>();



		////////////////
		
		public ChestImplantsMod() {
			ChestImplantsMod.Instance = this;
		}

		////////////////

		public override void Load() {
		}

		public override void Unload() {
			ChestImplantsMod.Instance = null;
		}


		////////////////

		public override object Call( params object[] args ) {
			return ModBoilerplateHelpers.HandleModCall( typeof( ChestImplantsAPI ), args );
		}
	}
}
