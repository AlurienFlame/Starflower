using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
            Item.width = 16;
            Item.height = 26;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = 80;
            Item.buffType = ModContent.BuffType<Buffs.Dusk>();
            Item.buffTime = 36000;
        }

        public override void AddRecipes()
        {
            ModContent.GetInstance<StarlightPotion>().CreateRecipe()
                .AddIngredient(ItemID.BottledWater)
                .AddIngredient(ItemID.Daybloom)
                .AddIngredient(ItemID.Moonglow)
                .AddIngredient<Starflower>()
                .AddTile(TileID.ImbuingStation)
                .Register();
        }
    }
}