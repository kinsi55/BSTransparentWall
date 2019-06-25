using System;
using System.Linq;
using System.Collections.Generic;
using BS_Utils.Gameplay;
using LogLevel = IPA.Logging.Logger.Level;
using UnityEngine;

namespace TransparentWall
{
    public class TransparentWall : MonoBehaviour
    {
        public static int MoveBackLayer = 27;
        public static List<int> LayersToMask = new List<int> { TransparentWallsPatch.WallLayerMask, MoveBackLayer };
        public static List<string> livNames = new List<string> { "MenuMainCamera", "MainCamera", "LIV Camera" };

        private void Start()
        {
            if (!Plugin.IsAnythingOn)
            {
                Logger.Log("Nothing is turned on!", LogLevel.Debug);
                EnableScore();
                return;
            }

            try
            {
                if (Resources.FindObjectsOfTypeAll<MoveBackWall>().Count() > 0)
                {
                    MoveBackLayer = Resources.FindObjectsOfTypeAll<MoveBackWall>().First().gameObject.layer;
                }

                StartCoroutine(setupCamerasCoroutine());
            }
            catch (Exception ex)
            {
                Logger.Log(ex, LogLevel.Error);
            }
        }

        private void OnDestroy() { }

        private IEnumerator<WaitForEndOfFrame> setupCamerasCoroutine()
        {
            yield return new WaitForEndOfFrame();

            if (Plugin.IsHMDOn)
            {
                DisableScore();
                Camera.main.cullingMask &= ~(1 << TransparentWallsPatch.WallLayerMask);
            }
            else
            {
                EnableScore();
                Camera.main.cullingMask |= (1 << TransparentWallsPatch.WallLayerMask);
            }

            if (Plugin.IsDisableInLIVCamera)
            {
                try
                {
                    LIV.SDK.Unity.LIV.FindObjectsOfType<LIV.SDK.Unity.LIV>().Where(x => livNames.Contains(x.name)).ToList().ForEach(l =>
                    {
                        LayersToMask.ForEach(i => { l.SpectatorLayerMask |= (1 << i); });
                    });
                }
                catch (Exception ex)
                {
                    Logger.Log(ex, LogLevel.Error);
                }
            }
        }

        private void DisableScore()
        {
            if (!Plugin.isScoreDisabled)
            {
                Logger.Log("ScoreSubmission has been disabled.", LogLevel.Notice);
                ScoreSubmission.ProlongedDisableSubmission(Plugin.PluginName);
                Plugin.isScoreDisabled = true;
            }
        }

        private void EnableScore()
        {
            if (Plugin.isScoreDisabled)
            {
                Logger.Log("ScoreSubmission has been re-enabled.", LogLevel.Notice);
                ScoreSubmission.RemoveProlongedDisable(Plugin.PluginName);
                Plugin.isScoreDisabled = false;
            }
        }
    }
}
