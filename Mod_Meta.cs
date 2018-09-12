using HamstarHelpers.Components.Config;
using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;


namespace ChestImplants {
	partial class ChestImplantsMod : Mod {
		public static string GithubUserName { get { return "hamstar0"; } }
		public static string GithubProjectName { get { return "tml-chestimplants-mod"; } }

		public static string ConfigFileRelativePath {
			get { return ConfigurationDataBase.RelativePath + Path.DirectorySeparatorChar + ChestImplantsConfigData.ConfigFileName; }
		}

		public static void ReloadConfigFromFile() {
			if( Main.netMode != 0 ) {
				throw new Exception( "Cannot reload configs outside of single player." );
			}
			if( !ChestImplantsMod.Instance.ConfigJson.LoadFile() ) {
				ChestImplantsMod.Instance.ConfigJson.SaveFile();
			}
		}

		public static void ResetConfigFromDefaults() {
			if( Main.netMode != 0 ) {
				throw new Exception( "Cannot reset to default configs outside of single player." );
			}

			var config_data = new ChestImplantsConfigData();
			config_data.SetDefaults();

			ChestImplantsMod.Instance.ConfigJson.SetData( config_data );
			ChestImplantsMod.Instance.ConfigJson.SaveFile();
		}
	}
}
