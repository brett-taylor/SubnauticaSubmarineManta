using Harmony;
using Odyssey.Core;
using Odyssey.Utilities;
using System.Reflection;

namespace Odyssey
{
    /**
    * Entry point into our Odyssey submarine
    */
    public class EntryPoint
    {
        public static HarmonyInstance HarmonyInstance { get; private set; }
        public static string MOD_FOLDER_LOCATION { get; private set; }
        public static string ASSET_FOLDER_LOCATION { get; private set; }

        public static void Entry()
        {
            HarmonyInstance = HarmonyInstance.Create("taylor.brett.Odyssey.mod");
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

            OdysseyAssetLoader.LoadAssets();
            OdysseyMod manta = new OdysseyMod();
            manta.Patch();
        }

        public static void SetModFolderDirectory(string directory)
        {
            MOD_FOLDER_LOCATION = directory;
            ASSET_FOLDER_LOCATION = MOD_FOLDER_LOCATION + "Assets/";
        }
    }
}
