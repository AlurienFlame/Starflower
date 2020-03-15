using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace Starflower
{
    class StarflowerPlayer : ModPlayer
    {
        public bool hasDusk;

        public override void ResetEffects()
        {
            hasDusk = false;
        }

        // Moved into it's own method so I don't have to repeat code.
        private void ApplyNightfall(NPC target)
        {
            // Nightfall will not apply to bosses.
            if (!target.boss)
            {
                target.AddBuff(BuffType<Buffs.Nightfall>(), 60 * Main.rand.Next(5, 10), false);
            }
        }

        // Melee weapons.
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (hasDusk)
            {
                ApplyNightfall(target);
            }
        }

        // Ranged weapons with melee damage type.
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if ((proj.melee) && hasDusk && !proj.noEnchantments)
            {
                ApplyNightfall(target);
            }
        }
    }
}