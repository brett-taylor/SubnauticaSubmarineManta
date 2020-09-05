using System.Collections.Generic;
using UnityEngine;

namespace Manta.Core.Factory.Materials.Impl
{
    public class ApplyPointOfInterestSteeringConsoleEmissionMaterial : ApplyMaterial
    {
        protected override IEnumerable<string> TargetPaths => new[] {"PointsOfInterest/SteeringConsole/Model"};
        protected override IEnumerable<string> MaterialTargets => new[] {"Console-emission"};

        protected override void ApplyMaterialProperties(Material material)
        {
            material.EnableKeyword("MARMO_EMISSION");
            
            material.SetColor(MaterialProperty.GLOW_COLOR, Color.magenta);
            material.SetFloat(MaterialProperty.GLOW_STRENGTH, 1f);
        }
    }
}