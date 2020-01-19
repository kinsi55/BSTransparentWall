using BeatSaberMarkupLanguage.Attributes;

namespace TransparentWall.Settings.UI
{
    internal class MainSettings : PersistentSingleton<MainSettings>
    {
        [UIValue("enable-in-hmd")]
        public bool EnableForHeadset
        {
            get => Configuration.EnableForHeadset;
            set => Configuration.EnableForHeadset = value;
        }

        [UIValue("disable-in-liv")]
        public bool DisableForLIVCamera
        {
            get => Configuration.DisableForLIVCamera;
            set => Configuration.DisableForLIVCamera = value;
        }
    }
}
