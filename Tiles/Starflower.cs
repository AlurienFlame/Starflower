using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace Starflower.Tiles
{
    public class Starflower : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileCut[Type] = true;
            Main.tileNoFail[Type] = true;
            soundType = 6;
            TileObjectData.newTile.CopyFrom(TileObjectData.StyleAlch);
            TileObjectData.newTile.AnchorValidTiles = new int[] {
                TileID.Cloud,
                TileID.RainCloud
            };
            TileObjectData.newTile.AnchorAlternateTiles = new int[] {
                TileID.ClayPot,
                TileID.PlanterBox,
                TileType<Tiles.StarflowerPlanterBox>()
            };
            TileObjectData.addTile(Type);
        }

        public override bool CanPlace(int i, int j)
        {
            // Starflower cannot be placed on herbs or itself.
            if (Main.tileAlch[Main.tile[i, j].type] || Main.tile[i, j].type == Type)
            {
                return false;
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

        public override bool Drop(int i, int j)
        {
            int growthStage = Main.tile[i, j].frameX / 15;
            if (growthStage > 0)
            {
                // I thought this was a workaround but it turns out the source code does it this way too. Ech.
                if (Main.player[Player.FindClosest(new Microsoft.Xna.Framework.Vector2(i * 16, j * 16), 0, 0)].HeldItem.netID == ItemID.StaffofRegrowth)
                {
                    Item.NewItem(i * 16, j * 16, 0, 0, mod.ItemType("StarflowerSeeds"), Main.rand.Next(1, 6));
                    Item.NewItem(i * 16, j * 16, 0, 0, mod.ItemType("Starflower"), Main.rand.Next(1, 3));
                }
                else if (growthStage == 2)
                {
                    Item.NewItem(i * 16, j * 16, 0, 0, mod.ItemType("StarflowerSeeds"), Main.rand.Next(1, 4));
                    Item.NewItem(i * 16, j * 16, 0, 0, mod.ItemType("Starflower"));
                }
            }
            // Tells game not to use default drop. Method won't work without a return statement.
            return false;
        }

        // Not sure why 15 is the number considering my textures are 14 pixels wide, and 18 is the number for 16 pixel wide textures.
        public override void RandomUpdate(int i, int j)
        {
            if (Main.tile[i, j].frameX == 0)
            {
                Main.tile[i, j].frameX += 15;
            }
            else if (Main.tile[i, j].frameX == 15)
            {
                Main.tile[i, j].frameX += 15;
            }
        }
    }
}
