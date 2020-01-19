using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TransparentWall.Gameplay.Modifications
{
    public class LIVWalls : MonoBehaviour
    {
        public IEnumerator<WaitForEndOfFrame> ApplyGameCoreModifications()
        {
            yield return new WaitForEndOfFrame();

            try
            {
                FindObjectsOfType<LIV.SDK.Unity.LIV>().Where(x => TransparentWalls.livNames.Contains(x.name)).ToList().ForEach(l =>
                {
                    TransparentWalls.layersToMask.ForEach(i =>
                    {
                        l.SpectatorLayerMask |= 1 << i;
                    });
                });
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex);
            }
        }
    }
}
