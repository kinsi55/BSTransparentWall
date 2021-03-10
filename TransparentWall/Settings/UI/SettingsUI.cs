using BeatSaberMarkupLanguage.GameplaySetup;
using BeatSaberMarkupLanguage.Settings;

namespace TransparentWall.Settings.UI
{
    internal class SettingsUI
    {
        private static bool _created;

        public static void CreateMenu()
        {
            if (_created) return;

            BSMLSettings.instance.AddSettingsMenu("Transparent Walls", "TransparentWall.Settings.UI.Views.mainsettings.bsml", MainSettings.instance);
            GameplaySetup.instance.AddTab("Transparent Walls", "TransparentWall.Settings.UI.Views.mainmodifiers.bsml", MainModifiers.instance);
            _created = true;
        }

        public static void RemoveMenu()
        {
            if (!_created) return;

            BSMLSettings.instance.RemoveSettingsMenu(MainSettings.instance);
            GameplaySetup.instance.RemoveTab("Transparent Walls");
            _created = false;
        }
    }
}
