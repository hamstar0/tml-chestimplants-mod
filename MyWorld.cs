using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;


namespace ChestImplants {
	class ChestStufferGenPass : GenPass {
		public ChestStufferGenPass() : base( "Chest Implants: Stuff Chests", 1f ) { }


		////////////////

		public override void Apply( GenerationProgress progress ) {
			for( int i=0; i<Main.chest.Length; i++ ) {
				Chest chest = Main.chest[i];
				if( chest == null || ChestImplanter.IsChestEmpty(chest) ) { continue; }

				ChestImplanter.ApplyAllImplantsToChest( chest );

				progress.Value = (float)i / (float)Main.chest.Length;
			}
		}
	}




	class ChestImplantsWorld : ModWorld {
		public override void ModifyWorldGenTasks( List<GenPass> tasks, ref float totalWeight ) {
			tasks.Add( new ChestStufferGenPass() );
		}
	}
}
