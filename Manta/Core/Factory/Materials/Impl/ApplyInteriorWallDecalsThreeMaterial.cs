using System.Collections.Generic;
using Manta.Utilities;
using UnityEngine;

namespace Manta.Core.Factory.Materials.Impl
{
    public class ApplyInteriorWallDecalsThreeMaterial : ApplyMaterial
    {
        protected override IEnumerable<string> TargetPaths => new[] {"Model/Interior/InteriorDecals"};
        protected override IEnumerable<string> MaterialTargets => new[] {"WallDecalsThree"};

        protected override void ApplyMaterialProperties(Material material)
        {
            material.EnableKeyword("_ZWRITE_ON");
            material.EnableKeyword("MARMO_ALPHA");
            material.EnableKeyword("MARMO_ALPHA_CLIP");
            material.SetTexture(MaterialProperty.BUMP_MAP, Assets.WALL_DECALS_THREE_NORMAL_MAP);
        }
    }
}