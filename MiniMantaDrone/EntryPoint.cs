using Harmony;
using MiniMantaDrone.Core;
using MiniMantaDrone.Utilities;
using System.Reflection;

namespace MiniMantaDrone
{
    /**
    * Entry point into our Mini Manta Drone
    */
    public class EntryPoint
    {
        public static HarmonyInstance HarmonyInstance { get; private set; }
        public static string MOD_FOLDER_LOCATION { get; private set; }
        public static string ASSET_FOLDER_LOCATION { get; private set; }

        public static void Entry()
        {
            HarmonyInstance = HarmonyInstance.Create("taylor.brett.MiniMantaDrone.mod");
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

            MiniMantaDroneAssetLoader.LoadAssets();
            MiniMantaDroneMod miniMantaDrone = new MiniMantaDroneMod();
            miniMantaDrone.Patch();
        }

        public static void SetModFolderDirectory(string directory)
        {
            MOD_FOLDER_LOCATION = directory;
            ASSET_FOLDER_LOCATION = MOD_FOLDER_LOCATION + "Assets/";
        }
    }
}
