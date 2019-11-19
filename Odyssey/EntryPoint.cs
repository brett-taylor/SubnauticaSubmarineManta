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
        public static string QMODS_FOLDER_LOCATION { get; private set; }
        public static string MOD_FOLDER_NAME { get; private set; }
        public static readonly string ASSET_FOLDER_NAME = "Assets/";
        public static HarmonyInstance HarmonyInstance { get; private set; }

        public static void Entry()
        {
            HarmonyInstance = HarmonyInstance.Create("taylor.brett.Odyssey.mod");
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

            Assets.Instance = new Assets();

            OdysseyMod odyssey = new OdysseyMod();
            odyssey.Patch();
        }

        public static void SetModFolderDirectory(string qmodsFolderLocation, string modFolderName)
        {
            QMODS_FOLDER_LOCATION = qmodsFolderLocation;
            MOD_FOLDER_NAME = modFolderName;
        }
    }
}
