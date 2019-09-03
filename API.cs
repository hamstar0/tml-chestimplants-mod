using System;
using Terraria;
using static ChestImplants.ChestImplantsMod;


namespace ChestImplants {
	public static partial class ChestImplantsAPI {
		public static void AddCustomStuffer( ChestStuffer stuffer ) {
			ChestImplantsMod.Instance.CustomStuffers.Add( stuffer );
		}
	}
}
