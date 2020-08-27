using UnityEngine;

namespace Manta.Core.Factory.Materials.Impl
{
    public class ApplyExteriorWindshieldGlass : ApplyMaterial
    {
        protected override string TargetPath => "Model/Exterior/WindshieldGlass";
        protected override string MaterialTarget => "Glass";
        protected override bool ApplyMarmosetShader { get; } = false;
        
        protected override void ApplyMaterialProperties(Material material)
        {
            material.SetColor(MaterialProperty.COLOR, new Color(0.06999998f, 0.91f, 1f, 0.05f));
        }
    }
}