using UnityEngine;

namespace Manta.Core.Factory.Materials.Impl
{
    public class ApplyExteriorBodyMaterial : ApplyMaterial
    {
        protected override string TargetPath => "Model/Exterior/ExteriorModel";
        protected override string MaterialTarget => "Exterior-middleBody";
        
        protected override void ApplyMaterialProperties(Material material)
        {
        }
    }
}