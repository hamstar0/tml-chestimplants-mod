using HamstarHelpers.Classes.Errors;
using System;
using Terraria;


namespace ChestImplants {
	public static partial class ChestImplantsAPI {
		public static void AddCustomImplanter( string name, CustomChestImplanter stuffer ) {
			if( ChestImplantsMod.Instance.CustomImplanter.ContainsKey(name) ) {
				throw new ModHelpersException( "Implanter " + name + " already defined." );
			}
			ChestImplantsMod.Instance.CustomImplanter[name] = stuffer;
		}

		public static void ClearCustomImplanters() {
			ChestImplantsMod.Instance.CustomImplanter.Clear();
		}
	}
}
