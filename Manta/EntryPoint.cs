using HarmonyLib;
using Manta.Core;
using Manta.Utilities;
using QModManager.API.ModLoading;
using System.Reflection;

namespace Manta
{
    /**
    * Entry point into our Manta submarine
    */
    public class EntryPoint
    {
        public static string QMODS_FOLDER_LOCATION { get; private set; }
        public static string MOD_FOLDER_NAME { get; private set; }
        public static readonly string ASSET_FOLDER_NAME = "Assets/";
        public static Harmony harmony { get; private set; }

        public static void Entry()
        {
            harmony = new Harmony("taylor.brett.TheMantaMod.mod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            MantaAssetLoader.LoadAssets();
            MantaMod manta = new MantaMod();
            manta.Patch();
        }

        public static void SetModFolderDirectory(string qmodsFolderLocation, string modFolderName)
        {
            QMODS_FOLDER_LOCATION = qmodsFolderLocation;
            MOD_FOLDER_NAME = modFolderName;
        }
    }
}