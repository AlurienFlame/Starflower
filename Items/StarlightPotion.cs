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
            Item.width = 16;
            Item.height = 30;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = 80;
            Item.buffType = ModContent.BuffType<Buffs.Starlight>();
            Item.buffTime = 10800;
        }

        public override void AddRecipes()
        {
            // Topaz version
            ModContent.GetInstance<StarlightPotion>().CreateRecipe()
                .AddIngredient(ItemID.BottledWater)
                .AddIngredient(ItemID.Blinkroot)
                .AddIngredient<Starflower>()
                .AddIngredient(ItemID.Topaz)
                .AddTile(TileID.Bottles)
                .Register();
            // Amythest version
            ModContent.GetInstance<StarlightPotion>().CreateRecipe()
                .AddIngredient(ItemID.BottledWater)
                .AddIngredient(ItemID.Blinkroot)
                .AddIngredient<Starflower>()
                .AddIngredient(ItemID.Amethyst)
                .AddTile(TileID.Bottles)
                .Register();
        }

    }
}
