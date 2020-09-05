using System.Collections.Generic;
using UnityEngine;

namespace Manta.Core.Factory.Materials.Impl
{
    public class ApplyExteriorWindshieldGlass : ApplyMaterial
    {
        protected override IEnumerable<string> TargetPaths => new[] {"Model/Exterior/WindshieldGlass"};
        protected override IEnumerable<string> MaterialTargets => new[] {"Glass"};
        protected override bool ApplyMarmosetShader { get; } = false;
        
        protected override void ApplyMaterialProperties(Material material)
        {
            material.SetColor(MaterialProperty.COLOR, new Color(0.06999998f, 0.91f, 1f, 0.05f));
        }
    }
}