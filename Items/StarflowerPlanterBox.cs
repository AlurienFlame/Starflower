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
            Item.maxStack = 999;
            Item.width = 24;
            Item.height = 20;
            Item.value = 100;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.consumable = true;
            Item.createTile = TileType<Tiles.StarflowerPlanterBox>();
        }

        public override void AddRecipes()
        {
            if (GetInstance<StarflowerServerConfig>().isPlanterBoxCraftingEnabled)
            {
                GetInstance<StarflowerPlanterBox>().CreateRecipe()
                    .AddIngredient(ItemID.DirtBlock, 2)
                    .AddRecipeGroup("Wood", 2)
                    .AddIngredient<StarflowerSeeds>()
                    .AddTile(TileID.WorkBenches)
                    .Register();
            }
        }
    }
}
