using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 * Built upon CCGould's/FCStudio system
 * https://github.com/ccgould/FCStudios_SubnauticaMods/blob/master/FCSCommon/Helpers/MaterialHelpers.cs
 */
namespace Submarines.Utilities.Helpers
{
    public static class MaterialHelper
    {
        private static Shader marmosetUberShader;
        public static Shader MARMOSET_UBER_SHADER
        {
            get
            {
                if (marmosetUberShader == null)
                {
                    marmosetUberShader = Shader.Find("MarmosetUBER");
                }

                return marmosetUberShader;
            }
        }

        private static void SetShader(Material material, Shader shader)
        {
            if (material.shader != shader)
            {
                material.shader = shader;
            }
        }

        private static void IterateMaterials(Renderer[] renderers, string materialName, Action<Material> iterationAction)
        {
            int materialsFoundCount = 0;
            foreach (Renderer renderer in renderers)
            {
                foreach (Material material in renderer.materials)
                {
                    if (material.name.StartsWith(materialName))
                    {
                        materialsFoundCount++;
                        iterationAction.Invoke(material);
                    }
                }
            }

            if (materialsFoundCount == 0)
            {
                string[] rendererNames = renderers.Select(r => r.name).ToArray();
                Log.Error($"No material ({materialName}) found on renders ({string.Join(",", rendererNames)})");
            }
        }

        public static void SetMarmosetShader(GameObject gameObject, string materialName)
        {
            Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>(true);
            IterateMaterials(renderers, materialName, material =>
            {
                material.shader = MARMOSET_UBER_SHADER;
            });
        }

        public static void ApplyEmissionShader(GameObject gameObject, string materialName, Texture texture, Color emissionColor, float emissionMuli = 1.0f)
        {
            Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>(true);
            IterateMaterials(renderers, materialName, material => 
            {
                SetShader(material, MARMOSET_UBER_SHADER);
                material.EnableKeyword("MARMO_EMISSION");
                material.SetVector("_EmissionColor", emissionColor * emissionMuli);
                material.SetTexture("_Illum", texture);
                material.SetVector("_Illum_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            });
        }

        public static void ApplyNormalShader(GameObject gameObject, string materialName, Texture texture)
        {
            Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>(true);
            IterateMaterials(renderers, materialName, material =>
            {
                SetShader(material, MARMOSET_UBER_SHADER);
                material.EnableKeyword("_NORMALMAP");
                material.SetTexture("_BumpMap", texture);
            });
        }

        public static void ChangeMaterialColor(GameObject gameObject, string materialName, Color color)
        {
            Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>(true);
            IterateMaterials(renderers, materialName, material =>
            {
                material.SetColor("_Color", color);
            });
        }

        public static void ApplyMetallicShader(GameObject gameObject, string materialName, Texture texture, float glossiness)
        {
            Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>(true);
            IterateMaterials(renderers, materialName, material =>
            {
                SetShader(material, MARMOSET_UBER_SHADER);
                material.EnableKeyword("_METALLICGLOSSMAP");
                material.SetColor("_Color", Color.white);
                material.SetTexture("_Metallic", texture);
                material.SetFloat("_Glossiness", glossiness);
            });
        }

        public static void ApplySpecShader(GameObject gameObject, string materialName, Texture texture, float specInt, float shininess)
        {
            Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>(true);
            IterateMaterials(renderers, materialName, material =>
            {
                SetShader(material, MARMOSET_UBER_SHADER);
                material.EnableKeyword("MARMO_SPECMAP");
                material.SetColor("_SpecColor", new Color(0.796875f, 0.796875f, 0.796875f, 0.796875f));
                material.SetFloat("_SpecInt", specInt);
                material.SetFloat("_Shininess", shininess);

                if (texture != null)
                {
                    material.SetTexture("_SpecTex", texture);
                }

                material.SetFloat("_Fresnel", 0f);
                material.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            });
        }

        public static void ApplyAlphaShader(GameObject gameObject, string materialName)
        {
            Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>(true);
            IterateMaterials(renderers, materialName, material =>
            {
                SetShader(material, MARMOSET_UBER_SHADER);
                material.EnableKeyword("_ZWRITE_ON");
                material.EnableKeyword("MARMO_ALPHA");
                material.EnableKeyword("MARMO_ALPHA_CLIP");
            });
        }

        public static void ApplyGlassShader(GameObject gameObject, string materialName)
        {
            var shader = Shader.Find("Standard");
            Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>(true);
            IterateMaterials(renderers, materialName, material =>
            {
                SetShader(material, MARMOSET_UBER_SHADER);
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.EnableKeyword("MARMO_SIMPLE_GLASS");
                material.EnableKeyword("MARMO_SPECMAP");
                material.EnableKeyword("WBOIT");
                material.EnableKeyword("_ZWRITE_ON");
                material.SetFloat("_Mode", 3);
                material.SetFloat("_DstBlend", 1);
                material.SetFloat("_SrcBlend2", 0);
                material.SetFloat("_AddDstBlend", 1f);
                material.SetFloat("_ZWrite", 0);
                material.SetFloat("_Cutoff", 0);
                material.SetFloat("_IBLreductionAtNight", 0.92f);
                material.SetFloat("_EnableSimpleGlass", 1f);
                material.SetFloat("_MarmoSpecEnum", 2f);
                material.SetColor("_SpecColor", new Color(1.000f, 1.000f, 1.000f, 1.000f));
                material.SetFloat("_Shininess", 6.2f);
                material.SetFloat("_Fresnel", 4f);
                material.renderQueue = 3101;
            });
        }

        public static void ApplyPrecursorShader(GameObject gameObject, string materialName, Texture normalMap, Texture metalicmap, float glossiness)
        {
            var shader = Shader.Find("UWE/Marmoset/IonCrystal");
            Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>(true);
            IterateMaterials(renderers, materialName, material =>
            {
                SetShader(material, shader);
                material.EnableKeyword("_NORMALMAP");
                material.EnableKeyword("_METALLICGLOSSMAP");
                material.SetTexture("_BumpMap", normalMap);
                material.SetColor("_BorderColor", new Color(0.14f, 0.55f, 0.43f));
                material.SetColor("_Color", new Color(0.33f, 0.83f, 0.17f));
                material.SetColor("_DetailsColor", new Color(0.42f, 0.85f, 0.26f));
                material.SetTexture("_MarmoSpecEnum", metalicmap);
                material.SetFloat("_Glossiness", glossiness);
            });
        }

        public static void ApplyColorMaskShader(GameObject gameObject, string materialName, Shader shader, Texture normalMap, Texture metalicmap, Texture maskmap, float glossiness)
        {
            Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>(true);
            IterateMaterials(renderers, materialName, material =>
            {
                SetShader(material, shader);
                material.EnableKeyword("_NORMALMAP");
                material.EnableKeyword("_METALLICGLOSSMAP");
                material.SetTexture("_Normal", normalMap);
                material.SetTexture("_MaskTex", maskmap);
                material.SetColor("_Color1", Color.green);
                material.SetColor("_Color2", Color.white);
                material.SetColor("_Color3", Color.white);
                material.SetTexture("_Metallic", metalicmap);
                material.SetFloat("_Glossiness", glossiness);
            });
        }

        public static void ReplaceEmissionTexture(string materialName, Texture replacementTexture, GameObject gameObject)
        {
            Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>(true);
            IterateMaterials(renderers, materialName, material =>
            {
                material.SetTexture("_Illum", replacementTexture);
            });
        }
    }
}
