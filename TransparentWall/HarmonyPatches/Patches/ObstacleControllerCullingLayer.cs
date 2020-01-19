using Harmony;
using TransparentWall.Settings;
using UnityEngine;

namespace TransparentWall.HarmonyPatches.Patches
{
    [HarmonyPatch(typeof(ObstacleController))]
    [HarmonyPatch("Init", MethodType.Normal)]
    internal class ObstacleControllerCullingLayer
    {
        private static void Postfix(ref ObstacleController __instance)
        {
            if (Configuration.EnableForHeadset || Configuration.DisableForLIVCamera)
            {
                Renderer mesh = __instance.gameObject?.GetComponentInChildren<Renderer>(false);
                if (mesh?.gameObject)
                {
                    mesh.gameObject.layer = Configuration.WallLayerMask;
                }
            }
        }
    }
}
