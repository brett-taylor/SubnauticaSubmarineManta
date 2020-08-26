using UnityEngine;

namespace Manta.Core.Factory.Materials.Impl
{
    public class ApplyExteriorWingsMaterial : ApplyMaterial
    {
        protected override string TargetPath => "Model/Exterior/ExteriorModel";
        protected override string MaterialTarget => "Exterior-wings";
        
        protected override void ApplyMaterialProperties(Material material)
        {
        }
    }
}