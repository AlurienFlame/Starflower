using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Starflower.Items
{
    public class StarlightPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Emits a powerful aura of light\n3 minute duration");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 30;
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
            player.AddBuff(mod.BuffType("Starlight"), 10800);
            return true;
        }

        public override void AddRecipes()
        {
            // Topaz version
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddIngredient(ItemID.Blinkroot);
            recipe.AddIngredient(mod, "Starflower");
            recipe.AddIngredient(ItemID.Topaz);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
            // Amythest version
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddIngredient(ItemID.Blinkroot);
            recipe.AddIngredient(mod, "Starflower");
            recipe.AddIngredient(ItemID.Amethyst);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
