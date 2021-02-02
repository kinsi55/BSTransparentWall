using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TransparentWall.Gameplay.Modifications
{
    public class LIVWalls : MonoBehaviour
    {
        public static IEnumerator<WaitForEndOfFrame> ApplyGameCoreModifications()
        {
            yield return new WaitForEndOfFrame();

            try
            {
                FindObjectsOfType<LIV.SDK.Unity.LIV>().Where(x => TransparentWalls.LivNames.Contains(x.name)).ToList().ForEach(l =>
                {
                    TransparentWalls.LayersToMask.ForEach(i =>
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
