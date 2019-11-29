using IPA.Config;
using IPA.Utilities;
using TransparentWall.Settings.Utilities;
using LogLevel = IPA.Logging.Logger.Level;

namespace TransparentWall.Settings
{
    internal class Configuration
    {
        private static bool isInit = false;
        private static Ref<PluginConfig> config;
        private static IConfigProvider configProvider;

        internal static bool InHeadset;
        internal static bool DisabledInLivCamera;
        internal static bool ShowCallSource;

        internal static void Init(IConfigProvider cfgProvider)
        {
            if (!isInit && cfgProvider != null)
            {
                configProvider = cfgProvider;
                config = cfgProvider.MakeLink<PluginConfig>((p, v) =>
                {
                    if (v.Value == null || v.Value.RegenerateConfig || v.Value == null && v.Value.RegenerateConfig)
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

                if (config.Value.Logging["ShowCallSource"] is bool loggerShowCallSource)
                {
                    ShowCallSource = loggerShowCallSource;
                }

                Logger.Log("Configuration has been loaded.", LogLevel.Debug);
            }
        }

        internal static void Save()
        {
            if (isInit)
            {
                config.Value.HMD = InHeadset;
                config.Value.DisableInLIVCamera = DisabledInLivCamera;
                config.Value.Logging["ShowCallSource"] = ShowCallSource;

                configProvider.Store(config.Value);
            }
        }
    }
}
