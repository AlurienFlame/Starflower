using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Starflower.Items
{
    public class StarflowerPlanterBox : ModItem
    {
        public static bool isCraftable;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Only starflowers can grow in this arcane soil\nIt's not a bug, it's lore");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 24;
            item.height = 20;
            item.value = 100;
            item.autoReuse = true;
            item.useTurn = true;
            item.useStyle = 1;
            item.useAnimation = 15;
            item.useTime = 10;
            item.consumable = true;
            item.createTile = mod.TileType("StarflowerPlanterBox");
        }

        public override void AddRecipes()
        {
            if (GetInstance<StarflowerServerConfig>().isPlanterBoxCraftingEnabled)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.DirtBlock, 2);
                recipe.AddRecipeGroup("Wood", 2);
                recipe.AddIngredient(mod, "StarflowerSeeds");
                recipe.AddTile(TileID.WorkBenches);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}
