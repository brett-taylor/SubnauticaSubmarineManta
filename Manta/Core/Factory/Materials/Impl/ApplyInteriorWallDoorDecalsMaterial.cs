using System.Collections.Generic;
using UnityEngine;

namespace Manta.Core.Factory.Materials.Impl
{
    public class ApplyInteriorWallDoorDecalsMaterial : ApplyMaterial
    {
        protected override IEnumerable<string> TargetPaths => new[] {"Model/Interior/WallDoorDecals"};
        protected override IEnumerable<string> MaterialTargets => new[] {"WallDecals"};

        protected override void ApplyMaterialProperties(Material material)
        {
            material.EnableKeyword("MARMO_SPECMAP");
            material.EnableKeyword("_ZWRITE_ON");

            material.SetColor(MaterialProperty.COLOR, Color.gray);
            material.SetFloat(MaterialProperty.SPEC_INT, 0f);
            material.SetFloat(MaterialProperty.SHININESS, 0f);
            material.SetFloat(MaterialProperty.FRESNEL, 0f);
            material.SetVector(MaterialProperty.SPEC_TEX_ST, new Vector4(1f, 1.0f, 0.0f, 0.0f));
        }
    }
}