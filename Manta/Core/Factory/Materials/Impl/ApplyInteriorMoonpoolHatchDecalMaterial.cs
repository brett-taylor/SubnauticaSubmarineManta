using System.Collections.Generic;
using UnityEngine;

namespace Manta.Core.Factory.Materials.Impl
{
    public class ApplyInteriorMoonpoolHatchDecalMaterial : ApplyMaterial
    {
        protected override IEnumerable<string> TargetPaths => new[] {"Model/Interior/MoonPool/MoonPoolHatchDecal"};
        protected override IEnumerable<string> MaterialTargets => new[] {"Moonpool-hatch-decal"};

        protected override void ApplyMaterialProperties(Material material)
        {
            material.SetColor(MaterialProperty.COLOR, new Color(0.2f, 0.2f, 0.2f, 1f));
        }
    }
}