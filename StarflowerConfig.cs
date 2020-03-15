using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Starflower
{
    class StarflowerServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("Craftable starflower planter boxes")]
        [Tooltip("Provides an alternative way of getting starflower planter boxes,\nin case there's no room for the item in the dryad's shop because you have so many mods installed.\nRequires a reload to take effect.")]
        [DefaultValue(false)]
        [ReloadRequired]
        public bool isPlanterBoxCraftingEnabled;
    }
}
