using System.Collections.Generic;
using UnityEngine;

namespace Manta.Core.Factory.Materials.Impl
{
    public class ApplyInteriorDoorWaysMaterial : ApplyMaterial
    {
        protected override IEnumerable<string> TargetPaths => new[] {"Model/Interior/Doorways"};
        protected override IEnumerable<string> MaterialTargets => new[] {"Dividers"};

        protected override void ApplyMaterialProperties(Material material)
        {  
            material.EnableKeyword("MARMO_SPECMAP");
            material.EnableKeyword("_ZWRITE_ON");
            material.SetColor(MaterialProperty.COLOR, Color.white);
            material.SetColor(MaterialProperty.SPEC_COLOR, Color.white);
            material.SetFloat(MaterialProperty.SPEC_INT, 1f);
            material.SetFloat(MaterialProperty.SHININESS, 6.5f);
        }
    }
}