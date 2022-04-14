using Terraria;
using Terraria.ModLoader;
// TODO: Herbs should not be possible to cut with weapon swings when on this.
// TODO: Should grow weeds.
// TODO: Make smart cursor treat this like a planter box.
// TODO: Allow vanilla herbs to be placed on this.

namespace Starflower.Tiles
{
    public class StarflowerPlanterBox : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileSpelunker[Type] = true;
            ItemDrop = ModContent.ItemType<Items.StarflowerPlanterBox>();
        }

        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            Tile tile = Main.tile[i, j];
            Tile left = Main.tile[i - 1, j];
            Tile right = Main.tile[i + 1, j];
            if (tile == null || left == null || right == null)
            {
                return false;
            }
            bool isLeftSame = left.TileType == tile.TileType;
            bool isRightSame = right.TileType == tile.TileType;
            if (isLeftSame && isRightSame)
            {
                tile.TileFrameX = 18;
            }
            else if (isLeftSame && !isRightSame)
            {
                tile.TileFrameX = 36;
            }
            else if (!isLeftSame && isRightSame)
            {
                tile.TileFrameX = 0;
            }
            else
            {
                tile.TileFrameX = 54;
            }
            return true;
        }
    }
}
