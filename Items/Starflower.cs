using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Starflower.Items {
    public class Starflower : ModItem {

        public override void SetDefaults() {
            item.maxStack = 99;
            item.width = 14;
            item.height = 18;
            item.value = 80;
        }
    }
}
