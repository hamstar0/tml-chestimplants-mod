using HamstarHelpers.Helpers.TModLoader.Mods;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;


namespace ChestImplants {
	public delegate void CustomChestImplanter( string context, Chest chest );




	public partial class ChestImplantsMod : Mod {
		public static ChestImplantsMod Instance { get; private set; }


		////////////////

		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-chestimplants-mod";



		////////////////

		internal IDictionary<string, CustomChestImplanter> CustomImplanter { get; } = new ConcurrentDictionary<string, CustomChestImplanter>();



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
