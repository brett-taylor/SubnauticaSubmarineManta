using System;
using System.Collections.Generic;

namespace Submarines.Assets
{
    internal class Assets : AssetFile
    {
        internal static Assets Instance { get; set; }
        protected override string AssetBundleName => "submarine";
        protected override string AssetBundleLocation => throw new System.NotImplementedException();
        protected override bool InAssetsFolder => true;
        protected override bool ShouldLogContents => false;
        protected override List<Type> TypesToLoad => new List<Type>();
    }
}
