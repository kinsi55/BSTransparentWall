using System.Collections.Generic;
using System.Linq;
using TransparentWall.Gameplay.Modifications;
using TransparentWall.Settings;
using TransparentWall.Utilities;
using UnityEngine;

namespace TransparentWall.Gameplay
{
    public class TransparentWalls : MonoBehaviour
    {
        private HMDWalls HMDWalls = null;
        private LIVWalls LIVWalls = null;

        public static readonly List<string> livNames = new List<string> { "MenuMainCamera", "MainCamera", "LIV Camera" };
        public static readonly List<int> LayersToMask = new List<int> { Configuration.WallLayerMask, Configuration.MoveBackLayer };

#pragma warning disable IDE0051 // Used by MonoBehaviour
        private void Start() => Load();
        private void OnDestroy() => Unload();
#pragma warning restore IDE0051 // Used by MonoBehaviour

        private void Load()
        {
            if (Configuration.InHeadset)
            {
                ScoreUtility.DisableScoreSubmission("InHeadset");
                HMDWalls = new HMDWalls();
                StartCoroutine(HMDWalls.ApplyGameCoreModifications());
            }
            else if (ScoreUtility.ScoreIsBlocked)
            {
                ScoreUtility.EnableScoreSubmission("InHeadset");
            }

            if (Configuration.DisabledInLivCamera)
            {
                if (Resources.FindObjectsOfTypeAll<MoveBackWall>().Count() > 0)
                {
                    Configuration.MoveBackLayer = Resources.FindObjectsOfTypeAll<MoveBackWall>().First().gameObject.layer;
                }

                LIVWalls = new LIVWalls();
                StartCoroutine(LIVWalls.ApplyGameCoreModifications());
            }
        }

        private void Unload()
        {
            HMDWalls = null;
            LIVWalls = null;
        }
    }
}
