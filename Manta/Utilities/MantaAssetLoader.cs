using UnityEngine;

namespace Manta.Utilities
{
    /**
     * Handles loading all of the assets needed for the manta.
     */
    public static class MantaAssetLoader
    {
        public static readonly string MANTAOS_ASSET_BUNDLE_LOCATION = EntryPoint.QMODS_FOLDER_LOCATION + EntryPoint.MOD_FOLDER_NAME + EntryPoint.ASSET_FOLDER_NAME + "mantaos";
        public static AssetBundle MANTAOS_ASSET_BUNDLE { get; private set; }
        public static GameObject MANTAOS_OFFLINE_PAGE { get; private set; }
        public static GameObject MANTA_OS_MAIN_LAYOUT_PAGE { get; private set; }

        public static readonly string ASSET_BUNDLE_LOCATION = EntryPoint.QMODS_FOLDER_LOCATION + EntryPoint.MOD_FOLDER_NAME + EntryPoint.ASSET_FOLDER_NAME + "manta";
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

        public static Texture WALL_DECALS_ONE_NORMAL_MAP { get; private set; }
        public static Texture WALL_DECALS_ONE_EMISSIVE_MAP { get; private set; }

        public static Texture WALL_DECALS_TWO_NORMAL_MAP { get; private set; }
        public static Texture WALL_DECALS_TWO_EMISSIVE_MAP { get; private set; }

        public static Texture WALL_DECALS_THREE_NORMAL_MAP { get; private set; }

        public static Texture EXTERIOR_DECALS_NORMAL_MAP { get; private set; }

        public static Sprite MANTA_PING_ICON { get; private set; }

        public static void LoadOSAssets()
        {
            MANTAOS_ASSET_BUNDLE = AssetBundle.LoadFromFile(MANTAOS_ASSET_BUNDLE_LOCATION);
            if (MANTAOS_ASSET_BUNDLE == null)
            {
                Log.Error("MANTAOS_ASSET_BUNDLE not found.");
            }

            MANTAOS_OFFLINE_PAGE = MANTAOS_ASSET_BUNDLE.LoadAsset("Offline") as GameObject;
            if (MANTAOS_OFFLINE_PAGE == null)
            {
                Log.Error("MANTAOS_OFFLINE_PAGE not found.");
            }

            MANTA_OS_MAIN_LAYOUT_PAGE = MANTAOS_ASSET_BUNDLE.LoadAsset("MainLayout") as GameObject;
            if (MANTA_OS_MAIN_LAYOUT_PAGE == null)
            {
                Log.Error("MANTA_OS_MAIN_LAYOUT_PAGE not found.");
            }
        }

        public static void LoadAssets()
        {
            LoadOSAssets();

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

            WALL_DECALS_ONE_NORMAL_MAP = ASSET_BUNDLE.LoadAsset("wall_decals_one_normals") as Texture;
            if (WALL_DECALS_ONE_NORMAL_MAP == null)
            {
                Log.Error("Manta WALL_DECALS_ONE_NORMAL_MAP not found.");
            }
            WALL_DECALS_ONE_EMISSIVE_MAP = ASSET_BUNDLE.LoadAsset("wall_decal_one_emissive") as Texture;
            if (WALL_DECALS_ONE_EMISSIVE_MAP == null)
            {
                Log.Error("Manta WALL_DECALS_ONE_EMISSIVE_MAP not found.");
            }

            WALL_DECALS_TWO_NORMAL_MAP = ASSET_BUNDLE.LoadAsset("wall_deacls_two_normals") as Texture;
            if (WALL_DECALS_TWO_NORMAL_MAP == null)
            {
                Log.Error("Manta WALL_DECALS_TWO_NORMAL_MAP not found.");
            }
            WALL_DECALS_TWO_EMISSIVE_MAP = ASSET_BUNDLE.LoadAsset("wall_decal_two_emissive") as Texture;
            if (WALL_DECALS_TWO_EMISSIVE_MAP == null)
            {
                Log.Error("Manta WALL_DECALS_TWO_EMISSIVE_MAP not found.");
            }

            WALL_DECALS_THREE_NORMAL_MAP = ASSET_BUNDLE.LoadAsset("wall_decal_three_normals") as Texture;
            if (WALL_DECALS_THREE_NORMAL_MAP == null)
            {
                Log.Error("Manta WALL_DECALS_THREE_NORMAL_MAP not found.");
            }

            EXTERIOR_DECALS_NORMAL_MAP = ASSET_BUNDLE.LoadAsset("exter_decals_normals") as Texture;
            if (EXTERIOR_DECALS_NORMAL_MAP == null)
            {
                Log.Error("Manta EXTERIOR_DECALS_NORMAL_MAP not found.");
            }

            Texture2D mantaPingIcon = ASSET_BUNDLE.LoadAsset("MantaPingIcon") as Texture2D;
            if (mantaPingIcon == null)
            {
                Log.Error("Manta MANTA_PING_ICON texture2d not found.");
            }
            MANTA_PING_ICON = Sprite.Create(mantaPingIcon, new Rect(0f, 0f, mantaPingIcon.width, mantaPingIcon.height), new Vector2(0.5f, 0.5f));
            if (MANTA_PING_ICON == null)
            {
                Log.Error("Manta MANTA_PING_ICON not found.");
            }
        }
    }
}
