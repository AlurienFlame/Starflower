using Terraria.ModLoader;
using Terraria;

namespace Starflower.Buffs
{
    class Nightfall : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Nightfall");
            Description.SetDefault("It's your bedtime.");
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            // Move at 1/2 speed.
            npc.velocity /= 2;
        }
    }
}