using HarmonyLib;
using System.Reflection;

namespace Submarines
{
    /**
     * Entry point into our mod
     */
    public class EntryPoint
    {
        public static string MOD_FOLDER_LOCATION;
        public static Harmony HarmonyInstance;
        public static bool LOAD_DEFAULT_CYCLOPS_ASSETS = true;

        public static void InitialiseFramework()
        {
            Assets.Assets.Instance = new Assets.Assets();

            HarmonyInstance = new Harmony("taylor.brett.SubmarinesFramework.mod");
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
            Content.Beacon.CustomBeaconManager.Initialize();
        }
    }
}
