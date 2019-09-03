using System;
using Terraria;


namespace ChestImplants {
	public static partial class ChestImplantsAPI {
		public static void AddCustomStuffer( ChestStuffer stuffer ) {
			ChestImplantsMod.Instance.CustomStuffers.Add( stuffer );
		}


		public static void ClearStuffers() {
			ChestImplantsMod.Instance.CustomStuffers.Clear();
		}
	}
}
