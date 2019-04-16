using Odyssey.Components;
using Odyssey.Utilities;
using SMLHelper.V2.Assets;
using Submarines.Content;
using Submarines.DefaultCyclopsContent;
using Submarines.Engine;
using Submarines.Miscellaneous;
using Submarines.Movement;
using Submarines.Utilities.Extension;
using Submarines.Water;
using UnityEngine;

namespace Odyssey.Core
{
    /**
     * The Odyssey
     */
    public class OdysseyMod : Spawnable
    {
        public static TechType ODYSSEY_TECH_TYPE = new OdysseyMod().TechType;
        public override string AssetsFolder => EntryPoint.MOD_FOLDER_NAME + EntryPoint.ASSET_FOLDER_NAME;
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
            CyclopsDefaultAssets.LoadDefaultCyclopsContent();

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
            submarine.GetOrAddComponent<SubmarineDuplicateFixer>();
            submarine.GetOrAddComponent<OdysseySubmarine>();
            submarine.GetOrAddComponent<MovementStabiliser>();
            submarine.GetOrAddComponent<WaterClipProxyModified>().Initialise();
            submarine.GetOrAddComponent<OdysseyTemporarySteeringHUD>();

            submarine.GetOrAddComponent<OdysseySerializationFixer>();
            return submarine;
        }

        /**
         * If the component requires other custom components then do it here.
         * Read the comment on Components.OdyseeySeializationFixer if you wish to understand why this horrible system exists.
         */
        public static void SetUpOdyssey(GameObject submarine)
        {
            OdysseySubmarine odysseySubmarine = submarine.GetComponent<OdysseySubmarine>();
            Transform applyForceLocation = submarine.FindChild("PointsOfInterest").FindChild("ForceAppliedLocation").transform;
            MovementController movementController = submarine.GetOrAddComponent<MovementController>();
            movementController.ApplyForceLocation = applyForceLocation;

            GameObject doorLeft = submarine.FindChild("PointsOfInterest").FindChild("LeftDoorFlap");
            doorLeft.GetComponentInChildren<HingeJoint>().connectedBody = submarine.GetComponent<Rigidbody>();
            HingeJointDoor hingeDoorLeft = doorLeft.GetOrAddComponent<HingeJointDoor>();
            hingeDoorLeft.OverwriteTargetVelocity = true;
            hingeDoorLeft.TargetVelocity = 130f;
            hingeDoorLeft.TriggerToEverything = false;
            hingeDoorLeft.TriggerToPlayer = true;
            hingeDoorLeft.TriggerToVehicles = false;
            hingeDoorLeft.OpenSound = CyclopsDefaultAssets.PLAYER_HATCH_OPEN;
            hingeDoorLeft.CloseSound = CyclopsDefaultAssets.PLAYER_HATCH_CLOSE;

            GameObject doorRight = submarine.FindChild("PointsOfInterest").FindChild("RightDoorFlap");
            doorRight.GetComponentInChildren<HingeJoint>().connectedBody = submarine.GetComponent<Rigidbody>();
            HingeJointDoor hingeDoorRight = doorRight.GetOrAddComponent<HingeJointDoor>();
            hingeDoorRight.OverwriteTargetVelocity = true;
            hingeDoorRight.TargetVelocity = 130f;
            hingeDoorRight.TriggerToEverything = false;
            hingeDoorRight.TriggerToPlayer = true;
            hingeDoorRight.TriggerToVehicles = false;
            hingeDoorRight.OpenSound = CyclopsDefaultAssets.PLAYER_HATCH_OPEN;
            hingeDoorRight.CloseSound = CyclopsDefaultAssets.PLAYER_HATCH_CLOSE;

            GameObject entrancePosition = submarine.FindChild("PointsOfInterest").FindChild("EntranceTeleportSpot");
            GameObject entranceHatch = submarine.FindChild("PointsOfInterest").FindChild("Enter");
            EntranceHatch entranceHatchTeleport = entranceHatch.GetOrAddComponent<EntranceHatch>();
            entranceHatchTeleport.HoverText = "Board Odyssey";
            entranceHatchTeleport.HoverHandReticle = HandReticle.IconType.Hand;
            entranceHatchTeleport.TeleportTarget = entrancePosition;
            entranceHatchTeleport.Submarine = odysseySubmarine;
            entranceHatchTeleport.EnteringSubmarine = true;

            GameObject leavePosition = submarine.FindChild("PointsOfInterest").FindChild("ExitTeleportSpot");
            GameObject leaveHatch = submarine.FindChild("PointsOfInterest").FindChild("Exit");
            EntranceHatch leaveHatchTeleport = leaveHatch.GetOrAddComponent<EntranceHatch>();
            leaveHatchTeleport.HoverText = "Disembark Odyssey";
            leaveHatchTeleport.HoverHandReticle = HandReticle.IconType.Hand;
            leaveHatchTeleport.TeleportTarget = leavePosition;
            leaveHatchTeleport.Submarine = odysseySubmarine;
            leaveHatchTeleport.EnteringSubmarine = false;

            GameObject steeringConsolePOI = submarine.FindChild("PointsOfInterest").FindChild("SteeringConsole");
            GameObject playerParentWhilePiloting = submarine.FindChild("PointsOfInterest").FindChild("SteeringConsole").FindChild("PlayerLockedWhileSteeringPosition");
            SteeringConsole steeringConsole = steeringConsolePOI.GetOrAddComponent<SteeringConsole>();
            steeringConsole.MovementController = submarine.GetComponent<MovementController>();
            steeringConsole.ParentWhilePilotingGO = playerParentWhilePiloting;
            steeringConsole.LeftHandIKTarget = null;
            steeringConsole.RightHandIKTarget = null;
            steeringConsole.Submarine = odysseySubmarine;

            CyclopsCollisionSounds collisionSounds = submarine.GetOrAddComponent<CyclopsCollisionSounds>();

            MovementData normalSpeedMovementData = new MovementData
            {
                ForwardAccelerationSpeed = 10f,
                BackwardsAccelerationSpeed = 8f,
                AscendDescendSpeed = 15f,
                RotationSpeed = 0.7f
            };

            EngineManager engineManager = submarine.GetOrAddComponent<EngineManager>();
            engineManager.SetMovementDataForEngineState(EngineState.SLOW, normalSpeedMovementData);
            engineManager.SetMovementDataForEngineState(EngineState.NORMAL, normalSpeedMovementData);
            engineManager.SetMovementDataForEngineState(EngineState.FAST, normalSpeedMovementData);
            engineManager.SetMovementDataForEngineState(EngineState.SPECIAL, normalSpeedMovementData);

            CyclopsStartupPowerDownSequence cyclopsStartupPowerDownSequence = submarine.GetOrAddComponent<CyclopsStartupPowerDownSequence>();
            CyclopsEngineStateChangedCallouts cyclopsEngineStateChangedCallouts = submarine.GetOrAddComponent<CyclopsEngineStateChangedCallouts>();
            movementController.EngineManager = engineManager;
            CyclopsWelcomeCallout cyclopsWelcomeCallout = submarine.GetOrAddComponent<CyclopsWelcomeCallout>();
            CyclopsEngineSound cyclopsEngineSound = submarine.GetOrAddComponent<CyclopsEngineSound>();
            cyclopsEngineSound.RampUpSpeed = 0.2f;
            cyclopsEngineSound.RampDownSpeed = 0.2f;
            movementController.EngineSound = cyclopsEngineSound;

            OxygenReplenishment oxygenReplenishment = submarine.GetOrAddComponent<OxygenReplenishment>();
            oxygenReplenishment.OxygenPerSecond = 15f;
            oxygenReplenishment.OxygenEnergyCost = 0.1f;
        }

