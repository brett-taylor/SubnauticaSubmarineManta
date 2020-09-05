using System.Collections.Generic;
using Manta.Utilities;
using UnityEngine;

namespace Manta.Core.Factory.Materials.Impl
{
    public class ApplyInteriorWallDecalsOneMaterial : ApplyMaterial
    {
        protected override IEnumerable<string> TargetPaths => new[] {"Model/Interior/InteriorDecals"};
        protected override IEnumerable<string> MaterialTargets => new[] {"WallDecalsOne"};

        protected override void ApplyMaterialProperties(Material material)
        {
            material.EnableKeyword("_ZWRITE_ON");
            material.EnableKeyword("MARMO_ALPHA");
            material.EnableKeyword("MARMO_ALPHA_CLIP");
            material.EnableKeyword("MARMO_EMISSION");
            material.SetTexture(MaterialProperty.BUMP_MAP, Assets.WALL_DECALS_ONE_NORMAL_MAP);
            material.SetColor(MaterialProperty.GLOW_COLOR, Color.white);
            material.SetFloat(MaterialProperty.GLOW_STRENGTH, 1f);
            material.SetFloat(MaterialProperty.EMISSION_LM, 0f);
            material.SetVector(MaterialProperty.EMISSION_COLOR, Vector4.zero);
            material.SetTexture(MaterialProperty.ILLUM, Assets.WALL_DECALS_ONE_EMISSIVE_MAP);
            material.SetVector(MaterialProperty.ILLUM_ST, new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            material.SetFloat(MaterialProperty.ENABLE_GLOW, 1.3f);
        }
    }
}