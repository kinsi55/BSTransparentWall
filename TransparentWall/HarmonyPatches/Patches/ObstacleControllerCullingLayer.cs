using HarmonyLib;
using System.Diagnostics.CodeAnalysis;
using TransparentWall.Settings;
using UnityEngine;

namespace TransparentWall.HarmonyPatches.Patches
{
    [HarmonyPatch(typeof(ObstacleController))]
    [HarmonyPatch("Init", MethodType.Normal)]
    internal class ObstacleControllerCullingLayer
    {
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Harmony calls this")]
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

            if (Configuration.EnableForHeadset)
            {
                Camera.main.cullingMask &= ~(1 << Configuration.WallLayerMask);
            }
        }
    }
}
