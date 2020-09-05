using System.Collections.Generic;
using Manta.Utilities;
using UnityEngine;

namespace Manta.Core.Factory.Materials.Impl
{
    public class ApplyExteriorDecalsMaterial : ApplyMaterial
    {
        protected override IEnumerable<string> TargetPaths => new[] {"Model/Exterior/ExteriorDecals"};
        protected override IEnumerable<string> MaterialTargets => new[]
        {
            "ExteriorDecalsOne",
            "ExteriorDecalsTwo",
            "ExteriorDecalsThree"
        };
        
        protected override void ApplyMaterialProperties(Material material)
        {
            material.EnableKeyword("_ZWRITE_ON");
            material.EnableKeyword("MARMO_ALPHA");
            material.EnableKeyword("MARMO_ALPHA_CLIP");
            material.SetColor(MaterialProperty.COLOR, Color.white);
            material.SetTexture(MaterialProperty.BUMP_MAP, Assets.EXTERIOR_DECALS_NORMAL_MAP);
        }
    }
}