using System.Collections.Generic;
using System.Linq;
using TransparentWall.Gameplay.Modifications;
using TransparentWall.HarmonyPatches.Patches;
using TransparentWall.Utilities;
using UnityEngine;

namespace TransparentWall.Gameplay
{
    internal class TransparentWalls : MonoBehaviour
    {
        internal static int MoveBackLayer = 27;
        internal static List<string> livNames = new List<string> { "MenuMainCamera", "MainCamera", "LIV Camera" };
        internal static List<int> LayersToMask = new List<int> { StretchableCubeCullingLayer.WallLayerMask, MoveBackLayer };

        private HMDWalls HMDWalls;
        private LIVWalls LIVWalls;

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
            else
            {
                ScoreUtility.EnableScoreSubmission("InHeadset");
            }

            if (Configuration.DisabledInLivCamera)
            {
                if (Resources.FindObjectsOfTypeAll<MoveBackWall>().Count() > 0)
                {
                    MoveBackLayer = Resources.FindObjectsOfTypeAll<MoveBackWall>().First().gameObject.layer;
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
