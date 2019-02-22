using Harmony;
using MiniMantaVehicle.Core;
using MiniMantaVehicle.Utilities;
using System.Reflection;

namespace MiniMantaVehicle
{
    /**
    * Entry point into our Mini Manta Vehicle
    */
    public class EntryPoint
    {
        public static HarmonyInstance HarmonyInstance { get; private set; }
        public static string MOD_FOLDER_LOCATION { get; private set; }
        public static string ASSET_FOLDER_LOCATION { get; private set; }

        public static void Entry()
        {
            HarmonyInstance = HarmonyInstance.Create("taylor.brett.MiniMantaVehicle.mod");
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

            MiniMantaVehicleAssetLoader.LoadAssets();
            MiniMantaVehicleMod miniMantaVehicle = new MiniMantaVehicleMod();
            miniMantaVehicle.Patch();
        }

        public static void SetModFolderDirectory(string directory)
        {
            MOD_FOLDER_LOCATION = directory;
            ASSET_FOLDER_LOCATION = MOD_FOLDER_LOCATION + "Assets/";
        }
    }
}
