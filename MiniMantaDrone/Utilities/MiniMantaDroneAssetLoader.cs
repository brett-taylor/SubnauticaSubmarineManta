using UnityEngine;

namespace MiniMantaDrone.Utilities
{
    /**
     * Handles loading all of the assets needed for the Mini Manta Drone.
     */
    public class MiniMantaDroneAssetLoader
    {
        public static readonly string ASSET_BUNDLE_LOCATION = EntryPoint.QMODS_FOLDER_LOCATION + EntryPoint.MOD_FOLDER_NAME + EntryPoint.ASSET_FOLDER_NAME + "minimantadrone";
        public static AssetBundle ASSET_BUNDLE { get; private set; }
        public static GameObject MINI_MANTA_DRONE_EXTERIOR { get; private set; }

        public static void LoadAssets()
        {
            ASSET_BUNDLE = AssetBundle.LoadFromFile(ASSET_BUNDLE_LOCATION);
            if (ASSET_BUNDLE == null)
            {
                Log.Error("MiniMantaDrone Asset bundle not found.");
            }

            MINI_MANTA_DRONE_EXTERIOR = ASSET_BUNDLE.LoadAsset("MiniMantaDrone") as GameObject;
            if (MINI_MANTA_DRONE_EXTERIOR == null)
            {
                Log.Error("MiniMantaDrone exterior not found.");
            }
        }
    }
}
