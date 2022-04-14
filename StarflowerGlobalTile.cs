using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace Starflower
{
    class StarflowerGlobalTile : GlobalTile
    {
        public override bool CanPlace(int i, int j, int type)
        {
            if (type == TileID.ImmatureHerbs)
            {
                if (Main.tile[i, j].TileType == TileType<Tiles.Starflower>())
                {
                    return false;
                }
            }
            return base.CanPlace(i, j, type);
        }
    }
}