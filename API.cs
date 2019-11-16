using System;
using Terraria;


namespace ChestImplants {
	public static partial class ChestImplantsAPI {
		public static void AddCustomImplanter( CustomChestImplanter stuffer ) {
			ChestImplantsMod.Instance.CustomImplanter.Add( stuffer );
		}

		public static void ClearCustomImplanters() {
			ChestImplantsMod.Instance.CustomImplanter.Clear();
		}
	}
}
