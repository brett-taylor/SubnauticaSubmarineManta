using HarmonyLib;
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
        public static string QMODS_FOLDER_LOCATION { get; private set; }
        public static string MOD_FOLDER_NAME { get; private set; }
        public static readonly string ASSET_FOLDER_NAME = "Assets/";
        public static Harmony harmony { get; private set; }

        public static void Entry()
        {
            harmony = new Harmony("taylor.brett.Odyssey.mod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            OdysseyAssetLoader.LoadAssets();
            OdysseyMod manta = new OdysseyMod();
            manta.Patch();
        }

        public static void SetModFolderDirectory(string qmodsFolderLocation, string modFolderName)
        {
            QMODS_FOLDER_LOCATION = qmodsFolderLocation;
            MOD_FOLDER_NAME = modFolderName;
        }
    }
}
