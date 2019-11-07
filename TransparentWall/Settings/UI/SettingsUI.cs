using CustomUI.GameplaySettings;
using CustomUI.Settings;
using CustomUI.Utilities;
using UnityEngine;

namespace TransparentWall.Settings.UI
{
    public class SettingsUI : MonoBehaviour
    {
        private static ToggleOption hmdToggle;
        private static ToggleOption livCameraToggle;
        private static BoolViewController hmdController;
        private static BoolViewController livCameraController;

        private static readonly Sprite optionIcon = UIUtilities.LoadSpriteFromResources($"{Plugin.PluginName}.Resources.icon_playersettings.png");
        private static readonly string disclaimer = $"Enabling this option will deactivate high score submission until it's turned off again!";

        /// <summary>
        /// Adds an additional submenu in the "Settings" page
        /// </summary>
        public static void CreateSettingsMenu()
        {
            SubMenu subMenu = CustomUI.Settings.SettingsUI.CreateSubMenu(Plugin.PluginName);

            hmdController = subMenu.AddBool("Enable in headset\\VR", $"Default = Off.\nDisclaimer: {disclaimer}");
            hmdController.GetValue += delegate
            {
                return Configuration.InHeadset;
            };
            hmdController.SetValue += delegate (bool value)
            {
                ChangeTransparentWallState(value);
            };

            livCameraController = subMenu.AddBool("Disable in LIVCamera", "Default = Off");
            livCameraController.GetValue += delegate
            {
                return Configuration.DisabledInLivCamera;
            };
            livCameraController.SetValue += delegate (bool value)
            {
                ChangeLIVWallState(value);
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
            hmdToggle.GetValue = Configuration.InHeadset;
            hmdToggle.OnToggle += (bool value) =>
            {
                Configuration.InHeadset = value;
            };

            livCameraToggle = GameplaySettingsUI.CreateToggleOption(GameplaySettingsPanels.PlayerSettingsLeft, $"Disable in LIVCamera",
                Plugin.PluginName.ToLower(), "Default = Off.");
            livCameraToggle.GetValue = Configuration.DisabledInLivCamera;
            livCameraToggle.OnToggle += (bool value) =>
            {
                Configuration.DisabledInLivCamera = value;
            };
        }

        private static void ChangeTransparentWallState(bool state) => hmdToggle.SetToggleState(state);
        private static void ChangeLIVWallState(bool state) => livCameraToggle.SetToggleState(state);
    }
}
