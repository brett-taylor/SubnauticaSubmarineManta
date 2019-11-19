using Harmony;
using System.Reflection;

namespace Submarines
{
    /**
     * Entry point into our mod
     */
    public class EntryPoint
    {
        public static string QMODS_FOLDER_LOCATION { get; private set; }
        public static string MOD_FOLDER_NAME { get; private set; }
        public static readonly string ASSET_FOLDER_NAME = "Assets/";
        public static HarmonyInstance HarmonyInstance { get; private set; }
        public static bool LOAD_DEFAULT_CYCLOPS_ASSETS { get; set; } = true;

        public static void InitialiseFramework()
        {
            if (QMODS_FOLDER_LOCATION == null || MOD_FOLDER_NAME == null)
            {
                ErrorMessage.AddError("Submarines.EntryPoint.Entry::SetModFolderDirectory() must be set first.");
                return;
            }

            Assets.Assets.Instance = new Assets.Assets();

            HarmonyInstance = HarmonyInstance.Create("taylor.brett.SubmarinesFramework.mod");
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
            Content.Beacon.CustomBeaconManager.Initialize();
        }

        public static void SetModFolderDirectory(string qmodsFolderLocation, string modFolderName)
        {
            QMODS_FOLDER_LOCATION = qmodsFolderLocation;
            MOD_FOLDER_NAME = modFolderName;
        }
    }
}
