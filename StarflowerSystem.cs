using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using static Terraria.ModLoader.ModContent;

namespace Starflower
{
    public class StarflowerSystem : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            // Generate at some arbitrary point after sky islands but before trees, because otherwise trees break for some reason.
            // Right after silt just happens to be the arbitrary point I chose, because I couldn't guess the sky island ID.
            // Tree breaking is fixed by checking if a starflower can be placed before trying to place it.
            int genIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Herbs"));
            if (genIndex != -1)
                tasks.Insert(genIndex + 1, new StarflowerGenPass("Planting Starflower", 237.4298f)); // TODO: identify 237.4298f significance
        }

        public override void PostUpdateWorld()
        {
            // Weirdly complex math copied from source code
            float tries = (float)(Main.maxTilesX * Main.maxTilesY * WorldGen.GetWorldUpdateRate() * 3E-05f);
            int scale = 151;
            int chance = (int)MathHelper.Lerp(scale, (float)scale * 2.8f, MathHelper.Clamp((float)Main.maxTilesX / 4200f - 1f, 0f, 1f));
            if (Main.rand.Next(chance * 100 / (int)tries) == 0)
            {
                PlantStarflower();
            }
        }

        public static void PlantStarflower()
        {
            // Pick a random location.
            int x = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
            int y = WorldGen.genRand.Next(20, Main.maxTilesY - 20);
            // Note: y is the tile to plant ON, not the tile our herb will appear in

            // Keep moving till you find a tile or hit the ground
            while (y < Main.maxTilesY - 20 && !Main.tile[x, y].HasTile)
            {
                y++;
            }

            // Check if herb can be planted at location
            if (Main.tile[x, y - 1].HasTile || !Main.tile[x, y].HasUnactuatedTile || Main.tile[x, y].IsHalfBlock || !(Main.tile[x, y].Slope == 0))
            {
                return;
            }

            // Check if tile is valid
            if (Main.tile[x, y].TileType != TileID.Cloud && Main.tile[x, y].TileType != TileID.RainCloud)
            {
                // TODO: see if you can automatically loop through it's valid tiles instead of hard setting them both here and in the tile class
                return;
            }

            if (Main.tile[x, y - 1].LiquidType > 0)
            {
                return;
            }

            // Count nearby herbs
            int offset = 15;
            int alchMin = 5;
            int alchCount = 0;
            offset = (int)((float)offset * ((float)Main.maxTilesX / 4200f));
            int leftPart = Utils.Clamp(x - offset, 4, Main.maxTilesX - 4);
            int rightPart = Utils.Clamp(x + offset, 4, Main.maxTilesX - 4);
            int topPart = Utils.Clamp(y - offset, 4, Main.maxTilesY - 4);
            int botPart = Utils.Clamp(y + offset, 4, Main.maxTilesY - 4);
            for (int i = leftPart; i <= rightPart; i++)
            {
                for (int j = topPart; j <= botPart; j++)
                {
                    if (Main.tileAlch[Main.tile[i, j].TileType])
                        alchCount++;
                }
            }

            // Few enough nearby herbs
            if (alchCount >= alchMin)
            {
                return;
            }

            WorldGen.PlaceTile(x, y-1, TileType<Tiles.Starflower>(), mute: true);
            //if (Main.tile[x, y - 1].TileType == TileType<Tiles.Starflower>())
            //{
            //    Logging.PublicLogger.Debug($"Successfully planted new starflower at {x}, {y - 1}");
            //}
            //else
            //{
            //    Logging.PublicLogger.Debug($"Failed to plant new starflower at {x}, {y - 1}");
            //}

            // Not sure if this is necessary, copied it from vanilla code.
            if (Main.tile[x, y - 1].HasTile && Main.netMode == NetmodeID.Server)
                NetMessage.SendTileSquare(-1, x, y - 1);
        }
    }
}
