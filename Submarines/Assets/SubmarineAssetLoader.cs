using UnityEngine;

namespace Submarines.Assets
{
    /**
     * Handles loading all of the assets needed for the manta.
     */
    public static class SubmarineAssetLoader
    {
        public static readonly string ASSET_BUNDLE_LOCATION = EntryPoint.ASSET_FOLDER_LOCATION + "submarine";
        public static AssetBundle ASSET_BUNDLE { get; private set; }

        public static void LoadAssets()
        {
            ASSET_BUNDLE = AssetBundle.LoadFromFile(ASSET_BUNDLE_LOCATION);
            if (ASSET_BUNDLE == null)
            {
                Utilities.Log.Error("Submarine Asset bundle not found.");
            }
        }
    }
}
