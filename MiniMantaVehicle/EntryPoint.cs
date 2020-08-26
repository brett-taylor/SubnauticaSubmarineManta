using HarmonyLib;
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
        public static string QMODS_FOLDER_LOCATION { get; private set; }
        public static string MOD_FOLDER_NAME { get; private set; }
        public static readonly string ASSET_FOLDER_NAME = "Assets/";
        public static Harmony harmony { get; private set; }

        public static void Entry()
        {
            harmony = new Harmony("taylor.brett.MiniMantaVehicle.mod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            MiniMantaVehicleAssetLoader.LoadAssets();
            MiniMantaVehicleMod miniMantaVehicle = new MiniMantaVehicleMod();
            miniMantaVehicle.Patch();
        }

        public static void SetModFolderDirectory(string qmodsFolderLocation, string modFolderName)
        {
            QMODS_FOLDER_LOCATION = qmodsFolderLocation;
            MOD_FOLDER_NAME = modFolderName;
        }
    }
}
