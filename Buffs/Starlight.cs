using Terraria;
using Terraria.ModLoader;

namespace Starflower.Buffs
{
    public class Starlight : ModBuff
    {

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Starlight");
            Description.SetDefault("Twinkle twinkle little star.");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // Xpos, Ypos, Red, Green, Blue. Brightness corresponds to size of RGB numbers.
            Lighting.AddLight((int)(player.position.X + (float)(player.width / 2)) / 16, (int)(player.position.Y + (float)(player.height / 2)) / 16, 5f, 5f, 2.5f);
        }
    }
}
