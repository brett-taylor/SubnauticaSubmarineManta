using UnityEngine;

namespace Submarines.Utilities.Extensions
{
    /**
    * Extension methods for the Material object.
    */
    public static class MaterialExtensions
    {
        public static void PrintAllMarmosetUBERShaderProperties(this Material material)
        {
            Utilities.Log.Print(string.Format("Property {0} {1}", "_Color", material.GetVector("_Color")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_Mode", material.GetFloat("_Mode")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_SrcBlend", material.GetFloat("_SrcBlend")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_DstBlend", material.GetFloat("_DstBlend")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_SrcBlend2", material.GetFloat("_SrcBlend2")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_DstBlend2", material.GetFloat("_DstBlend2")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_AddSrcBlend", material.GetFloat("_AddSrcBlend")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_AddDstBlend", material.GetFloat("_AddDstBlend")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_AddSrcBlend2", material.GetFloat("_AddDstBlend2")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_EnableMisc", material.GetFloat("_EnableMisc")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_MyCullVariable", material.GetFloat("_MyCullVariable")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_ZWrite", material.GetFloat("_ZWrite")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_ZOffset", material.GetFloat("_ZOffset")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_EnableCutOff", material.GetFloat("_EnableCutOff")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_Cutoff", material.GetFloat("_Cutoff")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_EnableDitherAlpha", material.GetFloat("_EnableDitherAlpha")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_EnableVrFadeOut", material.GetFloat("_EnableVrFadeOut")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_VrFadeMask", material.GetTexture("_VrFadeMask")?.name), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_EnableLighting", material.GetFloat("_EnableLighting")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_IBLreductionAtNight", material.GetFloat("_IBLreductionAtNight")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_EnableSimpleGlass", material.GetFloat("_EnableSimpleGlass")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_EnableVertexColor", material.GetFloat("_EnableVertexColor")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_EnableSchoolFish", material.GetFloat("_EnableSchoolFish")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_EnableMainMaps", material.GetFloat("_EnableMainMaps")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_MainTex", material.GetTexture("_MainTex")?.name), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_BumpMap", material.GetTexture("_BumpMap")?.name), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_MarmoSpecEnum", material.GetFloat("_MarmoSpecEnum")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_SpecTex", material.GetTexture("_SpecTex")?.name), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_SpecColor", material.GetColor("_SpecColor")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_SpecInt", material.GetFloat("_SpecInt")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_Shininess", material.GetFloat("_Shininess")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_Fresnel", material.GetFloat("_Fresnel")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_EnableGlow", material.GetFloat("_EnableGlow")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_GlowColor", material.GetVector("_GlowColor")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_Illum", material.GetTexture("_Illum")?.name), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_GlowUVfromVC", material.GetFloat("_GlowUVfromVC")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_GlowStrength", material.GetFloat("_GlowStrength")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_GlowStrengthNight", material.GetFloat("_GlowStrengthNight")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_EmissionLM", material.GetFloat("_EmissionLM")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_EmissionLMNight", material.GetFloat("_EmissionLMNight")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_EnableDetailMaps", material.GetFloat("_EnableDetailMaps")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_DetailDiffuseTex", material.GetTexture("_DetailDiffuseTex")?.name), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_DetailBumpTex", material.GetTexture("_DetailBumpTex")?.name), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_DetailSpecTex", material.GetTexture("_DetailSpecTex")?.name), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_DetailIntensities", material.GetVector("_DetailIntensities")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_EnableLightmap", material.GetFloat("_EnableLightmap")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_Lightmap", material.GetTexture("_Lightmap")?.name), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_LightmapStrength", material.GetFloat("_LightmapStrength")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_Enable3Color", material.GetFloat("_Enable3Color")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_MultiColorMask", material.GetTexture("_MultiColorMask")?.name), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_Color2", material.GetVector("_Color2")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_Color3", material.GetVector("_Color3")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_SpecColor2", material.GetVector("_SpecColor2")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_SpecColor3", material.GetVector("_SpecColor3")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "FX", material.GetFloat("FX")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_DeformMap", material.GetTexture("_DeformMap")?.name), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_DeformParams", material.GetVector("_DeformParams")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_FillSack", material.GetFloat("_FillSack")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_OverlayStrength", material.GetFloat("_OverlayStrength")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_GlowScrollColor", material.GetVector("_GlowScrollColor")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_GlowScrollMask", material.GetTexture("_GlowScrollMask")?.name), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_Hypnotize", material.GetFloat("_Hypnotize")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_ScrollColor", material.GetVector("_ScrollColor")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_ColorStrength", material.GetVector("_ColorStrength")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_ScrollTex", material.GetTexture("_ScrollTex")?.name), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_GlowMask", material.GetTexture("_GlowMask")?.name), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_GlowMaskSpeed", material.GetVector("_GlowMaskSpeed")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_ScrollTex2", material.GetTexture("_ScrollTex2")?.name), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_ScrollSpeed", material.GetVector("_ScrollSpeed")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_NoiseTex", material.GetTexture("_NoiseTex")?.name), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_DetailsColor", material.GetVector("_DetailsColor")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_SquaresColor", material.GetVector("_SquaresColor")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_SquaresTile", material.GetFloat("_SquaresTile")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_SquaresSpeed", material.GetFloat("_SquaresSpeed")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_SquaresIntensityPow", material.GetFloat("_SquaresIntensityPow")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_NoiseSpeed", material.GetVector("_NoiseSpeed")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_FakeSSSparams", material.GetVector("_FakeSSSparams")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_FakeSSSSpeed", material.GetVector("_FakeSSSSpeed")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_BorderColor", material.GetVector("_BorderColor")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_Built", material.GetFloat("_Built")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_BuildParams", material.GetVector("_BuildParams")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_BuildLinear", material.GetFloat("_BuildLinear")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_EmissiveTex", material.GetTexture("_EmissiveTex")?.name), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_NoiseThickness", material.GetFloat("_NoiseThickness")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_NoiseStr", material.GetFloat("_NoiseStr")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "FX_Vertex", material.GetFloat("FX_Vertex")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_Scale", material.GetVector("_Scale")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_Frequency", material.GetVector("_Frequency")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_Speed", material.GetVector("_Speed")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_AnimMask", material.GetTexture("_AnimMask")?.name), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_ObjectUp", material.GetVector("_ObjectUp")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_Fallof", material.GetFloat("_Fallof")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_RopeGravity", material.GetFloat("_RopeGravity")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_minYpos", material.GetFloat("_minYpos")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_maxYpos", material.GetFloat("_maxYpos")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_EnableBurst", material.GetFloat("_EnableBurst")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_DispTex", material.GetTexture("_DispTex")?.name), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_Displacement", material.GetFloat("_Displacement")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_BurstStrength", material.GetFloat("_BurstStrength")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_Range", material.GetVector("_Range")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_ClipRange", material.GetFloat("_ClipRange")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_EnableInfection", material.GetFloat("_EnableInfection")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_EnablePlayerInfection", material.GetFloat("_EnablePlayerInfection")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_InfectionHeightStrength", material.GetFloat("_InfectionHeightStrength")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_InfectionScale", material.GetVector("_InfectionScale")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_InfectionOffset", material.GetVector("_InfectionOffset")), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_InfectionNoiseTex", material.GetTexture("_InfectionNoiseTex")?.name), false);
            Utilities.Log.Print(string.Format("Property {0} {1}", "_InfectionSpeed", material.GetVector("_InfectionSpeed")), false);

            Utilities.Log.Print("Shader keywords: " + material.shaderKeywords.Length, true);
            foreach(string keyword in material.shaderKeywords)
            {
                Utilities.Log.Print("Keyword: " + keyword, false);
            }

            Utilities.Log.Print("Render queue: " + material.renderQueue, false);
            Utilities.Log.Print("Done print");
        }
    }
}
