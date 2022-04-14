using Terraria;
using Terraria.IO;
using Terraria.WorldBuilding;

namespace Starflower
{
    public class StarflowerGenPass : GenPass
    {
        public StarflowerGenPass(string name, float loadWeight) : base(name, loadWeight)
        {
        }

		protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
		{
			// TODO: Lang files?
			progress.Message = "Planting Starflower";
			int attempts = (int)(Main.maxTilesX * Main.maxTilesY * 6E-4);
			for (int i = 0; i < attempts; i++)
			{
				progress.Set(i / attempts);
				StarflowerSystem.PlantStarflower();
			}
		}
	}
}