using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Starflower.Items {
    public class StarflowerSeeds : ModItem {

        public override void SetDefaults() {
            item.autoReuse = true;
            item.useTurn = true;
            item.useStyle = 1;
            item.useAnimation = 15;
            item.useTime = 10;
            item.maxStack = 99;
            item.consumable = true;
            item.width = 12;
            item.height = 14;
            item.value = 80;
            item.createTile = TileType<Tiles.Starflower>();
        }
    }
}
