using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Starflower
{
    class StarflowerGlobalNPC : GlobalNPC
    {
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Dryad && NPC.downedSlimeKing)
            {
                shop.item[nextSlot].SetDefaults(ItemType<Items.StarflowerPlanterBox>());
                nextSlot++;
            }
        }
    }
}