        private static void ApplyMaterials(GameObject submarine)
        {
            Shader shader = Shader.Find("MarmosetUBER");
            GameObject model = submarine.FindChild("Model");
            GameObject pointsOfInterest = submarine.FindChild("PointsOfInterest");
            Material body = model.FindChild("Body_LP").GetComponent<MeshRenderer>().material;
            Material bodyExtraOne = model.FindChild("Fin1_LP").GetComponent<MeshRenderer>().material;
            Material bodyExtraTwo = model.FindChild("ConningTower1_LP").GetComponent<MeshRenderer>().material;
            Material camera = model.FindChild("Camera_LP").GetComponent<MeshRenderer>().material;
            Material decals = model.FindChild("Decals_LP").GetComponent<MeshRenderer>().material;
            Material hatch = pointsOfInterest.FindChild("LeftDoorFlap").FindChild("LeftDoorFlap").GetComponent<MeshRenderer>().material;
            Material sensors = model.FindChild("Sensors").FindChild("RadarDish_LP").GetComponent<MeshRenderer>().material;

            body.shader = shader;
            body.EnableKeyword("MARMO_SPECMAP");
            body.EnableKeyword("_ZWRITE_ON");
            body.SetColor("_Color", Color.white);
            body.SetColor("_SpecColor", Color.white);
            body.SetFloat("_SpecInt", 1f);
            body.SetFloat("_Shininess", 6.5f);
            body.SetTexture("_SpecTex", OdysseyAssetLoader.BODY_SPEC);
            body.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            body.SetTexture("_BumpMap", OdysseyAssetLoader.BODY_NORMAL);

            bodyExtraOne.shader = shader;
            bodyExtraOne.EnableKeyword("MARMO_SPECMAP");
            bodyExtraOne.EnableKeyword("_ZWRITE_ON");
            bodyExtraOne.SetColor("_Color", Color.white);
            bodyExtraOne.SetColor("_SpecColor", Color.white);
            bodyExtraOne.SetFloat("_SpecInt", 1f);
            bodyExtraOne.SetFloat("_Shininess", 6.5f);
            bodyExtraOne.SetTexture("_SpecTex", OdysseyAssetLoader.BODY_EXTRA_ONE_SPEC);
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
            hatch.SetTexture("_SpecTex", OdysseyAssetLoader.HATCH_SPEC);
            hatch.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            hatch.SetTexture("_BumpMap", OdysseyAssetLoader.HATCH_NORMAL);

            sensors.shader = shader;
            sensors.EnableKeyword("MARMO_SPECMAP");
            sensors.EnableKeyword("_ZWRITE_ON");
            sensors.SetColor("_Color", Color.white);
            sensors.SetColor("_SpecColor", Color.white);
            sensors.SetFloat("_SpecInt", 1f);
            sensors.SetFloat("_Shininess", 6.5f);
            sensors.SetTexture("_SpecTex", OdysseyAssetLoader.SENSORS_SPEC);
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
            pointsOfInterest.FindChild("LeftDoorFlap").FindChild("LeftDoorFlap").GetComponent<MeshRenderer>().material = hatch;
            pointsOfInterest.FindChild("RightDoorFlap").FindChild("RightDoorFlap").GetComponent<MeshRenderer>().material = hatch;
            model.FindChild("DoorPlate_LP").GetComponent<MeshRenderer>().material = hatch;
            model.FindChild("DoorSeal_LP").GetComponent<MeshRenderer>().material = hatch;
            model.FindChild("Fin1_LP").GetComponent<MeshRenderer>().material = bodyExtraOne;
            model.FindChild("Fin2_LP").GetComponent<MeshRenderer>().material = bodyExtraOne;
            model.FindChild("HeadLight_LP").GetComponent<MeshRenderer>().material = bodyExtraTwo;
            model.FindChild("HeadLight_LP001").GetComponent<MeshRenderer>().material = bodyExtraTwo;
            model.FindChild("Motor_LP").GetComponent<MeshRenderer>().material = bodyExtraOne;
            model.FindChild("PropShaft_LP").GetComponent<MeshRenderer>().material = bodyExtraOne;
            model.FindChild("SensorAntenna3_LP").GetComponent<MeshRenderer>().material = sensors;
            model.FindChild("Shroud_LP").GetComponent<MeshRenderer>().material = bodyExtraTwo;
            model.FindChild("ShroudSupport_LP").GetComponent<MeshRenderer>().material = bodyExtraOne;
            model.FindChild("SideFins_LP").GetComponent<MeshRenderer>().material = bodyExtraOne;
            model.FindChild("Thruster_LP").GetComponent<MeshRenderer>().material = bodyExtraTwo;
            model.FindChild("ThrusterLeg_LP").GetComponent<MeshRenderer>().material = bodyExtraTwo;
            model.FindChild("Sensors").FindChild("RadarDish_LP").GetComponent<MeshRenderer>().material = sensors;
            model.FindChild("Sensors").FindChild("RadarDish_T_LP").GetComponent<MeshRenderer>().material = sensors;
            model.FindChild("Sensors").FindChild("SensorAntenna1_LP").GetComponent<MeshRenderer>().material = sensors;
            model.FindChild("Sensors").FindChild("SensorAntenna1_T_LP").GetComponent<MeshRenderer>().material = sensors;
            model.FindChild("Sensors").FindChild("SensorAntenna2_LP").GetComponent<MeshRenderer>().material = sensors;
            model.FindChild("Sensors").FindChild("SensorAntenna2_T1_LP").GetComponent<MeshRenderer>().material = sensors;
            model.FindChild("Sensors").FindChild("SensorAntenna2_T2_LP").GetComponent<MeshRenderer>().material = sensors;
            model.FindChild("Sensors").FindChild("SensorAntenna2_T3_LP").GetComponent<MeshRenderer>().material = sensors;
            model.FindChild("Sensors").FindChild("SensorAntenna3_LP").GetComponent<MeshRenderer>().material = sensors;
            model.FindChild("Sensors").FindChild("SensorBase_LP").GetComponent<MeshRenderer>().material = sensors;
        }
    }
}
