using IPA.Config;
using IPA.Config.Stores;
using TransparentWall.Settings.Utilities;

namespace TransparentWall.Settings
{
    public static class Configuration
    {
        public static bool EnableForHeadset { get; internal set; }
        public static bool DisableForLivCamera { get; internal set; }

        // Culling layers
        public static int WallLayerMask => 25;
        public static int MoveBackLayer { get; internal set; } = 27;

        internal static void Init(Config config)
        {
            PluginConfig.Instance = config.Generated<PluginConfig>();
        }

        internal static void Load()
        {
            EnableForHeadset = PluginConfig.Instance.EnableForHeadset;
            DisableForLivCamera = PluginConfig.Instance.DisableForLivCamera;
        }

        internal static void Save()
        {
            PluginConfig.Instance.EnableForHeadset = EnableForHeadset;
            PluginConfig.Instance.DisableForLivCamera = DisableForLivCamera;
        }
    }
}
