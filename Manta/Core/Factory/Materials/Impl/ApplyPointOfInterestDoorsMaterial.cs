using System.Collections.Generic;
using UnityEngine;

namespace Manta.Core.Factory.Materials.Impl
{
    public class ApplyPointOfInterestDoorsMaterial : ApplyMaterial
    {
        protected override IEnumerable<string> TargetPaths => new[]
        {
            "PointsOfInterest/PlayerEntranceLeftFlap",
            "PointsOfInterest/PlayerEntranceRightFlap",
            "PointsOfInterest/VehicleEntranceLeftFlap",
            "PointsOfInterest/VehicleEntranceRightFlap"
        };
        protected override IEnumerable<string> MaterialTargets => new[] {"Exterior-doors"};

        protected override void ApplyMaterialProperties(Material material)
        {
            material.SetColor(MaterialProperty.COLOR, Color.white);
        }
    }
}