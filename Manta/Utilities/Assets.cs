using Submarines.Assets;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Manta.Utilities
{
    internal class Assets : AssetFile
    {
        internal static Assets Instance { get; set; }
        protected override string AssetBundleName => "manta";
        protected override string AssetBundleLocation => throw new NotImplementedException();
        protected override bool InAssetsFolder => true;
        protected override bool ShouldLogContents => false;
        protected override List<Type> TypesToLoad => new List<Type>() { typeof(GameObject), typeof(Texture), typeof(Texture2D), typeof(Sprite) };

        public static GameObject MANTA_EXTERIOR => Instance.GetAsset<GameObject>("manta-prefab");
        public static Texture HULL_ONE_NORMAL_MAP => Instance.GetAsset<Texture>("Hull1_normal");
        public static Texture HULL_ONE_SPEC_MAP => Instance.GetAsset<Texture>("DefaultSpecMap");
        public static Texture HULL_TWO_NORMAL_MAP => Instance.GetAsset<Texture>("Hull2_normal");
        public static Texture HULL_TWO_SPEC_MAP => Instance.GetAsset<Texture>("Hull2_Diffuse");
        public static Texture HULL_TWO_EMISSIVE_MAP => Instance.GetAsset<Texture>("Hull2_Emit");
        public static Texture HULL_THREE_NORMAL_MAP => Instance.GetAsset<Texture>("Hull3_normal");
        public static Texture HULL_THREE_SPEC_MAP => Instance.GetAsset<Texture>("Hull3_Diffuse");
        public static Texture HULL_FOUR_NORMAL_MAP => Instance.GetAsset<Texture>("MantaFinv2interiorrevision2Reunwrappedexterior_Hull4_Normal");
        public static Texture HULL_FOUR_SPEC_MAP => Instance.GetAsset<Texture>("DefaultSpecMap");
        public static Texture HULL_FOUR_EMISSIVE_MAP => Instance.GetAsset<Texture>("Hull4_Emit");
        public static Texture FLOOR_NORMAL_MAP => Instance.GetAsset<Texture>("HexNormal");
        public static Texture LIGHT_EMISSIVE_MAP => Instance.GetAsset<Texture>("LightDecal");
        public static Texture WALL_DECALS_ONE_NORMAL_MAP => Instance.GetAsset<Texture>("wall_decals_one_normals");
        public static Texture WALL_DECALS_ONE_EMISSIVE_MAP => Instance.GetAsset<Texture>("wall_decal_one_emissive");
        public static Texture WALL_DECALS_TWO_NORMAL_MAP => Instance.GetAsset<Texture>("wall_deacls_two_normals");
        public static Texture WALL_DECALS_TWO_EMISSIVE_MAP => Instance.GetAsset<Texture>("wall_decal_two_emissive");
        public static Texture WALL_DECALS_THREE_NORMAL_MAP => Instance.GetAsset<Texture>("wall_decal_three_normals");
        public static Texture EXTERIOR_DECALS_NORMAL_MAP => Instance.GetAsset<Texture>("exter_decals_normals");
        public static Sprite PING_ICON { get; private set; }

        internal Assets() : base()
        {
            Texture2D pingIcon = GetAsset<Texture2D>("MantaPingIcon");
            PING_ICON = Sprite.Create(pingIcon, new Rect(0f, 0f, pingIcon.width, pingIcon.height), new Vector2(0.5f, 0.5f));
        }
    }
}
