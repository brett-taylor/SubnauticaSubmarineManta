using Manta.Utilities;
using UnityEngine;

namespace Manta.Core.Factory.Materials.Impl
{
    public class ApplyExteriorWingsMaterial : ApplyMaterial
    {
        protected override string TargetPath => "Model/Exterior/ExteriorModel";
        protected override string MaterialTarget => "Exterior-wings";
        
        protected override void ApplyMaterialProperties(Material material)
        { 
            material.DisableKeyword("_EMISSION");
            material.EnableKeyword("MARMO_SPECMAP");
            material.EnableKeyword("_ZWRITE_ON");
            material.EnableKeyword("MARMO_EMISSION");
            
            material.SetColor(MaterialProperty.COLOR, Color.white);
            material.SetColor(MaterialProperty.SPEC_COLOR, Color.white);
            material.SetFloat(MaterialProperty.SPEC_INT, 1f);
            material.SetFloat(MaterialProperty.SHININESS, 6.5f);
            material.SetFloat(MaterialProperty.FRESNEL, 0f);
            material.SetTexture(MaterialProperty.SPEC_TEX, Assets.HULL_TWO_SPEC_MAP);
            material.SetVector(MaterialProperty.SPEC_TEX_ST, new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            material.SetTexture(MaterialProperty.BUMP_MAP, Assets.HULL_TWO_NORMAL_MAP);
            material.SetColor(MaterialProperty.GLOW_COLOR, Color.white);
            material.SetFloat(MaterialProperty.GLOW_STRENGTH, 1f);
            material.SetFloat(MaterialProperty.EMISSION_LM, 0f);
            material.SetVector(MaterialProperty.EMISSION_COLOR, Vector4.zero);
            material.SetTexture(MaterialProperty.ILLUM, Assets.HULL_TWO_EMISSIVE_MAP);
            material.SetVector(MaterialProperty.ILLUM_ST, new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            material.SetFloat(MaterialProperty.ENABLE_GLOW, 1.3f);
        }
    }
}