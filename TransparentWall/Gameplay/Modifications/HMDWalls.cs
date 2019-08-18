using System.Collections.Generic;
using TransparentWall.HarmonyPatches.Patches;
using UnityEngine;

namespace TransparentWall.Gameplay.Modifications
{
    public class HMDWalls
    {
        public IEnumerator<WaitForEndOfFrame> ApplyGameCoreModifications()
        {
            yield return new WaitForEndOfFrame();

            if (Configuration.InHeadset)
            {
                Camera.main.cullingMask &= ~(1 << StretchableCubeCullingLayer.WallLayerMask);
            }
            else
            {
                Camera.main.cullingMask |= (1 << StretchableCubeCullingLayer.WallLayerMask);
            }
        }
    }
}
