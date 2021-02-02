using BS_Utils.Gameplay;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TransparentWall.Gameplay.Modifications;
using TransparentWall.Settings;
using UnityEngine;

namespace TransparentWall.Gameplay
{
    public class TransparentWalls : MonoBehaviour
    {
        private LIVWalls LIVWalls = null;

        public static readonly IList<string> LivNames = new List<string> { "MenuMainCamera", "MainCamera", "LIV Camera" };
        public static readonly List<int> LayersToMask = new List<int> { Configuration.WallLayerMask, Configuration.MoveBackLayer };

        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Unity calls this")]
        private void Start()
        {
            if (Configuration.EnableForHeadset)
            {
                ScoreSubmission.DisableSubmission(Plugin.PluginName);
            }

            if (!Configuration.DisableForLIVCamera) return;

            if (Resources.FindObjectsOfTypeAll<MoveBackWall>().Any())
            {
                Configuration.MoveBackLayer = Resources.FindObjectsOfTypeAll<MoveBackWall>().First().gameObject.layer;
            }

            LIVWalls = new LIVWalls();
            //LIVWalls = gameObject.AddComponent<LIVWalls>();
            StartCoroutine(LIVWalls.ApplyGameCoreModifications());
        }

        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Unity calls this")]
        private void OnDestroy()
        {
            LIVWalls = null;
        }
    }
}
