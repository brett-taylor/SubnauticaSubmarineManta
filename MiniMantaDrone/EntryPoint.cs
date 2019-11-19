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
        public static string QMODS_FOLDER_LOCATION { get; private set; }
        public static string MOD_FOLDER_NAME { get; private set; }
        public static readonly string ASSET_FOLDER_NAME = "Assets/";
        public static HarmonyInstance HarmonyInstance { get; private set; }

        public static void Entry()
        {
            HarmonyInstance = HarmonyInstance.Create("taylor.brett.MiniMantaDrone.mod");
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

            Assets.Instance = new Assets();

            MiniMantaDroneMod miniMantaDrone = new MiniMantaDroneMod();
            miniMantaDrone.Patch();
        }

        public static void SetModFolderDirectory(string qmodsFolderLocation, string modFolderName)
        {
            QMODS_FOLDER_LOCATION = qmodsFolderLocation;
            MOD_FOLDER_NAME = modFolderName;
        }
    }
}
