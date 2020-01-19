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

        public static readonly IList<string> livNames = new List<string> { "MenuMainCamera", "MainCamera", "LIV Camera" };
        public static readonly List<int> layersToMask = new List<int> { Configuration.WallLayerMask, Configuration.MoveBackLayer };

        private void Start()
        {
            if (Configuration.EnableForHeadset)
            {
                ScoreUtility.DisableScoreSubmission("InHeadset");
                HMDWalls = new HMDWalls();
                StartCoroutine(HMDWalls.ApplyGameCoreModifications());
            }
            else if (ScoreUtility.ScoreIsBlocked)
            {
                ScoreUtility.EnableScoreSubmission("InHeadset");
            }

            if (Configuration.DisableForLIVCamera)
            {
                if (Resources.FindObjectsOfTypeAll<MoveBackWall>().Count() > 0)
                {
                    Configuration.MoveBackLayer = Resources.FindObjectsOfTypeAll<MoveBackWall>().First().gameObject.layer;
                }

                LIVWalls = new LIVWalls();
                StartCoroutine(LIVWalls.ApplyGameCoreModifications());
            }
        }

        private void OnDestroy()
        {
            HMDWalls = null;
            LIVWalls = null;
        }
    }
}
