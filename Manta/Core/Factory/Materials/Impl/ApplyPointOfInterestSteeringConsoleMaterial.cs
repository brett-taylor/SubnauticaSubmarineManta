using System.Collections.Generic;
using UnityEngine;

namespace Manta.Core.Factory.Materials.Impl
{
    public class ApplyPointOfInterestSteeringConsoleMaterial : ApplyMaterial
    {
        protected override IEnumerable<string> TargetPaths => new[] {"PointsOfInterest/SteeringConsole/Model"};
        protected override IEnumerable<string> MaterialTargets => new[]
        {
            "Console-screen",
            "Console-screen-bevel",
            "Console-body",
        };

        protected override void ApplyMaterialProperties(Material material)
        {
        }
    }
}