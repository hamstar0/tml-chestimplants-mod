using HamstarHelpers.Helpers.TModLoader.Mods;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;


namespace ChestImplants {
	public delegate void ChestStuffer( string context, int wallId, Chest chest );




	partial class ChestImplantsMod : Mod {
		public static ChestImplantsMod Instance { get; private set; }



		////////////////

		public ChestImplantsConfig Config => this.GetConfig<ChestImplantsConfig>();

		public ISet<ChestStuffer> CustomStuffers { get; } = new HashSet<ChestStuffer>();



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
