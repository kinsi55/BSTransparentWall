using IPA;
using IPA.Config;
using IPA.Loader;
using IPA.Utilities;
using TransparentWall.Gameplay;
using TransparentWall.HarmonyPatches;
using TransparentWall.Settings;
using TransparentWall.Settings.UI;
using TransparentWall.Settings.Utilities;
using TransparentWall.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;
using LogLevel = IPA.Logging.Logger.Level;

namespace TransparentWall
{
    class Plugin : IBeatSaberPlugin, IDisablablePlugin
    {
        public static string PluginName => "TransparentWall";
        public static SemVer.Version PluginVersion { get; private set; } = new SemVer.Version("0.0.0"); // Default

        internal static Ref<PluginConfig> config;
        internal static IConfigProvider configProvider;

        public void Init(IPALogger logger, [Config.Prefer("json")] IConfigProvider cfgProvider, PluginLoader.PluginMetadata metadata)
        {
            Logger.log = logger;

            configProvider = cfgProvider;
            config = cfgProvider.MakeLink<PluginConfig>((p, v) =>
            {
                if (v.Value == null || v.Value.RegenerateConfig || v.Value == null && v.Value.RegenerateConfig)
                {
                    p.Store(v.Value = new PluginConfig() { RegenerateConfig = false });
                }
                config = v;
            });

            if (metadata?.Version != null)
            {
                PluginVersion = metadata.Version;
            }
        }

        public void OnApplicationQuit() => Unload();
        public void OnEnable() => Load();
        public void OnDisable() => Unload();

        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
        {
            if (nextScene.name == "GameCore")
            {
                new GameObject(PluginName).AddComponent<TransparentWalls>();
            }
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            if (scene.name == "MenuCore")
            {
                SettingsUI.CreateGameplaySetupMenu();
                SettingsUI.CreateSettingsMenu();
            }
        }

        public void OnApplicationStart() { }
        public void OnSceneUnloaded(Scene scene) { }
        public void OnUpdate() { }
        public void OnFixedUpdate() { }

        private void Load()
        {
            Configuration.Load();
            TransparentWallPatches.ApplyHarmonyPatches();

            Logger.Log($"{PluginName} v.{PluginVersion} has started.", LogLevel.Info);
        }

        private void Unload()
        {
            TransparentWallPatches.RemoveHarmonyPatches();
            ScoreUtility.Cleanup();
            Configuration.Save();
        }
    }
}
