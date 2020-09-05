using System.Collections.Generic;
using UnityEngine;

namespace Manta.Core.Factory.Materials.Impl
{
    public class ApplyInteriorPlayerHatchBaseMaterial : ApplyMaterial
    {
        protected override IEnumerable<string> TargetPaths => new[] {"Model/Interior/PlayerHatch"};
        protected override IEnumerable<string> MaterialTargets => new[] {"Hatch Base"};

        protected override void ApplyMaterialProperties(Material material)
        {
            material.EnableKeyword("MARMO_SPECMAP");
            material.EnableKeyword("_ZWRITE_ON");
            
            material.SetColor(MaterialProperty.COLOR, Color.white);
            material.SetColor(MaterialProperty.SPEC_COLOR, Color.white);
            material.SetFloat(MaterialProperty.SPEC_INT, 0f);
            material.SetFloat(MaterialProperty.SHININESS, 8f);
            material.SetFloat(MaterialProperty.FRESNEL, 0.8f);
        }
    }
}