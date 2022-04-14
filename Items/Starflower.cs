using Terraria;
using Terraria.ModLoader;
// TODO: Should drop from herb bags

namespace Starflower.Items {
    public class Starflower : ModItem {

        public override void SetDefaults() {
            Item.maxStack = 999;
            Item.width = 14;
            Item.height = 18;
            Item.value = 80;
        }
    }
}
