using IPA.Config;
using IPA.Utilities;
using TransparentWall.Settings.Utilities;

namespace TransparentWall.Settings
{
    public class Configuration
    {
        private static Ref<PluginConfig> config;
        private static IConfigProvider configProvider;

        public static bool EnableForHeadset { get; internal set; }
        public static bool DisableForLIVCamera { get; internal set; }

        // Culling layers
        public static int WallLayerMask => 25;
        public static int MoveBackLayer { get; internal set; } = 27;

        internal static void Init(IConfigProvider cfgProvider)
        {
            configProvider = cfgProvider;
            config = cfgProvider.MakeLink<PluginConfig>((p, v) =>
            {
                if (v.Value == null || v.Value.RegenerateConfig)
                {
                    p.Store(v.Value = new PluginConfig() { RegenerateConfig = false });
                }
                config = v;
            });
        }

        internal static void Load()
        {
            EnableForHeadset = config.Value.EnableForHeadset;
            DisableForLIVCamera = config.Value.DisableForLIVCamera;
        }

        internal static void Save()
        {
            config.Value.EnableForHeadset = EnableForHeadset;
            config.Value.DisableForLIVCamera = DisableForLIVCamera;

            configProvider.Store(config.Value);
        }
    }
}
