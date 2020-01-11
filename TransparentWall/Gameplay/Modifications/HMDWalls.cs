using System.Collections.Generic;
using TransparentWall.Settings;
using UnityEngine;

namespace TransparentWall.Gameplay.Modifications
{
    public class HMDWalls
    {
        public IEnumerator<WaitForEndOfFrame> ApplyGameCoreModifications()
        {
            yield return new WaitForEndOfFrame();
            Camera.main.cullingMask &= ~(1 << Configuration.WallLayerMask);
        }
    }
}
