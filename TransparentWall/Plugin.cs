using System;
using IPA;
using IPA.Config;
using IPA.Utilities;
using IPALogger = IPA.Logging.Logger;
using LogLevel = IPA.Logging.Logger.Level;
using Harmony;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TransparentWall
{
    class Plugin : IBeatSaberPlugin, IDisablablePlugin
    {
        public static string PluginName = "TransparentWall";
        public static bool isScoreDisabled = false;

        internal static Ref<PluginConfig> config;
        internal static IConfigProvider configProvider;

        private static HarmonyInstance Harmony;

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
            if (logger != null)
            {
                Logger.log = logger;
                Logger.Log("Logger prepared", LogLevel.Debug);
            }

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
            Harmony = HarmonyInstance.Create("com.pespiri.beatsaber.transparentwall");

            try
            {
                Harmony.PatchAll(System.Reflection.Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Logger.Log(ex, LogLevel.Error);
            }

            Logger.Log($"{Plugin.PluginName} has started", LogLevel.Notice);
        }

        public void OnApplicationQuit()
        {
            configProvider.Store(config.Value);

            try
            {
                Harmony.UnpatchAll("com.pespiri.beatsaber.transparentwall");
            }
            catch (Exception ex)
            {
                Logger.Log(ex, LogLevel.Error);
            }
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

        public void OnEnable() { }
        public void OnDisable()
        {
            if (Plugin.isScoreDisabled)
            {
                Logger.Log("Re-enabling ScoreSubmission on plugin disable", LogLevel.Debug);
                Plugin.isScoreDisabled = false;
                BS_Utils.Gameplay.ScoreSubmission.RemoveProlongedDisable(Plugin.PluginName);
            }
        }
    }
}
