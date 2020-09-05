using System.Collections.Generic;
using UnityEngine;

namespace Manta.Core.Factory.Materials.Impl
{
    public class ApplyPointsOfInterestPowerCellsMaterial : ApplyMaterial
    {
        protected override IEnumerable<string> TargetPaths => new[] {"PointsOfInterest/UpgradesAndBatteries/Batteries"};
        protected override IEnumerable<string> MaterialTargets => new[] {"Power-cells"};

        protected override void ApplyMaterialProperties(Material material)
        {
        }
    }
}