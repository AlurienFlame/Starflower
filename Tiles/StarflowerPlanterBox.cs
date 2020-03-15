using Terraria;
using Terraria.ModLoader;
// TODO: Herbs should not be possible to cut with weapon swings when on this.
// TODO: Make weeds grow on this.
// TODO: Make smart cursor treat this like a planter box.
// TODO: Allow vanilla herbs to be placed on this.

namespace Starflower.Tiles
{
    public class StarflowerPlanterBox : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileSpelunker[Type] = true;
            drop = mod.ItemType("StarflowerPlanterBox");
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
            bool isLeftSame = left.type == tile.type;
            bool isRightSame = right.type == tile.type;
            if (isLeftSame && isRightSame)
            {
                tile.frameX = 18;
            }
            else if (isLeftSame && !isRightSame)
            {
                tile.frameX = 36;
            }
            else if (!isLeftSame && isRightSame)
            {
                tile.frameX = 0;
            }
            else
            {
                tile.frameX = 54;
            }
            return true;
        }
    }
}
