using Harmony;
using System.Reflection;

namespace Submarines
{
    /**
     * Entry point into our mod
     */
    public class EntryPoint
    {
        public static string MOD_FOLDER_LOCATION { get; private set; }
        public static string ASSET_FOLDER_LOCATION { get; private set; }
        public static HarmonyInstance HarmonyInstance { get; private set; }
        public static bool LOAD_DEFAULT_CYCLOPS_ASSETS { get; set; } = true;

        public static void InitialiseFramework()
        {
            if (MOD_FOLDER_LOCATION == null || ASSET_FOLDER_LOCATION == null)
            {
                ErrorMessage.AddError("Submarines.EntryPoint.Entry::SetModFolderDirectory() must be set first.");
                return;
            }

            HarmonyInstance = HarmonyInstance.Create("taylor.brett.SubmarinesFramework.mod");
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

            Assets.SubmarineAssetLoader.LoadAssets();
        }

        public static void SetModFolderDirectory(string directory)
        {
            MOD_FOLDER_LOCATION = directory;
            ASSET_FOLDER_LOCATION = MOD_FOLDER_LOCATION + "Assets/";
        }
    }
}
