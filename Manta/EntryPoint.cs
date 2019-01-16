using Harmony;
using Manta.Core;
using Manta.Utilities;
using System.Reflection;

namespace Manta
{
    /**
    * Entry point into our Manta submarine
    */
    public class EntryPoint
    {
        public static HarmonyInstance HarmonyInstance { get; private set; }
        public static string MOD_FOLDER_LOCATION { get; private set; }
        public static string ASSET_FOLDER_LOCATION { get; private set; }

        public static void Entry()
        {
            HarmonyInstance = HarmonyInstance.Create("taylor.brett.TheMantaMod.mod");
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

            MantaAssetLoader.LoadAssets();
            MantaMod manta = new MantaMod();
            manta.Patch();
            MantaMod.MANTA_TECH_TYPE = manta.TechType;
        }

        public static void SetModFolderDirectory(string directory)
        {
            MOD_FOLDER_LOCATION = directory;
            ASSET_FOLDER_LOCATION = MOD_FOLDER_LOCATION + "Assets/";
        }
    }
}