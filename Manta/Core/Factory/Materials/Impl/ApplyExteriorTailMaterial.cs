using UnityEngine;

namespace Manta.Core.Factory.Materials.Impl
{
    public class ApplyExteriorTailMaterial : ApplyMaterial
    {
        protected override string TargetPath => "Model/Exterior/ExteriorModel";
        protected override string MaterialTarget => "Exterior-tail";
        
        protected override void ApplyMaterialProperties(Material material)
        {
        }
    }
}