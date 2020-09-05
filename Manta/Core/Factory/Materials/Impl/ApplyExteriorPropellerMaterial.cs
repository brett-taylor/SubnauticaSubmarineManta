using System.Collections.Generic;
using UnityEngine;

namespace Manta.Core.Factory.Materials.Impl
{
    public class ApplyExteriorPropellerMaterial : ApplyMaterial
    {
        protected override IEnumerable<string> TargetPaths => new[] {"Model/Exterior/Propeller"};
        protected override IEnumerable<string> MaterialTargets => new[] {"Propeller"};

        protected override void ApplyMaterialProperties(Material material)
        {
            material.EnableKeyword("MARMO_SPECMAP");
            material.EnableKeyword("_ZWRITE_ON");
            material.SetColor(MaterialProperty.COLOR, Color.white);
            material.SetColor(MaterialProperty.SPEC_COLOR, Color.white);
            material.SetFloat(MaterialProperty.SPEC_INT, 6.7f);
            material.SetFloat(MaterialProperty.SHININESS, 8f);
            material.SetVector(MaterialProperty.SPEC_TEX_ST, new Vector4(1f, 1.0f, 0.0f, 0.0f));
            material.SetFloat(MaterialProperty.FRESNEL, 0.87f);
        }
    }
}