using UnityEngine;

namespace Manta.Utilities
{
    /**
     * Handles loading all of the assets needed for the manta.
     */
    public static class MantaAssetLoader
    {
        public static readonly string ASSET_BUNDLE_LOCATION = EntryPoint.ASSET_FOLDER_LOCATION + "manta";
        public static AssetBundle ASSET_BUNDLE { get; private set; }
        public static GameObject MANTA_EXTERIOR { get; private set; }
        public static Texture MANTA_EXTERIOR_EMISSION_MAP { get; private set; }

        public static void LoadAssets()
        {
            ASSET_BUNDLE = AssetBundle.LoadFromFile(ASSET_BUNDLE_LOCATION);
            if (ASSET_BUNDLE == null)
            {
                Utilities.Log.Error("Manta Asset bundle not found.");
            }

            MANTA_EXTERIOR = ASSET_BUNDLE.LoadAsset("Manta-Prefab") as GameObject;
            if (MANTA_EXTERIOR == null)
            {
                Utilities.Log.Error("Manta exterior not found.");
            }

            MANTA_EXTERIOR_EMISSION_MAP = ASSET_BUNDLE.LoadAsset("MantaFinV2_Hull_Emissive") as Texture;
            if (MANTA_EXTERIOR_EMISSION_MAP == null)
            {
                Utilities.Log.Error("Manta exterior emission map not found.");
            }
        }
    }
}
