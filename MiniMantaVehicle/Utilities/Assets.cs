using Submarines.Assets;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MiniMantaVehicle.Utilities
{
    internal class Assets : AssetFile
    {
        internal static Assets Instance { get; set; }
        protected override string AssetBundleName => "minimantavehicle";
        protected override string AssetBundleLocation => throw new System.NotImplementedException();
        protected override bool InAssetsFolder => true;
        protected override bool ShouldLogContents => false;
        protected override List<Type> TypesToLoad => new List<Type>() { typeof(GameObject) };

        public static GameObject MINI_MANTA_VEHICLE_EXTERIOR => Instance.GetAsset<GameObject>("MiniMantaVehicle");
    }
}
