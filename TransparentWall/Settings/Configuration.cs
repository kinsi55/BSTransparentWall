using LogLevel = IPA.Logging.Logger.Level;

namespace TransparentWall.Settings
{
    internal class Configuration
    {
        internal static bool InHeadset;
        internal static bool DisabledInLivCamera;
        internal static bool ShowCallSource;

        internal static void Load()
        {
            InHeadset = Plugin.config.Value.HMD;
            DisabledInLivCamera = Plugin.config.Value.DisableInLIVCamera;

            if (Plugin.config.Value.Logging["ShowCallSource"] is bool loggerShowCallSource)
            {
                ShowCallSource = loggerShowCallSource;
            }

            Logger.Log("Configuration has been loaded.", LogLevel.Debug);
        }

        internal static void Save()
        {
            Plugin.config.Value.HMD = InHeadset;
            Plugin.config.Value.DisableInLIVCamera = DisabledInLivCamera;
            Plugin.config.Value.Logging["ShowCallSource"] = ShowCallSource;

            Plugin.configProvider.Store(Plugin.config.Value);
        }
    }
}
