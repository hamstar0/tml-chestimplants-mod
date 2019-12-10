using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader.Config;


namespace ChestImplants {
	public class ChestImplanterSetDefinition : Ref<List<Ref<ChestImplanterDefinition>>> {
		public ChestImplanterSetDefinition() {
			this.Value = new List<Ref<ChestImplanterDefinition>>();
		}

		public ChestImplanterSetDefinition( List<Ref<ChestImplanterDefinition>> list ) {
			this.Value = list;
		}

		////////////////

		public float TotalWeight() {
			return this.Value?.Sum( def => def.Value.Weight )
					?? 0f;
		}
	}



	public class ChestImplanterDefinition {
		[Range( 0f, 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float Weight { get; set; }

		public List<Ref<string>> ChestTypes { get; set; } = new List<Ref<string>>();

		public List<ChestImplanterItemDefinition> ItemDefinitions { get; set; } = new List<ChestImplanterItemDefinition>();
	}




	public class ChestImplanterItemDefinition {
		public ItemDefinition ChestItem { get; set; } = new ItemDefinition( ItemID.DirtBlock );

		[Range( -1, 1000 )]
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

		[Range( -1, 100 )]
		[DefaultValue( 0 )]
		public int Prefix { get; set; } = 0;



		////////////////

		public string ToCustomString() {
			return "Implanter "
				+this.MinQuantity+":"+this.MaxQuantity
				+" "+this.ChestItem.ToString()+"@"+this.ChancePerChest.ToString("N2");
		}
	}
}

