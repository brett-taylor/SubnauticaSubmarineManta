using HarmonyLib;
using System.Reflection;

namespace Class1Vehicles
{
    /**
    * Entry point into our mod
    */
    public class EntryPoint
    {
        public static readonly string QMODS_FOLDER_LOCATION = "./QMods/";
        public static readonly string MOD_FOLDER_NAME = "TheMantaMod/";

        public static void Entry()
        {
            Harmony HarmonyInstance = new Harmony("taylor.brett.Class1Vehicles.mod");
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

            Submarines.EntryPoint.LOAD_DEFAULT_CYCLOPS_ASSETS = true;
            Submarines.EntryPoint.SetModFolderDirectory(QMODS_FOLDER_LOCATION, MOD_FOLDER_NAME);
            Submarines.EntryPoint.InitialiseFramework();

            Manta.EntryPoint.SetModFolderDirectory(QMODS_FOLDER_LOCATION, MOD_FOLDER_NAME);
            Manta.EntryPoint.Entry();

            Odyssey.EntryPoint.SetModFolderDirectory(QMODS_FOLDER_LOCATION, MOD_FOLDER_NAME);
            Odyssey.EntryPoint.Entry();

            MiniMantaVehicle.EntryPoint.SetModFolderDirectory(QMODS_FOLDER_LOCATION, MOD_FOLDER_NAME);
            MiniMantaVehicle.EntryPoint.Entry();

            MiniMantaDrone.EntryPoint.SetModFolderDirectory(QMODS_FOLDER_LOCATION, MOD_FOLDER_NAME);
            MiniMantaDrone.EntryPoint.Entry();

            PlayerAnimations.EntryPoint.Entry();
        }
    }
}
