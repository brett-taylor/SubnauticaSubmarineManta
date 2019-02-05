using UnityEngine;

namespace Odyssey.Utilities
{
    /**
     * Handles loading all of the assets needed for the Odyssey.
     */
    public static class OdysseyAssetLoader
    {
        public static readonly string ASSET_BUNDLE_LOCATION = EntryPoint.ASSET_FOLDER_LOCATION + "odyssey";
        public static AssetBundle ASSET_BUNDLE { get; private set; }
        public static GameObject ODYSSEY_EXTERIOR { get; private set; }

        public static Texture BODY_NORMAL { get; private set; }
        public static Texture BODY_SPEC { get; private set; }

        public static Texture BODY_EXTRA_ONE_NORMAL { get; private set; }
        public static Texture BODY_EXTRA_ONE_SPEC { get; private set; }

        public static Texture BODY_EXTRA_TWO_NORMAL { get; private set; }
        public static Texture BODY_EXTRA_TWO_SPEC { get; private set; }
        public static Texture BODY_EXTRA_TWO_EMISSIVE { get; private set; }

        public static Texture CAMERA_NORMAL { get; private set; }
        public static Texture CAMERA_SPEC { get; private set; }

        public static Texture DECALS_NORMAL { get; private set; }
        public static Texture DECALS_SPEC { get; private set; }

        public static Texture HATCH_NORMAL { get; private set; }
        public static Texture HATCH_SPEC { get; private set; }

        public static Texture SENSORS_NORMAL { get; private set; }
        public static Texture SENSORS_SPEC { get; private set; }

        public static void LoadAssets()
        {
            ASSET_BUNDLE = AssetBundle.LoadFromFile(ASSET_BUNDLE_LOCATION);
            if (ASSET_BUNDLE == null)
            {
                Utilities.Log.Error("Odyssey Asset bundle not found.");
            }

            ODYSSEY_EXTERIOR = ASSET_BUNDLE.LoadAsset("Odyssey-Prefab") as GameObject;
            if (ODYSSEY_EXTERIOR == null)
            {
                Utilities.Log.Error("Odyssey exterior not found.");
            }

            BODY_NORMAL = ASSET_BUNDLE.LoadAsset("1_LP_Body_Normal") as Texture;
            if (BODY_NORMAL == null)
            {
                Utilities.Log.Error("BODY_NORMAL not found.");
            }
            BODY_SPEC = ASSET_BUNDLE.LoadAsset("1_LP_Body_SpecularSmoothness") as Texture;
            if (BODY_SPEC == null)
            {
                Utilities.Log.Error("BODY_SPEC not found.");
            }

            BODY_EXTRA_ONE_NORMAL = ASSET_BUNDLE.LoadAsset("1_LP_BodyExtra1_Normal") as Texture;
            if (BODY_EXTRA_ONE_NORMAL == null)
            {
                Utilities.Log.Error("BODY_EXTRA_ONE_NORMAL not found.");
            }
            BODY_EXTRA_ONE_SPEC = ASSET_BUNDLE.LoadAsset("1_LP_BodyExtra1_SpecularSmoothness") as Texture;
            if (BODY_EXTRA_ONE_SPEC == null)
            {
                Utilities.Log.Error("BODY_EXTRA_ONE_SPEC not found.");
            }

            BODY_EXTRA_TWO_NORMAL = ASSET_BUNDLE.LoadAsset("1_LP_BodyExtra2_Normal") as Texture;
            if (BODY_EXTRA_TWO_NORMAL == null)
            {
                Utilities.Log.Error("BODY_EXTRA_TWO_NORMAL not found.");
            }
            BODY_EXTRA_TWO_SPEC = ASSET_BUNDLE.LoadAsset("1_LP_BodyExtra2_SpecularSmoothness") as Texture;
            if (BODY_EXTRA_TWO_SPEC == null)
            {
                Utilities.Log.Error("BODY_EXTRA_TWO_SPEC not found.");
            }
            BODY_EXTRA_TWO_EMISSIVE = ASSET_BUNDLE.LoadAsset("1_LP_BodyExtra2_Emission") as Texture;
            if (BODY_EXTRA_TWO_EMISSIVE == null)
            {
                Utilities.Log.Error("BODY_EXTRA_TWO_EMISSIVE not found.");
            }

            CAMERA_NORMAL = ASSET_BUNDLE.LoadAsset("1_LP_Camera_Normal") as Texture;
            if (CAMERA_NORMAL == null)
            {
                Utilities.Log.Error("CAMERA_NORMAL not found.");
            }
            CAMERA_SPEC = ASSET_BUNDLE.LoadAsset("1_LP_Camera_SpecularSmoothness") as Texture;
            if (CAMERA_SPEC == null)
            {
                Utilities.Log.Error("CAMERA_SPEC not found.");
            }

            DECALS_NORMAL = ASSET_BUNDLE.LoadAsset("1_LP_Decals_Normal") as Texture;
            if (DECALS_NORMAL == null)
            {
                Utilities.Log.Error("DECALS_NORMAL not found.");
            }
            DECALS_SPEC = ASSET_BUNDLE.LoadAsset("1_LP_Decals_SpecularSmoothness") as Texture;
            if (DECALS_SPEC == null)
            {
                Utilities.Log.Error("DECALS_SPEC not found.");
            }

            HATCH_NORMAL = ASSET_BUNDLE.LoadAsset("1_LP_Hatch_Normal") as Texture;
            if (HATCH_NORMAL == null)
            {
                Utilities.Log.Error("HATCH_NORMAL not found.");
            }
            HATCH_SPEC = ASSET_BUNDLE.LoadAsset("1_LP_Hatch_SpecularSmoothness") as Texture;
            if (HATCH_SPEC == null)
            {
                Utilities.Log.Error("HATCH_SPEC not found.");
            }

            SENSORS_NORMAL = ASSET_BUNDLE.LoadAsset("1_LP_Sensors_Normal") as Texture;
            if (SENSORS_NORMAL == null)
            {
                Utilities.Log.Error("SENSORS_NORMAL not found.");
            }
            SENSORS_SPEC = ASSET_BUNDLE.LoadAsset("1_LP_Sensors_SpecularSmoothness") as Texture;
            if (SENSORS_SPEC == null)
            {
                Utilities.Log.Error("SENSORS_SPEC not found.");
            }
        }
    }
}
