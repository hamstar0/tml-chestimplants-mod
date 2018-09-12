using HamstarHelpers.Components.Config;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;


namespace ChestImplants {
	partial class ChestImplantsMod : Mod {
		public static ChestImplantsMod Instance { get; private set; }



		////////////////

		public JsonConfig<ChestImplantsConfigData> ConfigJson { get; private set; }
		public ChestImplantsConfigData Config { get { return this.ConfigJson.Data; } }

		public ISet<Action<string, int, Chest>> CustomStuffers = new HashSet<Action<string, int, Chest>>();


		////////////////
		
		public ChestImplantsMod() {
			this.ConfigJson = new JsonConfig<ChestImplantsConfigData>(
				ChestImplantsConfigData.ConfigFileName,
				ConfigurationDataBase.RelativePath,
				new ChestImplantsConfigData()
			);
		}

		////////////////

		public override void Load() {
			ChestImplantsMod.Instance = this;

			this.LoadConfig();
		}

		private void LoadConfig() {
			if( !this.ConfigJson.LoadFile() ) {
				this.ConfigJson.SaveFile();
				ErrorLogger.Log( "Chest Implants config " + this.Version.ToString() + " created." );
			}
			
			if( this.Config.UpdateToLatestVersion(this) ) {
				ErrorLogger.Log( "Chest Implants updated to " + this.Version.ToString() );
				this.ConfigJson.SaveFile();
			}
		}

		public override void Unload() {
			ChestImplantsMod.Instance = null;
		}


		////////////////

		public override object Call( params object[] args ) {
			if( args.Length == 0 ) { throw new Exception( "Undefined call type." ); }

			string call_type = args[0] as string;
			if( args == null ) { throw new Exception( "Invalid call type." ); }

			var new_args = new object[args.Length - 1];
			Array.Copy( args, 1, new_args, 0, args.Length - 1 );

			return ChestImplantsAPI.Call( call_type, new_args );
		}
	}
}
