using System.Collections.Generic;
using UnityEngine;

namespace Manta.Core.Factory.Materials.Impl
{
    public class ApplyInteriorWallsMaterial : ApplyMaterial
    {
        protected override IEnumerable<string> TargetPaths => new[] {"Model/Interior/InternalWalls"};
        protected override IEnumerable<string> MaterialTargets => new[] {"Interior-Walls"};

        protected override void ApplyMaterialProperties(Material material)
        {  
            material.EnableKeyword("MARMO_SPECMAP");
            material.EnableKeyword("_ZWRITE_ON");
            
            material.SetColor(MaterialProperty.COLOR, Color.white);
            material.SetColor(MaterialProperty.SPEC_COLOR, Color.white);
            material.SetFloat(MaterialProperty.SPEC_INT, 0.5f);
            material.SetFloat(MaterialProperty.SHININESS, 1f);
        }
    }
}