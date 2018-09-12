using HamstarHelpers.Helpers.DebugHelpers;
using System;
using Terraria;


namespace ChestImplants {
	public static partial class ChestImplantsAPI {
		public static ChestImplantsConfigData GetModSettings() {
			return ChestImplantsMod.Instance.Config;
		}

		public static void SaveModSettingsChanges() {
			ChestImplantsMod.Instance.ConfigJson.SaveFile();
		}

		////
		
		public static void AddCustomStuffer( Action<string, int, Chest> stuffer ) {
			ChestImplantsMod.Instance.CustomStuffers.Add( stuffer );
		}
	}
}
