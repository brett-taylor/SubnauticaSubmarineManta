using Submarines.Assets;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Odyssey.Utilities
{
    internal class Assets : AssetFile
    {
        internal static Assets Instance { get; set; }
        protected override string AssetBundleName => "odyssey";
        protected override string AssetBundleLocation => throw new NotImplementedException();
        protected override bool InAssetsFolder => true;
        protected override bool ShouldLogContents => false;
        protected override List<Type> TypesToLoad => new List<Type>() { typeof(GameObject), typeof(Texture) };

        public static GameObject ODYSSEY_EXTERIOR => Instance.GetAsset<GameObject>("Odyssey-Prefab");
        public static Texture BODY_NORMAL => Instance.GetAsset<Texture>("1_LP_Body_Normal");
        public static Texture BODY_SPEC => Instance.GetAsset<Texture>("1_LP_Body_SpecularSmoothness");
        public static Texture BODY_EXTRA_ONE_NORMAL => Instance.GetAsset<Texture>("1_LP_BodyExtra1_Normal");
        public static Texture BODY_EXTRA_ONE_SPEC => Instance.GetAsset<Texture>("1_LP_BodyExtra1_SpecularSmoothness");
        public static Texture BODY_EXTRA_TWO_NORMAL => Instance.GetAsset<Texture>("1_LP_BodyExtra2_Normal");
        public static Texture BODY_EXTRA_TWO_SPEC => Instance.GetAsset<Texture>("1_LP_BodyExtra2_SpecularSmoothness");
        public static Texture BODY_EXTRA_TWO_EMISSIVE => Instance.GetAsset<Texture>("1_LP_BodyExtra2_Emission");
        public static Texture CAMERA_NORMAL => Instance.GetAsset<Texture>("1_LP_Camera_Normal");
        public static Texture CAMERA_SPEC => Instance.GetAsset<Texture>("1_LP_Camera_SpecularSmoothness");
        public static Texture DECALS_NORMAL => Instance.GetAsset<Texture>("1_LP_Decals_Normal");
        public static Texture DECALS_SPEC => Instance.GetAsset<Texture>("1_LP_Decals_SpecularSmoothness");
        public static Texture HATCH_NORMAL => Instance.GetAsset<Texture>("1_LP_Hatch_Normal");
        public static Texture HATCH_SPEC => Instance.GetAsset<Texture>("1_LP_Hatch_SpecularSmoothness");
        public static Texture SENSORS_NORMAL => Instance.GetAsset<Texture>("1_LP_Sensors_Normal");
        public static Texture SENSORS_SPEC => Instance.GetAsset<Texture>("1_LP_Sensors_SpecularSmoothness");
    }
}
