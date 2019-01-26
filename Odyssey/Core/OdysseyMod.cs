using Odyssey.Utilities;
using SMLHelper.V2.Assets;
using Submarines.Utilities.Extension;
using UnityEngine;

namespace Odyssey.Core
{
    /**
     * The Odyssey
     */
    public class OdysseyMod : Spawnable
    {
        public static TechType ODYSSEY_TECH_TYPE = new OdysseyMod().TechType;
        public override string AssetsFolder => EntryPoint.ASSET_FOLDER_LOCATION;
        public override string IconFileName => "MantaIcon.png";

        public OdysseyMod() : base("SubmarineOdyssey", "Odyssey", "A smaller version of the Cyclops")
        {
        }

        public override GameObject GetGameObject()
        {
            return CreateOdyssey();
        }

        /*
         * The first part of setting up the odyssey. If the components are self contained and do not rely on other components
         * adding it here works fine.
         */
        public static GameObject CreateOdyssey()
        {
            GameObject submarine = Object.Instantiate(OdysseyAssetLoader.ODYSSEY_EXTERIOR);
            ApplyMaterials(submarine);

            SkyApplier skyApplier = submarine.GetOrAddComponent<SkyApplier>();
            skyApplier.renderers = submarine.GetComponentsInChildren<Renderer>();
            skyApplier.anchorSky = Skies.Auto;

            submarine.GetOrAddComponent<VFXSurface>();
            submarine.GetOrAddComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Global;

            Rigidbody rb = submarine.GetOrAddComponent<Rigidbody>();
            rb.angularDrag = 1f;
            rb.mass = 12000f;
            rb.useGravity = false;
            rb.centerOfMass = new Vector3(-0.1f, 0.8f, -1.7f);

            WorldForces forces = submarine.GetOrAddComponent<WorldForces>();
            forces.aboveWaterDrag = 0f;
            forces.aboveWaterGravity = 9.81f;
            forces.handleDrag = true;
            forces.handleGravity = true;
            forces.underwaterDrag = 0.5f;
            forces.underwaterGravity = 0;

            submarine.GetOrAddComponent<TechTag>().type = ODYSSEY_TECH_TYPE;
            submarine.GetOrAddComponent<PrefabIdentifier>().ClassId = "SubmarineOdyssey";
            return submarine;
        }

        private static void ApplyMaterials(GameObject submarine)
        {
            Shader shader = Shader.Find("MarmosetUBER");
            Renderer[] renderers = submarine.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                foreach (Material mat in renderer.materials)
                {
                    if (renderer.gameObject.name.ToLower().Contains("glass") == false)
                    {
                        mat.shader = shader;
                    }
                }
            }

            Material windshield = submarine.FindChild("Model").FindChild("BallMount_LP").GetComponent<MeshRenderer>().material;
            windshield.EnableKeyword("MARMO_SPECMAP");
            windshield.EnableKeyword("_ZWRITE_ON");
            windshield.SetColor("_Color", Color.white);
            windshield.SetColor("_SpecColor", Color.white);
            windshield.SetFloat("_SpecInt", 1f);
            windshield.SetFloat("_Shininess", 6.5f);
            windshield.SetTexture("_SpecTex", OdysseyAssetLoader.DEFAULT_SPEC_MAP);
            windshield.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            windshield.SetTexture("_BumpMap", OdysseyAssetLoader.BODY_EXTRA_ONE_NORMAL);

