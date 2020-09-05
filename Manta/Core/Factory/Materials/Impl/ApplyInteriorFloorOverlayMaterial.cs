using System.Collections.Generic;
using UnityEngine;

namespace Manta.Core.Factory.Materials.Impl
{
    public class ApplyInteriorFloorOverlayMaterial : ApplyMaterial
    {
        protected override IEnumerable<string> TargetPaths => new[] {"Model/Interior/FloorOverlay"};
        protected override IEnumerable<string> MaterialTargets => new[] {"Floor-Overlay"};

        protected override void ApplyMaterialProperties(Material material)
        {
            material.EnableKeyword("MARMO_SPECMAP");
            material.EnableKeyword("_ZWRITE_ON");
            
            material.SetColor(MaterialProperty.COLOR, Color.white);
            material.SetColor(MaterialProperty.SPEC_COLOR, Color.white);
            material.SetFloat(MaterialProperty.FRESNEL, 0f);
            material.SetFloat(MaterialProperty.SPEC_INT, 0f);
            material.SetFloat(MaterialProperty.SHININESS, 0f);
        }
    }
}