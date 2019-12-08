using HamstarHelpers.Classes.UI.ModConfig;
using HamstarHelpers.Services.Configs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Terraria.ID;
using Terraria.ModLoader.Config;


namespace ChestImplants {
	public class ChestImplanterSetDefinition : List<ChestImplanterDefinition> {
		public float TotalWeight() {
			return this?.Sum( def => def.Weight )
					?? 0f;
		}
	}



	public class ChestImplanterDefinition {
		[Range( 0f, 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float Weight { get; set; }
		public HashSet<string> ChestTypes { get; set; }
		public List<ChestImplanterItemDefinition> ItemDefinitions { get; set; }
	}




	public class ChestImplanterItemDefinition {
		public ItemDefinition ChestItem { get; set; }

		[DefaultValue( -1 )]
		public int WallId { get; set; } = -1;

		[Range( 0f, 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float ChancePerChest { get; set; } = 1f;

		[Range( 0, 999 )]
		[DefaultValue( 1 )]
		public int MinQuantity { get; set; } = 1;

		[Range( 0, 999 )]
		[DefaultValue( 1 )]
		public int MaxQuantity { get; set; } = 1;

		[DefaultValue( 0 )]
		public int Prefix { get; set; } = 0;



		////////////////

		public override string ToString() {
			return "Implanter "
				+this.MinQuantity+":"+this.MaxQuantity
				+" "+this.ChestItem.ToString()+"@"+this.ChancePerChest.ToString("N2");
		}
	}
}

