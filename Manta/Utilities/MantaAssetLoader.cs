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

        public static Texture HULL_ONE_MAIN_TEX { get; private set; }
        public static Texture HULL_TWO_MAIN_TEX { get; private set; }
        public static Texture HULL_THREE_MAIN_TEX { get; private set; }
        public static Texture HULL_FOUR_MAIN_TEX { get; private set; }

        public static Texture HULL_ONE_NORMAL_MAP { get; private set; }
        public static Texture HULL_TWO_NORMAL_MAP { get; private set; }
        public static Texture HULL_THREE_NORMAL_MAP { get; private set; }
        public static Texture HULL_FOUR_NORMAL_MAP { get; private set; }

        public static Texture HULL_ONE_SPEC_MAP { get; private set; }
        public static Texture HULL_TWO_SPEC_MAP { get; private set; }
        public static Texture HULL_THREE_SPEC_MAP { get; private set; }
        public static Texture HULL_FOUR_SPEC_MAP { get; private set; }

        public static Texture HULL_TWO_EMISSIVE_MAP { get; private set; }
        public static Texture HULL_FOUR_EMISSIVE_MAP { get; private set; }

        public static void LoadAssets()
        {
            /**
             * MISC
             */
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
            /**
             * END OFMISC
             */

            /**
             * START OF MAIN TEX
             */
            HULL_ONE_MAIN_TEX = ASSET_BUNDLE.LoadAsset("MantaFinv2interiorrevision2Reunwrappedexterior_Hull_BaseColor") as Texture;
            if (HULL_ONE_MAIN_TEX == null)
            {
                Utilities.Log.Error("Manta HULL_ONE_MAIN_TEX not found.");
            }

            HULL_TWO_MAIN_TEX = ASSET_BUNDLE.LoadAsset("MantaFinv2interiorrevision2Reunwrappedexterior_Hull2_BaseColor") as Texture;
            if (HULL_TWO_MAIN_TEX == null)
            {
                Utilities.Log.Error("Manta HULL_TWO_MAIN_TEX not found.");
            }

            HULL_THREE_MAIN_TEX = ASSET_BUNDLE.LoadAsset("MantaFinv2interiorrevision2Reunwrappedexterior_Hull3_BaseColor") as Texture;
            if (HULL_THREE_MAIN_TEX == null)
            {
                Utilities.Log.Error("Manta HULL_THREE_MAIN_TEX not found.");
            }

            HULL_FOUR_MAIN_TEX = ASSET_BUNDLE.LoadAsset("MantaFinv2interiorrevision2Reunwrappedexterior_Hull4_BaseColor") as Texture;
            if (HULL_FOUR_MAIN_TEX == null)
            {
                Utilities.Log.Error("Manta HULL_FOUR_MAIN_TEX not found.");
            }
            /**
            * END OF MAIN TEXT
            */

            /**
             * START OF NORMAL MAPS
             */
            HULL_ONE_NORMAL_MAP = ASSET_BUNDLE.LoadAsset("MantaFinv2interiorrevision2Reunwrappedexterior_Hull_Normal") as Texture;
            if (HULL_ONE_NORMAL_MAP == null)
            {
                Utilities.Log.Error("Manta HULL_ONE_NORMAL_MAP not found.");
            }

            HULL_TWO_NORMAL_MAP = ASSET_BUNDLE.LoadAsset("MantaFinv2interiorrevision2Reunwrappedexterior_Hull2_Normal") as Texture;
            if (HULL_TWO_NORMAL_MAP == null)
            {
                Utilities.Log.Error("Manta HULL_TWO_NORMAL_MAP not found.");
            }

            HULL_THREE_NORMAL_MAP = ASSET_BUNDLE.LoadAsset("MantaFinv2interiorrevision2Reunwrappedexterior_Hull3_Normal") as Texture;
            if (HULL_THREE_NORMAL_MAP == null)
            {
                Utilities.Log.Error("Manta HULL_THREE_NORMAL_MAP not found.");
            }

            HULL_FOUR_NORMAL_MAP = ASSET_BUNDLE.LoadAsset("MantaFinv2interiorrevision2Reunwrappedexterior_Hull4_Normal") as Texture;
            if (HULL_FOUR_NORMAL_MAP == null)
            {
                Utilities.Log.Error("Manta HULL_FOUR_NORMAL_MAP not found.");
            }
            /**
             * END OF NORMAL MAPS
             */

            /**
             * START OF SPEC MAPS
             */
            HULL_ONE_SPEC_MAP = ASSET_BUNDLE.LoadAsset("MantaFinv2interiorrevision2Reunwrappedexterior_Hull_Roughness") as Texture;
            if (HULL_ONE_SPEC_MAP == null)
            {
                Utilities.Log.Error("Manta HULL_ONE_SPEC_MAP not found.");
            }

            HULL_TWO_SPEC_MAP = ASSET_BUNDLE.LoadAsset("MantaFinv2interiorrevision2Reunwrappedexterior_Hull2_Roughness") as Texture;
            if (HULL_TWO_SPEC_MAP == null)
            {
                Utilities.Log.Error("Manta HULL_TWO_SPEC_MAP not found.");
            }

            HULL_THREE_SPEC_MAP = ASSET_BUNDLE.LoadAsset("MantaFinv2interiorrevision2Reunwrappedexterior_Hull3_Roughness") as Texture;
            if (HULL_THREE_SPEC_MAP == null)
            {
                Utilities.Log.Error("Manta HULL_THREE_SPEC_MAP not found.");
            }

            HULL_FOUR_SPEC_MAP = ASSET_BUNDLE.LoadAsset("MantaFinv2interiorrevision2Reunwrappedexterior_Hull4_Roughness") as Texture;
            if (HULL_FOUR_SPEC_MAP == null)
            {
                Utilities.Log.Error("Manta HULL_FOUR_SPEC_MAP not found.");
            }
            /**
            * END OF SPEC MAPS
            */

            /**
             * START OF EMISSIVE MAPS
             */
            HULL_TWO_EMISSIVE_MAP = ASSET_BUNDLE.LoadAsset("MantaFinv2interiorrevision2Reunwrappedexterior_Hull2_Emissive") as Texture;
            if (HULL_TWO_EMISSIVE_MAP == null)
            {
                Utilities.Log.Error("Manta HULL_TWO_EMISSIVE_MAP not found.");
            }

            HULL_FOUR_EMISSIVE_MAP = ASSET_BUNDLE.LoadAsset("MantaFinv2interiorrevision2Reunwrappedexterior_Hull4_Emissive") as Texture;
            if (HULL_FOUR_EMISSIVE_MAP == null)
            {
                Utilities.Log.Error("Manta HULL_FOUR_EMISSIVE_MAP not found.");
            }
            /**
             * END OF EMISSIVE MAPS
             */
        }
    }
}
