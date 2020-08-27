using System;
using System.Linq;
using Manta.Utilities;
using UnityEngine;

namespace Manta.Core.Factory.Materials
{
    public abstract class ApplyMaterial
    {
        private static readonly Shader MARMOSET_SHADER = Shader.Find("MarmosetUBER");
        private static readonly string INSTANCE_POSTFIX = " (Instance)";
        protected abstract string TargetPath { get; }
        protected abstract string MaterialTarget { get; }
        protected virtual bool ApplyMarmosetShader { get; } = true;
        
        public void Apply(GameObject gameObject)
        {
            try
            {
                var target = gameObject.transform.Find(TargetPath).GetComponent<Renderer>().materials.ToList()
                    .Find(m => m.name.Replace(INSTANCE_POSTFIX, "") == MaterialTarget);

                if (ApplyMarmosetShader)
                    target.shader = MARMOSET_SHADER;
                
                ApplyMaterialProperties(target);
            }
            catch (Exception)
            {
                Log.Error($"[ApplyMaterial] Did not find a material called {MaterialTarget} on {gameObject.name}/{TargetPath}");
            }
        }

        protected abstract void ApplyMaterialProperties(Material material);
    }
}