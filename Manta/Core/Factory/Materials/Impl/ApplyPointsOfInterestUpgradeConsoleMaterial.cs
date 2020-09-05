using System.Collections.Generic;
using UnityEngine;

namespace Manta.Core.Factory.Materials.Impl
{
    public class ApplyPointsOfInterestUpgradeConsoleMaterial : ApplyMaterial
    {
        protected override IEnumerable<string> TargetPaths => new[] {"PointsOfInterest/UpgradesAndBatteries/UpgradeConsole"};
        protected override IEnumerable<string> MaterialTargets => new[] {"Upgrade-console"};

        protected override void ApplyMaterialProperties(Material material)
        {
        }
    }
}