using System;
using Terraria;


namespace ChestImplants {
	public static partial class ChestImplantsAPI {
		public static void AddCustomStuffer( CustomChestImplanter stuffer ) {
			ChestImplantsMod.Instance.CustomImplanter.Add( stuffer );
		}

		public static void ClearStuffers() {
			ChestImplantsMod.Instance.CustomImplanter.Clear();
		}
	}
}
