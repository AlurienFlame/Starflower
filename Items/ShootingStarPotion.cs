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
            player.AddBuff(mod.BuffType("ShootingStar"), 18000);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddIngredient(ItemID.Shiverthorn);
            recipe.AddIngredient(mod, "Starflower");
            recipe.AddIngredient(ItemID.FallenStar);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
