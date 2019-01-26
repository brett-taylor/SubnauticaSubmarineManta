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
            GameObject model = submarine.FindChild("Model");
            Material body = model.FindChild("Body_LP").GetComponent<MeshRenderer>().material;
            Material bodyExtraOne = model.FindChild("Fin1_LP").GetComponent<MeshRenderer>().material;
            Material bodyExtraTwo = model.FindChild("ConningTower1_LP").GetComponent<MeshRenderer>().material;
            Material camera = model.FindChild("Camera_LP").GetComponent<MeshRenderer>().material;
            Material decals = model.FindChild("Decals_LP").GetComponent<MeshRenderer>().material;
            Material hatch = model.FindChild("DoorMainHinge_LP").GetComponent<MeshRenderer>().material;
            Material sensors = model.FindChild("SensorAntenna1_LP").GetComponent<MeshRenderer>().material;

            body.shader = shader;
            body.EnableKeyword("MARMO_SPECMAP");
            body.EnableKeyword("_ZWRITE_ON");
            body.SetColor("_Color", Color.white);
            body.SetColor("_SpecColor", Color.white);
            body.SetFloat("_SpecInt", 1f);
            body.SetFloat("_Shininess", 6.5f);
            body.SetTexture("_SpecTex", OdysseyAssetLoader.DEFAULT_SPEC_MAP);
            body.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            body.SetTexture("_BumpMap", OdysseyAssetLoader.BODY_NORMAL);

            bodyExtraOne.shader = shader;
            bodyExtraOne.EnableKeyword("MARMO_SPECMAP");
            bodyExtraOne.EnableKeyword("_ZWRITE_ON");
            bodyExtraOne.SetColor("_Color", Color.white);
            bodyExtraOne.SetColor("_SpecColor", Color.white);
            bodyExtraOne.SetFloat("_SpecInt", 1f);
            bodyExtraOne.SetFloat("_Shininess", 6.5f);
            bodyExtraOne.SetTexture("_SpecTex", OdysseyAssetLoader.DEFAULT_SPEC_MAP);
            bodyExtraOne.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            bodyExtraOne.SetTexture("_BumpMap", OdysseyAssetLoader.BODY_EXTRA_ONE_NORMAL);

            bodyExtraTwo.shader = shader;
            bodyExtraTwo.EnableKeyword("MARMO_SPECMAP");
            bodyExtraTwo.EnableKeyword("_ZWRITE_ON");
            bodyExtraTwo.EnableKeyword("MARMO_EMISSION");
            bodyExtraTwo.SetColor("_Color", Color.white);
            bodyExtraTwo.SetColor("_SpecColor", Color.white);
            bodyExtraTwo.SetFloat("_SpecInt", 1f);
            bodyExtraTwo.SetFloat("_Shininess", 6.5f);
            bodyExtraTwo.SetTexture("_SpecTex", OdysseyAssetLoader.BODY_EXTRA_TWO_SPEC);
            bodyExtraTwo.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            bodyExtraTwo.SetTexture("_BumpMap", OdysseyAssetLoader.BODY_EXTRA_TWO_NORMAL);
            bodyExtraTwo.SetColor("_GlowColor", Color.white);
            bodyExtraTwo.SetFloat("_GlowStrength", 1f);
            bodyExtraTwo.SetFloat("_EmissionLM", 0f);
            bodyExtraTwo.SetVector("_EmissionColor", Vector4.zero);
            bodyExtraTwo.SetTexture("_Illum", OdysseyAssetLoader.BODY_EXTRA_TWO_EMISSIVE);
            bodyExtraTwo.SetVector("_Illum_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            bodyExtraTwo.SetFloat("_EnableGlow", 1.3f);

            camera.shader = shader;
            camera.EnableKeyword("MARMO_SPECMAP");
            camera.EnableKeyword("_ZWRITE_ON");
            camera.SetColor("_Color", Color.white);
            camera.SetColor("_SpecColor", Color.white);
            camera.SetFloat("_SpecInt", 1f);
            camera.SetFloat("_Shininess", 6.5f);
            camera.SetTexture("_SpecTex", OdysseyAssetLoader.CAMERA_SPEC);
            camera.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            camera.SetTexture("_BumpMap", OdysseyAssetLoader.CAMERA_NORMAL);

            decals.shader = shader;
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

            hatch.shader = shader;
            hatch.EnableKeyword("MARMO_SPECMAP");
            hatch.EnableKeyword("_ZWRITE_ON");
            hatch.SetColor("_Color", Color.white);
            hatch.SetColor("_SpecColor", Color.white);
            hatch.SetFloat("_SpecInt", 1f);
            hatch.SetFloat("_Shininess", 6.5f);
            hatch.SetTexture("_SpecTex", OdysseyAssetLoader.DEFAULT_SPEC_MAP);
            hatch.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            hatch.SetTexture("_BumpMap", OdysseyAssetLoader.HATCH_NORMAL);

            sensors.shader = shader;
            sensors.EnableKeyword("MARMO_SPECMAP");
            sensors.EnableKeyword("_ZWRITE_ON");
            sensors.SetColor("_Color", Color.white);
            sensors.SetColor("_SpecColor", Color.white);
            sensors.SetFloat("_SpecInt", 1f);
            sensors.SetFloat("_Shininess", 6.5f);
            sensors.SetTexture("_SpecTex", OdysseyAssetLoader.DEFAULT_SPEC_MAP);
            sensors.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            sensors.SetTexture("_BumpMap", OdysseyAssetLoader.SENSORS_NORMAL);

            model.FindChild("BallMount_LP").GetComponent<MeshRenderer>().material = bodyExtraOne;
            model.FindChild("Blades_LP").GetComponent<MeshRenderer>().material = bodyExtraTwo;
            model.FindChild("Body_LP").GetComponent<MeshRenderer>().material = body;
            model.FindChild("Camera_LP").GetComponent<MeshRenderer>().material = camera;
            model.FindChild("CameraDock_LP").GetComponent<MeshRenderer>().material = sensors;
            model.FindChild("ConningTower1_LP").GetComponent<MeshRenderer>().material = bodyExtraTwo;
            model.FindChild("ConningTower2_LP").GetComponent<MeshRenderer>().material = bodyExtraTwo;
            model.FindChild("Decals_LP").GetComponent<MeshRenderer>().material = decals;
            model.FindChild("DoorMainHinge_LP").GetComponent<MeshRenderer>().material = hatch;
            model.FindChild("DoorMainHinge_LP001").GetComponent<MeshRenderer>().material = hatch;
            model.FindChild("Fin1_LP").GetComponent<MeshRenderer>().material = bodyExtraOne;
            model.FindChild("Fin2_LP").GetComponent<MeshRenderer>().material = bodyExtraOne;
            model.FindChild("HeadLight_LP").GetComponent<MeshRenderer>().material = bodyExtraTwo;
            model.FindChild("HeadLight_LP001").GetComponent<MeshRenderer>().material = bodyExtraTwo;
            model.FindChild("HeadLight_LP002").GetComponent<MeshRenderer>().material = bodyExtraTwo;
            model.FindChild("HeadLight_LP003").GetComponent<MeshRenderer>().material = bodyExtraTwo;
            model.FindChild("HeadLight_LP004").GetComponent<MeshRenderer>().material = bodyExtraTwo;
            model.FindChild("HeadLight_LP005").GetComponent<MeshRenderer>().material = bodyExtraTwo;
            model.FindChild("HeadLight_LP006").GetComponent<MeshRenderer>().material = bodyExtraTwo;
            model.FindChild("HeadLight_LP007").GetComponent<MeshRenderer>().material = bodyExtraTwo;
            model.FindChild("Motor_LP").GetComponent<MeshRenderer>().material = bodyExtraOne;
            model.FindChild("PropShaft_LP").GetComponent<MeshRenderer>().material = bodyExtraOne;
            model.FindChild("RadarDish_LP").GetComponent<MeshRenderer>().material = sensors;
            model.FindChild("SensorAntenna1_LP").GetComponent<MeshRenderer>().material = sensors;
            model.FindChild("SensorAntenna2_LP").GetComponent<MeshRenderer>().material = sensors;
            model.FindChild("SensorAntenna3_LP").GetComponent<MeshRenderer>().material = sensors;
            model.FindChild("SensorBase_LP").GetComponent<MeshRenderer>().material = sensors;
            model.FindChild("Shroud_LP").GetComponent<MeshRenderer>().material = bodyExtraTwo;
            model.FindChild("ShroudSupport_LP").GetComponent<MeshRenderer>().material = bodyExtraOne;
            model.FindChild("SideFins_LP").GetComponent<MeshRenderer>().material = bodyExtraOne;
            model.FindChild("Thruster_LP").GetComponent<MeshRenderer>().material = bodyExtraTwo;
            model.FindChild("ThrusterLeg_LP").GetComponent<MeshRenderer>().material = bodyExtraTwo;
        }
    }
}
