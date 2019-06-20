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
        public static int WallLayer = 25;
        public static int MoveBackLayer = 27;
        public static List<int> LayersToMask = new List<int> { WallLayer, MoveBackLayer };
        public static List<string> livNames = new List<string> { "MenuMainCamera", "MainCamera", "LIV Camera" };

        private BeatmapObjectSpawnController _beatmapObjectSpawnController;

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
                if (Resources.FindObjectsOfTypeAll<BeatmapObjectSpawnController>().Count() > 0)
                {
                    _beatmapObjectSpawnController = Resources.FindObjectsOfTypeAll<BeatmapObjectSpawnController>().First();
                }

                if (Resources.FindObjectsOfTypeAll<MoveBackWall>().Count() > 0)
                {
                    MoveBackLayer = Resources.FindObjectsOfTypeAll<MoveBackWall>().First().gameObject.layer;
                }

                if (_beatmapObjectSpawnController != null)
                {
                    _beatmapObjectSpawnController.obstacleDiStartMovementEvent += this.HandleObstacleDiStartMovementEvent;
                }

                StartCoroutine(setupCamerasCoroutine());
            }
            catch (Exception ex)
            {
                Logger.Log(ex, LogLevel.Error);
            }
        }

        private void OnDestroy()
        {
            if (_beatmapObjectSpawnController != null)
            {
                _beatmapObjectSpawnController.obstacleDiStartMovementEvent -= this.HandleObstacleDiStartMovementEvent;
            }
        }

        private IEnumerator<WaitForEndOfFrame> setupCamerasCoroutine()
        {
            yield return new WaitForEndOfFrame();

            Camera mainCamera = Camera.main;

            if (Plugin.IsHMDOn)
            {
                DisableScore();
                mainCamera.cullingMask &= ~(1 << WallLayer);
            }
            else
            {
                EnableScore();
                mainCamera.cullingMask |= (1 << WallLayer);
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

        public virtual void HandleObstacleDiStartMovementEvent(BeatmapObjectSpawnController obstacleSpawnController, ObstacleController obstacleController)
        {
            try
            {
                StretchableObstacle _stretchableObstacle = ReflectionUtil.GetPrivateField<StretchableObstacle>(obstacleController, "_stretchableObstacle");
                StretchableCube _stretchableCore = ReflectionUtil.GetPrivateField<StretchableCube>(_stretchableObstacle, "_stretchableCore");
                //MeshRenderer _meshRenderer = ReflectionUtil.GetPrivateField<MeshRenderer>(_stretchableCore, "_mesh");
                _stretchableCore.gameObject.layer = WallLayer;
            }
            catch (Exception ex)
            {
                Logger.Log(ex, LogLevel.Error);
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
