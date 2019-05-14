using IPA;
using IPA.Config;
using IPA.Utilities;
using IPALogger = IPA.Logging.Logger;
using LogLevel = IPA.Logging.Logger.Level;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TransparentWall
{
    class Plugin : IBeatSaberPlugin
    {
        public static string PluginName = "TransparentWall";
        public static bool isScoreDisabled = false;

        internal static Ref<PluginConfig> config;
        internal static IConfigProvider configProvider;

        public static bool IsAnythingOn => (Plugin.IsHMDOn || Plugin.IsDisableInLIVCamera);

        public static bool IsHMDOn
        {
            get => config.Value.HMD;
            set => config.Value.HMD = value;
        }

        public static bool IsDisableInLIVCamera
        {
            get => config.Value.DisableInLIVCamera;
            set => config.Value.DisableInLIVCamera = value;
        }

        public void Init(IPALogger logger, [Config.Prefer("json")] IConfigProvider cfgProvider)
        {
            Logger.log = logger;
            Logger.Log("Logger prepared", LogLevel.Debug);

            configProvider = cfgProvider;
            config = cfgProvider.MakeLink<PluginConfig>((p, v) =>
            {
                if (v.Value == null || v.Value.RegenerateConfig || v.Value == null && v.Value.RegenerateConfig)
                {
                    p.Store(v.Value = new PluginConfig() { RegenerateConfig = false });
                }
                config = v;
            });
            Logger.Log("Configuration loaded", LogLevel.Debug);
        }

        public void OnApplicationStart()
        {
            Logger.Log($"{Plugin.PluginName} has started", LogLevel.Notice);
        }

        public void OnApplicationQuit()
        {
            configProvider.Store(config.Value);
        }

        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
        {
            if (nextScene.name == "GameCore")
            {
                new GameObject(Plugin.PluginName).AddComponent<TransparentWall>();
            }
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            if (scene.name == "MenuCore")
            {
                InGameSettingsUI.CreateGameplaySetupMenu();
                InGameSettingsUI.CreateSettingsMenu();
                //InGameSettingsUI.CreateModMenuButton()
            }
        }

        public void OnSceneUnloaded(Scene scene) { }
        public void OnUpdate() { }
        public void OnFixedUpdate() { }
    }
}
