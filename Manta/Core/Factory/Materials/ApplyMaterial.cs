using System;
using System.Collections.Generic;
using System.Linq;
using Manta.Utilities;
using UnityEngine;

namespace Manta.Core.Factory.Materials
{
    public abstract class ApplyMaterial
    {
        private static readonly Shader MARMOSET_SHADER = Shader.Find("MarmosetUBER");
        private static readonly string INSTANCE_POSTFIX = " (Instance)";

        protected abstract IEnumerable<string> TargetPaths { get; }
        protected abstract IEnumerable<string> MaterialTargets { get; }
        protected virtual bool ApplyMarmosetShader { get; } = true;

        public void Apply(GameObject gameObject)
        {
            try
            {
                TargetPaths.SelectMany(targetPath => gameObject.transform.Find(targetPath).GetComponentInChildren<Renderer>().materials)
                    .Where(material => MaterialTargets.Contains(material.name.Replace(INSTANCE_POSTFIX, "")))
                    .ForEach(ApplyShaderAndMaterialProperties);
            }
            catch (Exception)
            {
                Log.Error($"[ApplyMaterial] Did not find a material called {MaterialTargets} on {gameObject.name}/[{string.Join("; ", TargetPaths)}]");
            }
        }

        private void ApplyShaderAndMaterialProperties(Material material)
        {
            if (ApplyMarmosetShader)
                material.shader = MARMOSET_SHADER;

            ApplyMaterialProperties(material);
        }

        protected abstract void ApplyMaterialProperties(Material material);
    }
}