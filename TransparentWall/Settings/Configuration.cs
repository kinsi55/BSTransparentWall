using IPA.Config;
using IPA.Utilities;
using TransparentWall.Settings.Utilities;

namespace TransparentWall.Settings
{
    public class Configuration
    {
        private static bool isInit = false;
        private static Ref<PluginConfig> config;
        private static IConfigProvider configProvider;

        public static bool InHeadset { get; internal set; }
        public static bool DisabledInLivCamera { get; internal set; }

        // Culling layers
        public static int WallLayerMask => 25;
        public static int MoveBackLayer { get; internal set; } = 27;

        internal static void Init(IConfigProvider cfgProvider)
        {
            if (!isInit && cfgProvider != null)
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

                isInit = true;
            }
        }

        internal static void Load()
        {
            if (isInit)
            {
                InHeadset = config.Value.HMD;
                DisabledInLivCamera = config.Value.DisableInLIVCamera;
            }
        }

        internal static void Save()
        {
            if (isInit)
            {
                config.Value.HMD = InHeadset;
                config.Value.DisableInLIVCamera = DisabledInLivCamera;

                configProvider.Store(config.Value);
            }
        }
    }
}
