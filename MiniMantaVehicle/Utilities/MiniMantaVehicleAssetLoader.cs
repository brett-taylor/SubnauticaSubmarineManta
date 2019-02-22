using UnityEngine;

namespace MiniMantaVehicle.Utilities
{
    /**
     * Handles loading all of the assets needed for the Mini Manta Vehicle.
     */
    public class MiniMantaVehicleAssetLoader
    {
        public static readonly string ASSET_BUNDLE_LOCATION = EntryPoint.ASSET_FOLDER_LOCATION + "minimantavehicle";
        public static AssetBundle ASSET_BUNDLE { get; private set; }
        public static GameObject MINI_MANTA_VEHICLE_EXTERIOR { get; private set; }

        public static void LoadAssets()
        {
            ASSET_BUNDLE = AssetBundle.LoadFromFile(ASSET_BUNDLE_LOCATION);
            if (ASSET_BUNDLE == null)
            {
                Log.Error("MiniMantaVehicle Asset bundle not found.");
            }

            MINI_MANTA_VEHICLE_EXTERIOR = ASSET_BUNDLE.LoadAsset("MiniMantaVehicle") as GameObject;
            if (MINI_MANTA_VEHICLE_EXTERIOR == null)
            {
                Log.Error("MiniMantaVehicle exterior not found.");
            }
        }
    }
}
