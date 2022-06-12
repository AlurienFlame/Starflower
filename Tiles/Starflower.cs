using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Starflower.Items;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace Starflower.Tiles
{
    public enum PlantStage : byte
    {
        Planted,
        Growing,
        Grown
    }

    public class Starflower : ModTile
    {
        private const int FrameWidth = 15; // A constant for readability and to kick out those magic numbers
        public override void SetStaticDefaults()
        {
            // Flags
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            Main.tileCut[Type] = true;
            Main.tileNoFail[Type] = true;

            // Groups
            // FIXME: should not be possible to replace with block on if on planter box
            TileID.Sets.ReplaceTileBreakUp[Type] = true;
            TileID.Sets.IgnoredInHouseScore[Type] = true;
            TileID.Sets.IgnoredByGrowingSaplings[Type] = true;

            // Show on map
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Example Herb");
            AddMapEntry(new Color(111, 149, 217), name);

            // Anchors
            TileObjectData.newTile.CopyFrom(TileObjectData.StyleAlch);
            TileObjectData.newTile.AnchorValidTiles = new int[] {
                TileID.Cloud,
                TileID.RainCloud
            };
            TileObjectData.newTile.AnchorAlternateTiles = new int[] {
                TileID.ClayPot,
                TileID.PlanterBox,
                TileType<StarflowerPlanterBox>()
            };
            TileObjectData.addTile(Type);

            // Sound
            HitSound = SoundID.Grass;
        }

        // Directly copied from example mod
        public override bool CanPlace(int i, int j)
        {
            Tile tile = Framing.GetTileSafely(i, j); // Safe way of getting a tile instance

            if (tile.HasTile)
            {
                int tileType = tile.TileType;
                if (tileType == Type)
                {
                    PlantStage stage = GetStage(i, j); // The current stage of the herb

                    // Can only place on the same herb again if it's grown already
                    return stage == PlantStage.Grown;
                }
                else
                {
                    // Support for vanilla herbs/grasses:
                    if (Main.tileCut[tileType] || TileID.Sets.BreakableWhenPlacing[tileType] || tileType == TileID.WaterDrip || tileType == TileID.LavaDrip || tileType == TileID.HoneyDrip || tileType == TileID.SandDrip)
                    {
                        bool foliageGrass = tileType == TileID.Plants || tileType == TileID.Plants2;
                        bool moddedFoliage = tileType >= TileID.Count && (Main.tileCut[tileType] || TileID.Sets.BreakableWhenPlacing[tileType]);
                        bool harvestableVanillaHerb = Main.tileAlch[tileType] && WorldGen.IsHarvestableHerbWithSeed(tileType, tile.TileFrameX / 18);

                        if (foliageGrass || moddedFoliage || harvestableVanillaHerb)
                        {
                            WorldGen.KillTile(i, j);
                            if (!tile.HasTile && Main.netMode == NetmodeID.MultiplayerClient)
                            {
                                NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, i, j);
                            }

                            return true;
                        }
                    }

                    return false;
                }
            }

            return true;
        }

        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
        {
            if (i % 2 == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
        }

        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
        {
            offsetY = -2; // This is -1 for tiles using StyleAlch, but vanilla sets to -2 for herbs, which causes a slight visual offset between the placement preview and the placed tile. 
        }

        public override bool Drop(int x, int y)
        {
            PlantStage stage = GetStage(x, y);

            if (stage == PlantStage.Planted) return false;

            Vector2 worldPosition = new Vector2(x, y).ToWorldCoordinates();
            Player nearestPlayer = Main.player[Player.FindClosest(worldPosition, 16, 16)];

            int herbsToDrop = 1;
            int seedsToDrop = 1;

            if (nearestPlayer.active && nearestPlayer.HeldItem.type == ItemID.StaffofRegrowth)
            {
                // Increased yields with Staff of Regrowth, even when not fully grown
                herbsToDrop = Main.rand.Next(1, 3);
                seedsToDrop = Main.rand.Next(1, 6);
            }
            else if (stage == PlantStage.Grown)
            {
                // Default yields, only when fully grown
                herbsToDrop = 1;
                seedsToDrop = Main.rand.Next(1, 4);
            }

            var source = new EntitySource_TileBreak(x, y);

            if (herbsToDrop > 0)
                Item.NewItem(source, worldPosition, ItemType<Items.Starflower>(), herbsToDrop);

            if (seedsToDrop > 0)
                Item.NewItem(source, worldPosition, ItemType<Items.StarflowerSeeds>(), seedsToDrop);

            // Custom drop code, so return false
            return false;
        }

        public override bool IsTileSpelunkable(int i, int j)
        {
            PlantStage stage = GetStage(i, j);

            // Only glow if the herb is grown
            return stage == PlantStage.Grown;
        }

        public override void RandomUpdate(int x, int y)
        {
            Tile tile = Framing.GetTileSafely(x, y);
            PlantStage stage = GetStage(x, y);

            // Only grow to the next stage if there is a next stage. We don't want our tile turning pink!
            if (stage == PlantStage.Grown) return;

            // Increase the x frame to change the stage
            tile.TileFrameX += FrameWidth;

            // If in multiplayer, sync the frame change
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                NetMessage.SendTileSquare(-1, x, y, 1);
            }

        }

        // A helper method to quickly get the current stage of the herb (assuming the tile at the coordinates is our herb)
        private static PlantStage GetStage(int i, int j)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            return (PlantStage)(tile.TileFrameX / FrameWidth);
        }
    }
}
