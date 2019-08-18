using Harmony;
using System.Reflection;

namespace TransparentWall.HarmonyPatches
{
    /// <summary>
    /// Apply and remove all of our Harmony patches through this class
    /// </summary>
    internal class TransparentWallPatches
    {
        internal static bool IsPatched { get; private set; }
        internal static string InstanceId => "com.pespiri.beatsaber.transparentwalls";

        private static HarmonyInstance Instance;

        internal static void ApplyHarmonyPatches()
        {
            if (!IsPatched)
            {
                if (Instance == null)
                {
                    Instance = HarmonyInstance.Create(InstanceId);
                }

                Instance.PatchAll(Assembly.GetExecutingAssembly());
                IsPatched = true;
            }
        }

        internal static void RemoveHarmonyPatches()
        {
            if (Instance != null && IsPatched)
            {
                Instance.UnpatchAll(InstanceId);
                IsPatched = false;
            }
        }
    }
}
