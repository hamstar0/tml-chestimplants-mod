using HamstarHelpers.Components.Config;
using HamstarHelpers.Helpers.DebugHelpers;
using System;
using System.Collections.Generic;
using Terraria.ID;


namespace ChestImplants {
	public class ChestImplantsConfigData : ConfigurationDataBase {
		public static string ConfigFileName => "Chest Implants Config.json";


		////////////////

		public string VersionSinceUpdate = new Version( 0, 0, 0, 0 ).ToString();

		public bool DebugModeInfo = true;

		public IDictionary<string, IDictionary<int, ChestImplantInfo>> Stuffers = new Dictionary<string, IDictionary<int, ChestImplantInfo>>();



		////////////////

		public void SetDefaults() {
			this.Stuffers.Clear();

			this.Stuffers["Web Covered Chest"] = new Dictionary<int, ChestImplantInfo> {
				{
					WallID.SpiderUnsafe, new ChestImplantInfo {
						ItemByName = "Silk",
						SpawnChancePerChest = 1f,
						MinQuantity = 99,
						MaxQuantity = 99,
					}
				}
			};
		}


		////////////////

		internal bool UpdateToLatestVersion( ChestImplantsMod mymod ) {
			var new_config = new ChestImplantsConfigData();
			var vers_since = this.VersionSinceUpdate != "" ?
				new Version( this.VersionSinceUpdate ) :
				new Version();
			
			if( vers_since >= mymod.Version ) {
				return false;
			}

			if( vers_since == new Version(0, 0, 0, 0) ) {
				this.SetDefaults();
			}

			this.VersionSinceUpdate = mymod.Version.ToString();

			return true;
		}
	}
}
