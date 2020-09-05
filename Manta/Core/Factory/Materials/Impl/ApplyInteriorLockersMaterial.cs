using System.Collections.Generic;
using UnityEngine;

namespace Manta.Core.Factory.Materials.Impl
{
    public class ApplyInteriorLockersMaterial : ApplyMaterial
    {
        protected override IEnumerable<string> TargetPaths => new[] {"Model/Interior/Lockers"};
        protected override IEnumerable<string> MaterialTargets => new[] {"Lockers"};

        protected override void ApplyMaterialProperties(Material material)
        {
        }
    }
}