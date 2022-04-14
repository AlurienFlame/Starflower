using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace Starflower.Items {
    public class StarflowerSeeds : ModItem {

        public override void SetDefaults() {
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 12;
            Item.height = 14;
            Item.value = 80;
            Item.createTile = TileType<Tiles.Starflower>();
        }
    }
}
