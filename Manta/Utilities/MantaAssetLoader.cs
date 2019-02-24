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

        public static Texture HULL_ONE_NORMAL_MAP { get; private set; }
        public static Texture HULL_ONE_SPEC_MAP { get; private set; }

        public static Texture HULL_TWO_SPEC_MAP { get; private set; }
        public static Texture HULL_TWO_NORMAL_MAP { get; private set; }
        public static Texture HULL_TWO_EMISSIVE_MAP { get; private set; }

        public static Texture HULL_THREE_SPEC_MAP { get; private set; }
        public static Texture HULL_THREE_NORMAL_MAP { get; private set; }

        public static Texture HULL_FOUR_NORMAL_MAP { get; private set; }
        public static Texture HULL_FOUR_EMISSIVE_MAP { get; private set; }
        public static Texture HULL_FOUR_SPEC_MAP { get; private set; }

        public static Texture FLOOR_NORMAL_MAP { get; private set; }

        public static Texture LIGHT_EMISSIVE_MAP{ get; private set; }

        public static void LoadAssets()
        {
            ASSET_BUNDLE = AssetBundle.LoadFromFile(ASSET_BUNDLE_LOCATION);
            if (ASSET_BUNDLE == null)
            {
                Log.Error("Manta Asset bundle not found.");
            }

            MANTA_EXTERIOR = ASSET_BUNDLE.LoadAsset("Manta-Prefab") as GameObject;
            if (MANTA_EXTERIOR == null)
            {
                Log.Error("Manta exterior not found.");
            }

            HULL_ONE_NORMAL_MAP = ASSET_BUNDLE.LoadAsset("Hull1_normal") as Texture;
            if (HULL_ONE_NORMAL_MAP == null)
            {
                Log.Error("Manta HULL_ONE_NORMAL_MAP not found.");
            }
            HULL_ONE_SPEC_MAP = ASSET_BUNDLE.LoadAsset("DefaultSpecMap") as Texture;
            if (HULL_ONE_SPEC_MAP == null)
            {
                Log.Error("Manta HULL_ONE_SPEC_MAP not found.");
            }

            HULL_TWO_NORMAL_MAP = ASSET_BUNDLE.LoadAsset("Hull2_normal") as Texture;
            if (HULL_TWO_NORMAL_MAP == null)
            {
                Log.Error("Manta HULL_TWO_NORMAL_MAP not found.");
            }
            HULL_TWO_EMISSIVE_MAP = ASSET_BUNDLE.LoadAsset("Hull2_Emit") as Texture;
            if (HULL_TWO_EMISSIVE_MAP == null)
            {
                Log.Error("Manta HULL_TWO_EMISSIVE_MAP not found.");
            }
            HULL_TWO_SPEC_MAP = ASSET_BUNDLE.LoadAsset("Hull2_Diffuse") as Texture;
            if (HULL_TWO_SPEC_MAP == null)
            {
                Log.Error("Manta HULL_TWO_SPEC_MAP not found.");
            }

            HULL_THREE_NORMAL_MAP = ASSET_BUNDLE.LoadAsset("Hull3_normal") as Texture;
            if (HULL_THREE_NORMAL_MAP == null)
            {
                Log.Error("Manta HULL_THREE_NORMAL_MAP not found.");
            }
            HULL_THREE_SPEC_MAP = ASSET_BUNDLE.LoadAsset("Hull3_Diffuse") as Texture;
            if (HULL_THREE_SPEC_MAP == null)
            {
                Log.Error("Manta HULL_THREE_SPEC_MAP not found.");
            }

            HULL_FOUR_NORMAL_MAP = ASSET_BUNDLE.LoadAsset("MantaFinv2interiorrevision2Reunwrappedexterior_Hull4_Normal") as Texture;
            if (HULL_FOUR_NORMAL_MAP == null)
            {
                Log.Error("Manta HULL_FOUR_NORMAL_MAP not found.");
            }
            HULL_FOUR_EMISSIVE_MAP = ASSET_BUNDLE.LoadAsset("Hull4_Emit") as Texture;
            if (HULL_FOUR_EMISSIVE_MAP == null)
            {
                Log.Error("Manta HULL_FOUR_EMISSIVE_MAP not found.");
            }
            HULL_FOUR_SPEC_MAP = ASSET_BUNDLE.LoadAsset("DefaultSpecMap") as Texture;
            if (HULL_FOUR_SPEC_MAP == null)
            {
                Log.Error("Manta HULL_FOUR_SPEC_MAP not found.");
            }

            FLOOR_NORMAL_MAP = ASSET_BUNDLE.LoadAsset("HexNormal") as Texture;
            if (FLOOR_NORMAL_MAP == null)
            {
                Log.Error("Manta FLOOR_NORMAL_MAP not found.");
            }

            LIGHT_EMISSIVE_MAP = ASSET_BUNDLE.LoadAsset("LightDecal") as Texture;
            if (LIGHT_EMISSIVE_MAP == null)
            {
                Log.Error("Manta LIGHT_EMISSIVE_MAP not found.");
            }
        }
    }
}
