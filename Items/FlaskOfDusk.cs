using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Starflower.Items
{
    public class FlaskOfDusk : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Melee attacks put your foes to sleep\n10 minute duration");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 26;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.useAnimation = 17;
            item.useTime = 17;
            item.useTurn = true;
            item.UseSound = SoundID.Item3;
            item.maxStack = 30;
            item.consumable = true;
            item.rare = 1;
            item.value = 80;
        }

        public override bool UseItem(Player player)
        {
            player.AddBuff(BuffType<Buffs.Dusk>(), 36000);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddIngredient(ItemID.Daybloom);
            recipe.AddIngredient(ItemID.Moonglow);
            recipe.AddIngredient(mod, "Starflower");
            recipe.AddTile(TileID.ImbuingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}