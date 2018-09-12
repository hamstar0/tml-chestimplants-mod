using System;
using Terraria;


namespace ChestImplants {
	public static partial class ChestImplantsAPI {
		internal static object Call( string call_type, params object[] args ) {
			switch( call_type ) {
			case "GetModSettings":
				return ChestImplantsAPI.GetModSettings();
			case "SaveModSettingsChanges":
				ChestImplantsAPI.SaveModSettingsChanges();
				return null;

			case "AddCustomStuffer":
				if( args.Length < 1 ) { throw new Exception( "Insufficient parameters for API call " + call_type ); }

				var stuffer = args[0] as Action<string, int, Chest>;
				if( stuffer == null ) { throw new Exception( "Invalid parameter stuffer for API call " + call_type ); }

				ChestImplantsAPI.AddCustomStuffer( stuffer );
				return null;
			}

			throw new Exception( "No such api call " + call_type );
		}
	}
}
