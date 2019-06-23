﻿using CustomUI.Settings;
using CustomUI.Utilities;
using CustomUI.MenuButton;
using CustomUI.GameplaySettings;
using LogLevel = IPA.Logging.Logger.Level;
using UnityEngine;

namespace TransparentWall
{
    public class InGameSettingsUI : MonoBehaviour
    {
        private static ToggleOption hmdToggle;
        private static ToggleOption livCameraToggle;
        private static BoolViewController hmdController;
        private static BoolViewController livCameraController;

        private static readonly Sprite optionIcon = UIUtilities.LoadSpriteFromResources($"{Plugin.PluginName}.Properties.icon_playersettings.png");
        private static readonly string disclaimer = $"Enabling '{Plugin.PluginName}' in the headset\\VR will deactivate ScoreSubmission until this option is turned off!";

        /// <summary>
        /// Adds an additional submenu in the "Settings" page
        /// </summary>
        public static void CreateSettingsMenu()
        {
            SubMenu subMenu = SettingsUI.CreateSubMenu(Plugin.PluginName);

            hmdController = subMenu.AddBool("Enable in headset\\VR", $"Default = Off.\nDisclaimer: {disclaimer}");
            hmdController.GetValue += delegate { return Plugin.IsHMDOn; };
            hmdController.SetValue += delegate (bool value)
            {
                ChangeTransparentWallState(value);
                Logger.Log($"'Enable in headset' (IsHMDOn) in the main settings is set to '{value}'", LogLevel.Debug);
            };

            livCameraController = subMenu.AddBool("Disable in LIVCamera", "Default = Off");
            livCameraController.GetValue += delegate { return Plugin.IsDisableInLIVCamera; };
            livCameraController.SetValue += delegate (bool value)
            {
                ChangeLIVWallState(value);
                Logger.Log($"'Disable in LIVCamera' (IsDisableLIVCameraWall) in the main settings is set to '{value}'", LogLevel.Debug);
            };
        }

        /// <summary>
        /// Adds a toggle option to the in-game "Gameplay Setup" window. It can be found in the left panel of the Player Settings
        /// </summary>
        public static void CreateGameplaySetupMenu()
        {
            ToggleOption transparentWallMenu = GameplaySettingsUI.CreateSubmenuOption(GameplaySettingsPanels.PlayerSettingsLeft, Plugin.PluginName, "MainMenu",
                Plugin.PluginName.ToLower(), $"{Plugin.PluginName} options", optionIcon);

            hmdToggle = GameplaySettingsUI.CreateToggleOption(GameplaySettingsPanels.PlayerSettingsLeft, $"Enable in headset\\VR",
                Plugin.PluginName.ToLower(), $"Default = Off.\nDisclaimer: {disclaimer}");
            hmdToggle.GetValue = Plugin.IsHMDOn;
            hmdToggle.OnToggle += (bool value) =>
            {
                Plugin.IsHMDOn = value;
                Logger.Log($"Toggle is very '{(value ? "toggled" : "untoggled")}! Value is now '{value}'", LogLevel.Debug);
            };

            livCameraToggle = GameplaySettingsUI.CreateToggleOption(GameplaySettingsPanels.PlayerSettingsLeft, $"Disable in LIVCamera",
                Plugin.PluginName.ToLower(), "Default = Off.");
            livCameraToggle.GetValue = Plugin.IsDisableInLIVCamera;
            livCameraToggle.OnToggle += (bool value) =>
            {
                Plugin.IsDisableInLIVCamera = value;
                Logger.Log($"Toggle is very '{(value ? "toggled" : "untoggled")}! Value is now '{value}'", LogLevel.Debug);
            };
        }

        /// <summary>
        /// Adds a button to the "Mods" settings menu (INCOMPLETE)
        /// </summary>
        public static void CreateModMenuButton()
        {
            //Currently there's no indication of which state is set when it's clicked. You can always check the toggle option in "Gameplay Setup" to be sure afterwards :P
            MenuButtonUI.AddButton($"{Plugin.PluginName} toggle", delegate
            {
                ChangeTransparentWallState(!Plugin.IsHMDOn);
                Logger.Log($"Button was brutally smashed! Value should now be '{Plugin.IsHMDOn}'", LogLevel.Debug);
            });
        }

        private static void ChangeTransparentWallState(bool state) => hmdToggle.SetToggleState(state);
        private static void ChangeLIVWallState(bool state) => livCameraToggle.SetToggleState(state);
    }
}
