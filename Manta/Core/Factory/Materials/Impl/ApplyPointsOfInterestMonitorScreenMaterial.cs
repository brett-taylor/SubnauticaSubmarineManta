using System.Collections.Generic;
using UnityEngine;

namespace Manta.Core.Factory.Materials.Impl
{
    public class ApplyPointsOfInterestMonitorScreenMaterial : ApplyMaterial
    {
        protected override IEnumerable<string> TargetPaths => new[]
        {
            "PointsOfInterest/UpgradesAndBatteries/HelmScreen",
            "PointsOfInterest/UpgradesAndBatteries/UpgradeScreen"
        };
        protected override IEnumerable<string> MaterialTargets => new[] {"Screen"};

        protected override void ApplyMaterialProperties(Material material)
        {
            material.SetColor(MaterialProperty.COLOR, new Color(0.2f, 0.2f, 0.2f, 1f));
        }
    }
}