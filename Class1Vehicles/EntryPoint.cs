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
            Submarines.EntryPoint.SetModFolderDirectory(MOD_FOLDER_LOCATION);
            Submarines.EntryPoint.InitialiseFramework();

            Manta.EntryPoint.SetModFolderDirectory(MOD_FOLDER_LOCATION);
            Manta.EntryPoint.Entry();
        }
    }
}
