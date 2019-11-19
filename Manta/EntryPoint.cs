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
        public static string QMODS_FOLDER_LOCATION { get; private set; }
        public static string MOD_FOLDER_NAME { get; private set; }
        public static readonly string ASSET_FOLDER_NAME = "Assets/";
        public static HarmonyInstance HarmonyInstance { get; private set; }

        public static void Entry()
        {
            HarmonyInstance = HarmonyInstance.Create("taylor.brett.TheMantaMod.mod");
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

            Assets.Instance = new Assets();

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