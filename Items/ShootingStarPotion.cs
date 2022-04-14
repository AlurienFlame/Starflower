using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Starflower.Items
{
    public class ShootingStarPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("20% increased bullet damage\n5 minute duration");
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
            Item.buffType = ModContent.BuffType<Buffs.ShootingStar>();
            Item.buffTime = 18000;
        }

        public override void AddRecipes()
        {
            ModContent.GetInstance<StarlightPotion>().CreateRecipe()
                .AddIngredient(ItemID.BottledWater)
                .AddIngredient(ItemID.Shiverthorn)
                .AddIngredient<Starflower>()
                .AddIngredient(ItemID.FallenStar)
                .AddTile(TileID.Bottles)
                .Register();
        }

    }
}
