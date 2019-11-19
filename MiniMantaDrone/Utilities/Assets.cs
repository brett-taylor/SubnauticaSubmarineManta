using Submarines.Assets;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MiniMantaDrone.Utilities
{
    internal class Assets : AssetFile
    {
        internal static Assets Instance { get; set; }
        protected override string AssetBundleName => "minimantadrone";
        protected override string AssetBundleLocation => throw new NotImplementedException();
        protected override bool InAssetsFolder => true;
        protected override bool ShouldLogContents => false;
        protected override List<Type> TypesToLoad => new List<Type>() { typeof(GameObject) };

        public static GameObject MINI_MANTA_DRONE_EXTERIOR => Instance.GetAsset<GameObject>("MiniMantaDrone");
    }
}
