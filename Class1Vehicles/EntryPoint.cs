using Harmony;
using System.Reflection;

namespace Class1Vehicles
{
    /**
    * Entry point into our mod
    */
    public class EntryPoint
    {
        public static readonly string MOD_FOLDER_LOCATION = "./QMods/TheMantaMod/";
        public static readonly string ASSET_FOLDER_LOCATION = MOD_FOLDER_LOCATION + "Assets/";

        public static void Entry()
        {
            HarmonyInstance HarmonyInstance = HarmonyInstance.Create("taylor.brett.Class1Vehicles.mod");
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

            Submarines.EntryPoint.LOAD_DEFAULT_CYCLOPS_ASSETS = true;
            Submarines.EntryPoint.SetModFolderDirectory(MOD_FOLDER_LOCATION);
            Submarines.EntryPoint.InitialiseFramework();

            Manta.EntryPoint.SetModFolderDirectory(MOD_FOLDER_LOCATION);
            Manta.EntryPoint.Entry();

            Odyssey.EntryPoint.SetModFolderDirectory(MOD_FOLDER_LOCATION);
            Odyssey.EntryPoint.Entry();

            MiniMantaVehicle.EntryPoint.SetModFolderDirectory(MOD_FOLDER_LOCATION);
            MiniMantaVehicle.EntryPoint.Entry();
        }
    }
}
