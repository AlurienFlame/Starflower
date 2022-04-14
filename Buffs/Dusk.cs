using Terraria;
using Terraria.ModLoader;

namespace Starflower.Buffs
{
    class Dusk : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Weapon Imbue: Dusk");
            Description.SetDefault("Melee attacks put your foes to sleep");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<StarflowerPlayer>().hasDusk = true;
        }
    }
}
