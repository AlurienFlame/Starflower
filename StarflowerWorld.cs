using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.World.Generation;
using static Terraria.ModLoader.ModContent;

namespace Starflower
{
    public class StarflowerWorld : ModWorld
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            // Generate at some arbitrary point after sky islands but before trees, because otherwise trees break for some reason.
            // Right after silt just happens to be the arbitrary point I chose, because I couldn't guess the sky island ID.
            // Tree breaking is fixed by checking if a starflower can be placed before trying to place it.
            int genIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Silt"));
            tasks.Insert(genIndex + 1, new PassLegacy("Planting Starflower", GenerateStarflower));
        }

        private void GenerateStarflower(GenerationProgress progress)
        {
            progress.Message = "Planting Starflower";

            for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 1E-06); k++)
            {
                GenerateOneStarflower(skyOnly: true);
            }
        }

        // Called every frame.
        public override void PostUpdate()
        {
            // Complex math copied from source code. It's weirdly specific, but whatever, it works.
            // Value from 151 to 422.8 representing world size
            int starflowerGenerateChance = (int)MathHelper.Lerp(151, (float)151 * 2.8f, MathHelper.Clamp((float)Main.maxTilesX / 4200f - 1f, 0f, 1f));
            // Value from 151.2 to 604.8 also representing world size.
            float numTilesToUpdate = (float)(Main.maxTilesX * Main.maxTilesY) * 3E-05f * (float)Main.worldRate; // worldRate defaults to 1.
            for (int i = 0; (float)i < numTilesToUpdate; i++)
            {
                // Very low chance
                if (Main.rand.Next(500) == 0 && Main.rand.Next(starflowerGenerateChance) == 0)
                {
                    GenerateOneStarflower(skyOnly: false);
                }
            }
        }

        private void GenerateOneStarflower(bool skyOnly)
        {
            int yLimit;
            if (skyOnly)
            {
                yLimit = (int)WorldGen.worldSurfaceLow;
            }
            else
            {
                yLimit = Main.maxTilesY - 20;
            }
            int attempts = 0;
            int starflowerID = TileType<Tiles.Starflower>();
            Tile tile;
            Tile tileBelow;
            bool placeSuccessful = false;
            while (!placeSuccessful)
            {
                attempts++;
                // Pick a location.
                int x = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
                int y = WorldGen.genRand.Next(20, yLimit);
                tile = Main.tile[x, y];
                tileBelow = Main.tile[x, y + 1];
                // Attempt to place tile.
                // Starflower cannot be placed on active tiles, or if the tile below is inactive, a half brick, sloped, or a planter box.
                if (!tile.active() && tileBelow.nactive() && !tileBelow.halfBrick() && tileBelow.slope() == 0 && tileBelow.type != TileID.PlanterBox && tileBelow.type != TileType<Tiles.StarflowerPlanterBox>())
                {
                    WorldGen.PlaceTile(x, y, starflowerID, true);
                    // Check if place was successful.
                    placeSuccessful = tile.active() && tile.type == starflowerID;
                }
                // Don't allow infinite loops.
                if (attempts >= 30000)
                {
                    return;
                }
            }
        }
    }
}
