using BeatSaberMarkupLanguage.Attributes;

namespace TransparentWall.Settings.UI
{
    public class MainSettings : PersistentSingleton<MainSettings>
    {
        [UIValue("enable-in-hmd")]
        public bool EnableInHMD
        {
            get => Configuration.InHeadset;
            set => Configuration.InHeadset = value;
        }

        [UIValue("disable-in-liv")]
        public bool DisabledInLiv
        {
            get => Configuration.DisabledInLivCamera;
            set => Configuration.DisabledInLivCamera = value;
        }
    }
}