            Material propellerBlades = submarine.FindChild("Model").FindChild("Blades_LP").GetComponent<MeshRenderer>().material;
            propellerBlades.EnableKeyword("MARMO_SPECMAP");
            propellerBlades.EnableKeyword("_ZWRITE_ON");
            propellerBlades.EnableKeyword("MARMO_EMISSION");
            propellerBlades.SetColor("_Color", Color.white);
            propellerBlades.SetColor("_SpecColor", Color.white);
            propellerBlades.SetFloat("_SpecInt", 1f);
            propellerBlades.SetFloat("_Shininess", 6.5f);
            propellerBlades.SetTexture("_SpecTex", OdysseyAssetLoader.BODY_EXTRA_TWO_SPEC);
            propellerBlades.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            propellerBlades.SetTexture("_BumpMap", OdysseyAssetLoader.BODY_EXTRA_TWO_NORMAL);
            propellerBlades.SetColor("_GlowColor", Color.white);
            propellerBlades.SetFloat("_GlowStrength", 1f);
            propellerBlades.SetFloat("_EmissionLM", 0f);
            propellerBlades.SetVector("_EmissionColor", Vector4.zero);
            propellerBlades.SetTexture("_Illum", OdysseyAssetLoader.BODY_EXTRA_TWO_EMISSIVE);
            propellerBlades.SetVector("_Illum_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            propellerBlades.SetFloat("_EnableGlow", 1.3f);

            Material body = submarine.FindChild("Model").FindChild("Body_LP").GetComponent<MeshRenderer>().material;
            body.EnableKeyword("MARMO_SPECMAP");
            body.EnableKeyword("_ZWRITE_ON");
            body.SetColor("_Color", Color.white);
            body.SetColor("_SpecColor", Color.white);
            body.SetFloat("_SpecInt", 1f);
            body.SetFloat("_Shininess", 6.5f);
            body.SetTexture("_SpecTex", OdysseyAssetLoader.DEFAULT_SPEC_MAP);
            body.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            body.SetTexture("_BumpMap", OdysseyAssetLoader.BODY_NORMAL);

            Material camera = submarine.FindChild("Model").FindChild("Camera_LP").GetComponent<MeshRenderer>().material;
            camera.EnableKeyword("MARMO_SPECMAP");
            camera.EnableKeyword("_ZWRITE_ON");
            camera.SetColor("_Color", Color.white);
            camera.SetColor("_SpecColor", Color.white);
            camera.SetFloat("_SpecInt", 1f);
            camera.SetFloat("_Shininess", 6.5f);
            camera.SetTexture("_SpecTex", OdysseyAssetLoader.CAMERA_SPEC);
            camera.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            camera.SetTexture("_BumpMap", OdysseyAssetLoader.CAMERA_NORMAL);

            Material cameraDock = submarine.FindChild("Model").FindChild("CameraDock_LP").GetComponent<MeshRenderer>().material;
            cameraDock.EnableKeyword("MARMO_SPECMAP");
            cameraDock.EnableKeyword("_ZWRITE_ON");
            cameraDock.SetColor("_Color", Color.white);
            cameraDock.SetColor("_SpecColor", Color.white);
            cameraDock.SetFloat("_SpecInt", 1f);
            cameraDock.SetFloat("_Shininess", 6.5f);
            cameraDock.SetTexture("_SpecTex", OdysseyAssetLoader.DEFAULT_SPEC_MAP);
            cameraDock.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            cameraDock.SetTexture("_BumpMap", OdysseyAssetLoader.SENSORS_NORMAL);

            Material conningTowerOne = submarine.FindChild("Model").FindChild("ConningTower1_LP").GetComponent<MeshRenderer>().material;
            conningTowerOne.EnableKeyword("MARMO_SPECMAP");
            conningTowerOne.EnableKeyword("_ZWRITE_ON");
            conningTowerOne.EnableKeyword("MARMO_EMISSION");
            conningTowerOne.SetColor("_Color", Color.white);
            conningTowerOne.SetColor("_SpecColor", Color.white);
            conningTowerOne.SetFloat("_SpecInt", 1f);
            conningTowerOne.SetFloat("_Shininess", 6.5f);
            conningTowerOne.SetTexture("_SpecTex", OdysseyAssetLoader.BODY_EXTRA_TWO_SPEC);
            conningTowerOne.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            conningTowerOne.SetTexture("_BumpMap", OdysseyAssetLoader.BODY_EXTRA_TWO_NORMAL);
            conningTowerOne.SetColor("_GlowColor", Color.white);
            conningTowerOne.SetFloat("_GlowStrength", 1f);
            conningTowerOne.SetFloat("_EmissionLM", 0f);
            conningTowerOne.SetVector("_EmissionColor", Vector4.zero);
            conningTowerOne.SetTexture("_Illum", OdysseyAssetLoader.BODY_EXTRA_TWO_EMISSIVE);
            conningTowerOne.SetVector("_Illum_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            conningTowerOne.SetFloat("_EnableGlow", 1.3f);

            Material conningTowerTwo = submarine.FindChild("Model").FindChild("ConningTower2_LP").GetComponent<MeshRenderer>().material;
            conningTowerTwo.EnableKeyword("MARMO_SPECMAP");
            conningTowerTwo.EnableKeyword("_ZWRITE_ON");
            conningTowerTwo.EnableKeyword("MARMO_EMISSION");
            conningTowerTwo.SetColor("_Color", Color.white);
            conningTowerTwo.SetColor("_SpecColor", Color.white);
            conningTowerTwo.SetFloat("_SpecInt", 1f);
            conningTowerTwo.SetFloat("_Shininess", 6.5f);
            conningTowerTwo.SetTexture("_SpecTex", OdysseyAssetLoader.BODY_EXTRA_TWO_SPEC);
            conningTowerTwo.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            conningTowerTwo.SetTexture("_BumpMap", OdysseyAssetLoader.BODY_EXTRA_TWO_NORMAL);
            conningTowerTwo.SetColor("_GlowColor", Color.white);
            conningTowerTwo.SetFloat("_GlowStrength", 1f);
            conningTowerTwo.SetFloat("_EmissionLM", 0f);
            conningTowerTwo.SetVector("_EmissionColor", Vector4.zero);
            conningTowerTwo.SetTexture("_Illum", OdysseyAssetLoader.BODY_EXTRA_TWO_EMISSIVE);
            conningTowerTwo.SetVector("_Illum_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            conningTowerTwo.SetFloat("_EnableGlow", 1.3f);

            Material decals = submarine.FindChild("Model").FindChild("Decals_LP").GetComponent<MeshRenderer>().material;
            decals.EnableKeyword("MARMO_SPECMAP");
            decals.EnableKeyword("_ZWRITE_ON");
            decals.EnableKeyword("MARMO_ALPHA");
            decals.EnableKeyword("MARMO_ALPHA_CLIP");
            decals.SetColor("_Color", Color.white);
            decals.SetColor("_SpecColor", Color.white);
            decals.SetFloat("_SpecInt", 1f);
            decals.SetFloat("_Shininess", 6.5f);
            decals.SetTexture("_SpecTex", OdysseyAssetLoader.DECALS_SPEC);
            decals.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            decals.SetTexture("_BumpMap", OdysseyAssetLoader.DECALS_NORMAL);

            Material doorMainHinge = submarine.FindChild("Model").FindChild("DoorMainHinge_LP").GetComponent<MeshRenderer>().material;
            doorMainHinge.EnableKeyword("MARMO_SPECMAP");
            doorMainHinge.EnableKeyword("_ZWRITE_ON");
            doorMainHinge.SetColor("_Color", Color.white);
            doorMainHinge.SetColor("_SpecColor", Color.white);
            doorMainHinge.SetFloat("_SpecInt", 1f);
            doorMainHinge.SetFloat("_Shininess", 6.5f);
            doorMainHinge.SetTexture("_SpecTex", OdysseyAssetLoader.DEFAULT_SPEC_MAP);
            doorMainHinge.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            doorMainHinge.SetTexture("_BumpMap", OdysseyAssetLoader.HATCH_NORMAL);

            Material doorMainHingeTwo = submarine.FindChild("Model").FindChild("DoorMainHinge_LP").GetComponent<MeshRenderer>().material;
            doorMainHingeTwo.EnableKeyword("MARMO_SPECMAP");
            doorMainHingeTwo.EnableKeyword("_ZWRITE_ON");
            doorMainHingeTwo.SetColor("_Color", Color.white);
            doorMainHingeTwo.SetColor("_SpecColor", Color.white);
            doorMainHingeTwo.SetFloat("_SpecInt", 1f);
            doorMainHingeTwo.SetFloat("_Shininess", 6.5f);
            doorMainHingeTwo.SetTexture("_SpecTex", OdysseyAssetLoader.DEFAULT_SPEC_MAP);
            doorMainHingeTwo.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            doorMainHingeTwo.SetTexture("_BumpMap", OdysseyAssetLoader.HATCH_NORMAL);

            Material finTop = submarine.FindChild("Model").FindChild("Fin1_LP").GetComponent<MeshRenderer>().material;
            finTop.EnableKeyword("MARMO_SPECMAP");
            finTop.EnableKeyword("_ZWRITE_ON");
            finTop.SetColor("_Color", Color.white);
            finTop.SetColor("_SpecColor", Color.white);
            finTop.SetFloat("_SpecInt", 1f);
            finTop.SetFloat("_Shininess", 6.5f);
            finTop.SetTexture("_SpecTex", OdysseyAssetLoader.DEFAULT_SPEC_MAP);
            finTop.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            finTop.SetTexture("_BumpMap", OdysseyAssetLoader.BODY_EXTRA_ONE_NORMAL);

            Material finBottom = submarine.FindChild("Model").FindChild("Fin2_LP").GetComponent<MeshRenderer>().material;
            finBottom.EnableKeyword("MARMO_SPECMAP");
            finBottom.EnableKeyword("_ZWRITE_ON");
            finBottom.SetColor("_Color", Color.white);
            finBottom.SetColor("_SpecColor", Color.white);
            finBottom.SetFloat("_SpecInt", 1f);
            finBottom.SetFloat("_Shininess", 6.5f);
            finBottom.SetTexture("_SpecTex", OdysseyAssetLoader.DEFAULT_SPEC_MAP);
            finBottom.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            finBottom.SetTexture("_BumpMap", OdysseyAssetLoader.BODY_EXTRA_ONE_NORMAL);

            Material motor = submarine.FindChild("Model").FindChild("Motor_LP").GetComponent<MeshRenderer>().material;
            motor.EnableKeyword("MARMO_SPECMAP");
            motor.EnableKeyword("_ZWRITE_ON");
            motor.SetColor("_Color", Color.white);
            motor.SetColor("_SpecColor", Color.white);
            motor.SetFloat("_SpecInt", 1f);
            motor.SetFloat("_Shininess", 6.5f);
            motor.SetTexture("_SpecTex", OdysseyAssetLoader.DEFAULT_SPEC_MAP);
            motor.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            motor.SetTexture("_BumpMap", OdysseyAssetLoader.BODY_EXTRA_ONE_NORMAL);

            Material propShaft = submarine.FindChild("Model").FindChild("PropShaft_LP").GetComponent<MeshRenderer>().material;
            propShaft.EnableKeyword("MARMO_SPECMAP");
            propShaft.EnableKeyword("_ZWRITE_ON");
            propShaft.SetColor("_Color", Color.white);
            propShaft.SetColor("_SpecColor", Color.white);
            propShaft.SetFloat("_SpecInt", 1f);
            propShaft.SetFloat("_Shininess", 6.5f);
            propShaft.SetTexture("_SpecTex", OdysseyAssetLoader.DEFAULT_SPEC_MAP);
            propShaft.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            propShaft.SetTexture("_BumpMap", OdysseyAssetLoader.BODY_EXTRA_ONE_NORMAL);

            Material radarDish = submarine.FindChild("Model").FindChild("RadarDish_LP").GetComponent<MeshRenderer>().material;
            radarDish.EnableKeyword("MARMO_SPECMAP");
            radarDish.EnableKeyword("_ZWRITE_ON");
            radarDish.SetColor("_Color", Color.white);
            radarDish.SetColor("_SpecColor", Color.white);
            radarDish.SetFloat("_SpecInt", 1f);
            radarDish.SetFloat("_Shininess", 6.5f);
            radarDish.SetTexture("_SpecTex", OdysseyAssetLoader.DEFAULT_SPEC_MAP);
            radarDish.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            radarDish.SetTexture("_BumpMap", OdysseyAssetLoader.SENSORS_SPEC);

            Material sensorAtennaOne = submarine.FindChild("Model").FindChild("SensorAntenna1_LP").GetComponent<MeshRenderer>().material;
            sensorAtennaOne.EnableKeyword("MARMO_SPECMAP");
            sensorAtennaOne.EnableKeyword("_ZWRITE_ON");
            sensorAtennaOne.SetColor("_Color", Color.white);
            sensorAtennaOne.SetColor("_SpecColor", Color.white);
            sensorAtennaOne.SetFloat("_SpecInt", 1f);
            sensorAtennaOne.SetFloat("_Shininess", 6.5f);
            sensorAtennaOne.SetTexture("_SpecTex", OdysseyAssetLoader.DEFAULT_SPEC_MAP);
            sensorAtennaOne.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            sensorAtennaOne.SetTexture("_BumpMap", OdysseyAssetLoader.SENSORS_NORMAL);

            Material sensorAtennaTwo = submarine.FindChild("Model").FindChild("SensorAntenna2_LP").GetComponent<MeshRenderer>().material;
            sensorAtennaTwo.EnableKeyword("MARMO_SPECMAP");
            sensorAtennaTwo.EnableKeyword("_ZWRITE_ON");
            sensorAtennaTwo.SetColor("_Color", Color.white);
            sensorAtennaTwo.SetColor("_SpecColor", Color.white);
            sensorAtennaTwo.SetFloat("_SpecInt", 1f);
            sensorAtennaTwo.SetFloat("_Shininess", 6.5f);
            sensorAtennaTwo.SetTexture("_SpecTex", OdysseyAssetLoader.DEFAULT_SPEC_MAP);
            sensorAtennaTwo.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            sensorAtennaTwo.SetTexture("_BumpMap", OdysseyAssetLoader.SENSORS_NORMAL);

            Material sensorAtennaThree = submarine.FindChild("Model").FindChild("SensorAntenna3_LP").GetComponent<MeshRenderer>().material;
            sensorAtennaThree.EnableKeyword("MARMO_SPECMAP");
            sensorAtennaThree.EnableKeyword("_ZWRITE_ON");
            sensorAtennaThree.SetColor("_Color", Color.white);
            sensorAtennaThree.SetColor("_SpecColor", Color.white);
            sensorAtennaThree.SetFloat("_SpecInt", 1f);
            sensorAtennaThree.SetFloat("_Shininess", 6.5f);
            sensorAtennaThree.SetTexture("_SpecTex", OdysseyAssetLoader.DEFAULT_SPEC_MAP);
            sensorAtennaThree.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            sensorAtennaThree.SetTexture("_BumpMap", OdysseyAssetLoader.SENSORS_NORMAL);

            Material sensorBase = submarine.FindChild("Model").FindChild("SensorBase_LP").GetComponent<MeshRenderer>().material;
            sensorBase.EnableKeyword("MARMO_SPECMAP");
            sensorBase.EnableKeyword("_ZWRITE_ON");
            sensorBase.SetColor("_Color", Color.white);
            sensorBase.SetColor("_SpecColor", Color.white);
            sensorBase.SetFloat("_SpecInt", 1f);
            sensorBase.SetFloat("_Shininess", 6.5f);
            sensorBase.SetTexture("_SpecTex", OdysseyAssetLoader.DEFAULT_SPEC_MAP);
            sensorBase.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            sensorBase.SetTexture("_BumpMap", OdysseyAssetLoader.SENSORS_NORMAL);

            Material engineShroud = submarine.FindChild("Model").FindChild("Shroud_LP").GetComponent<MeshRenderer>().material;
            engineShroud.EnableKeyword("MARMO_SPECMAP");
            engineShroud.EnableKeyword("_ZWRITE_ON");
            engineShroud.EnableKeyword("MARMO_EMISSION");
            engineShroud.SetColor("_Color", Color.white);
            engineShroud.SetColor("_SpecColor", Color.white);
            engineShroud.SetFloat("_SpecInt", 1f);
            engineShroud.SetFloat("_Shininess", 6.5f);
            engineShroud.SetTexture("_SpecTex", OdysseyAssetLoader.BODY_EXTRA_TWO_SPEC);
            engineShroud.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            engineShroud.SetTexture("_BumpMap", OdysseyAssetLoader.BODY_EXTRA_TWO_NORMAL);
            engineShroud.SetColor("_GlowColor", Color.white);
            engineShroud.SetFloat("_GlowStrength", 1f);
            engineShroud.SetFloat("_EmissionLM", 0f);
            engineShroud.SetVector("_EmissionColor", Vector4.zero);
            engineShroud.SetTexture("_Illum", OdysseyAssetLoader.BODY_EXTRA_TWO_EMISSIVE);
            engineShroud.SetVector("_Illum_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            engineShroud.SetFloat("_EnableGlow", 1.3f);

            Material engineShroudSupport = submarine.FindChild("Model").FindChild("ShroudSupport_LP").GetComponent<MeshRenderer>().material;
            engineShroudSupport.EnableKeyword("MARMO_SPECMAP");
            engineShroudSupport.EnableKeyword("_ZWRITE_ON");
            engineShroudSupport.SetColor("_Color", Color.white);
            engineShroudSupport.SetColor("_SpecColor", Color.white);
            engineShroudSupport.SetFloat("_SpecInt", 1f);
            engineShroudSupport.SetFloat("_Shininess", 6.5f);
            engineShroudSupport.SetTexture("_SpecTex", OdysseyAssetLoader.DEFAULT_SPEC_MAP);
            engineShroudSupport.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            engineShroudSupport.SetTexture("_BumpMap", OdysseyAssetLoader.BODY_EXTRA_ONE_NORMAL);

            Material sideFins = submarine.FindChild("Model").FindChild("SideFins_LP").GetComponent<MeshRenderer>().material;
            sideFins.EnableKeyword("MARMO_SPECMAP");
            sideFins.EnableKeyword("_ZWRITE_ON");
            sideFins.SetColor("_Color", Color.white);
            sideFins.SetColor("_SpecColor", Color.white);
            sideFins.SetFloat("_SpecInt", 1f);
            sideFins.SetFloat("_Shininess", 6.5f);
            sideFins.SetTexture("_SpecTex", OdysseyAssetLoader.DEFAULT_SPEC_MAP);
            sideFins.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            sideFins.SetTexture("_BumpMap", OdysseyAssetLoader.BODY_EXTRA_ONE_NORMAL);

            Material thrusters = submarine.FindChild("Model").FindChild("Thruster_LP").GetComponent<MeshRenderer>().material;
            thrusters.EnableKeyword("MARMO_SPECMAP");
            thrusters.EnableKeyword("_ZWRITE_ON");
            thrusters.EnableKeyword("MARMO_EMISSION");
            thrusters.SetColor("_Color", Color.white);
            thrusters.SetColor("_SpecColor", Color.white);
            thrusters.SetFloat("_SpecInt", 1f);
            thrusters.SetFloat("_Shininess", 6.5f);
            thrusters.SetTexture("_SpecTex", OdysseyAssetLoader.BODY_EXTRA_TWO_SPEC);
            thrusters.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            thrusters.SetTexture("_BumpMap", OdysseyAssetLoader.BODY_EXTRA_TWO_NORMAL);
            thrusters.SetColor("_GlowColor", Color.white);
            thrusters.SetFloat("_GlowStrength", 1f);
            thrusters.SetFloat("_EmissionLM", 0f);
            thrusters.SetVector("_EmissionColor", Vector4.zero);
            thrusters.SetTexture("_Illum", OdysseyAssetLoader.BODY_EXTRA_TWO_EMISSIVE);
            thrusters.SetVector("_Illum_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            thrusters.SetFloat("_EnableGlow", 1.3f);

            Material thrustersLegs = submarine.FindChild("Model").FindChild("ThrusterLeg_LP").GetComponent<MeshRenderer>().material;
            thrustersLegs.EnableKeyword("MARMO_SPECMAP");
            thrustersLegs.EnableKeyword("_ZWRITE_ON");
            thrustersLegs.EnableKeyword("MARMO_EMISSION");
            thrustersLegs.SetColor("_Color", Color.white);
            thrustersLegs.SetColor("_SpecColor", Color.white);
            thrustersLegs.SetFloat("_SpecInt", 1f);
            thrustersLegs.SetFloat("_Shininess", 6.5f);
            thrustersLegs.SetTexture("_SpecTex", OdysseyAssetLoader.BODY_EXTRA_TWO_SPEC);
            thrustersLegs.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            thrustersLegs.SetTexture("_BumpMap", OdysseyAssetLoader.BODY_EXTRA_TWO_NORMAL);
            thrustersLegs.SetColor("_GlowColor", Color.white);
            thrustersLegs.SetFloat("_GlowStrength", 1f);
            thrustersLegs.SetFloat("_EmissionLM", 0f);
            thrustersLegs.SetVector("_EmissionColor", Vector4.zero);
            thrustersLegs.SetTexture("_Illum", OdysseyAssetLoader.BODY_EXTRA_TWO_EMISSIVE);
            thrustersLegs.SetVector("_Illum_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            thrustersLegs.SetFloat("_EnableGlow", 1.3f);
        }
    }
}
