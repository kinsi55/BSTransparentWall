using IPA;
using IPA.Config;
using IPA.Loader;
using TransparentWall.Gameplay;
using TransparentWall.HarmonyPatches;
using TransparentWall.Settings;
using TransparentWall.Settings.UI;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;

namespace TransparentWall
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        public static string PluginName => "TransparentWall";
        public static SemVer.Version PluginVersion { get; private set; } = new SemVer.Version("0.0.0"); // Default

        [Init]
        public void Init(IPALogger logger, Config config, PluginMetadata metadata)
        {
            Logger.log = logger;
            Configuration.Init(config);

            if (metadata?.Version != null)
            {
                PluginVersion = metadata.Version;
            }
        }

        [OnEnable]
        public void OnEnable() => Load();
        [OnDisable]
        public void OnDisable() => Unload();

        public void OnGameSceneLoaded()
        {
            new GameObject(PluginName).AddComponent<TransparentWalls>();
        }

        private void Load()
        {
            Configuration.Load();
            TransparentWallPatches.ApplyHarmonyPatches();
            SettingsUI.CreateMenu();
            AddEvents();

            Logger.log.Info($"{PluginName} v.{PluginVersion} has started.");
        }

        private void Unload()
        {
            RemoveEvents();
            TransparentWallPatches.RemoveHarmonyPatches();
            Configuration.Save();
            SettingsUI.RemoveMenu();
        }

        private void AddEvents()
        {
            RemoveEvents();
            BS_Utils.Utilities.BSEvents.gameSceneLoaded += OnGameSceneLoaded;
        }

        private void RemoveEvents()
        {
            BS_Utils.Utilities.BSEvents.gameSceneLoaded -= OnGameSceneLoaded;
        }
    }
}
