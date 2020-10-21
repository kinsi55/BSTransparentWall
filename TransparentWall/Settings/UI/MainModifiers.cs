using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;

namespace TransparentWall.Settings.UI
{
    class MainModifiers : NotifiableSingleton<MainModifiers>
    {
        [UIValue("enable-headset")]
        public bool IsEnableForHeadset
        {
            get => Configuration.EnableForHeadset;
            set => Configuration.EnableForHeadset = value;
        }

        [UIValue("disable-liv")]
        public bool IsDisableForLIV
        {
            get => Configuration.DisableForLIVCamera;
            set => Configuration.DisableForLIVCamera = value;
        }

        [UIAction("trigger-headset-toggle")]
        public void TriggerHeadsetEnable(bool val) => IsEnableForHeadset = val;

        [UIAction("trigger-liv-toggle")]
        public void TriggerLIVDisable(bool val) => IsDisableForLIV = val;
    }
}
