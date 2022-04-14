using Terraria;
using Terraria.ModLoader;

namespace Starflower.Buffs
{
    public class ShootingStar : ModBuff
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shooting Star");
            Description.SetDefault("20% increased bullet damage");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.bulletDamage *= 1.2f;
        }
    }
}
