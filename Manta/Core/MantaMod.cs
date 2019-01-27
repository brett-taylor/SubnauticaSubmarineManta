using Manta.Utilities;
using SMLHelper.V2.Assets;
using Submarines;
using Submarines.Miscellaneous;
using Submarines.Movement;
using Submarines.Content;
using Submarines.DefaultCyclopsContent;
using Submarines.Utilities.Extension;
using Submarines.Water;
using UnityEngine;
using Submarines.Engine;

namespace Manta.Core
{
    /**
     * The Mantaaaaaaaaa
     */
    public class MantaMod : Spawnable
    {
        public static TechType MANTA_TECH_TYPE = new MantaMod().TechType;
        public override string AssetsFolder => EntryPoint.ASSET_FOLDER_LOCATION;
        public override string IconFileName => "MantaIcon.png";

        public MantaMod() : base("SubmarineManta", "Manta", "A high-speed submarine")
        {
        }

        public override GameObject GetGameObject()
        {
            return CreateManta();
        }

        /*
         * The first part of setting up the manta. If the components are self contained and do not rely on other components or require sounds
         * adding it here works fine.
         */
        public static GameObject CreateManta()
        {
            GameObject submarine = Object.Instantiate(MantaAssetLoader.MANTA_EXTERIOR);
            Renderer[] renderers = submarine.GetComponentsInChildren<Renderer>();
            ApplyMaterials(submarine, renderers);

            SkyApplier skyApplier = submarine.GetOrAddComponent<SkyApplier>();
            skyApplier.renderers = renderers;
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

            submarine.GetOrAddComponent<TechTag>().type = MANTA_TECH_TYPE;
            submarine.GetOrAddComponent<PrefabIdentifier>().ClassId = "SubmarineManta";
            submarine.GetOrAddComponent<SubmarineDuplicateFixer>();
            submarine.GetOrAddComponent<MantaSubmarine>();
            submarine.GetOrAddComponent<MovementStabiliser>();
            submarine.GetOrAddComponent<WaterClipProxyModified>().Initialise();
            submarine.GetOrAddComponent<Components.MantaTemporarySteeringHUD>();

            submarine.GetOrAddComponent<Components.MantaSerializationFixer>();
            return submarine;
        }

        /**
         * If the component requires other custom components then do it here.
         * Read the comment on Components.MantaSeializationFixer if you wish to understand why this horrible system exists.
         */
        public static void SetUpManta(GameObject submarine)
        {
            MantaSubmarine mantaSubmarine = submarine.GetComponent<MantaSubmarine>();
            Transform applyForceLocation = submarine.FindChild("PointsOfInterest").FindChild("ForceAppliedLocation").transform;
            MovementController movementController = submarine.GetOrAddComponent<MovementController>();
            movementController.ApplyForceLocation = applyForceLocation;

            GameObject doorLeft = submarine.FindChild("PointsOfInterest").FindChild("PlayerEntranceLeftFlap");
            doorLeft.GetComponentInChildren<HingeJoint>().connectedBody = submarine.GetComponent<Rigidbody>();
            HingeJointDoor hingeDoorLeft = doorLeft.GetOrAddComponent<HingeJointDoor>();
            hingeDoorLeft.OverwriteTargetVelocity = true;
            hingeDoorLeft.TargetVelocity = 150f;
            hingeDoorLeft.TriggerToEverything = false;
            hingeDoorLeft.TriggerToPlayer = true;
            hingeDoorLeft.TriggerToVehicles = false;

            GameObject doorRight = submarine.FindChild("PointsOfInterest").FindChild("PlayerEntranceRightFlap");
            doorRight.GetComponentInChildren<HingeJoint>().connectedBody = submarine.GetComponent<Rigidbody>();
            HingeJointDoor hingeDoorRight = doorRight.GetOrAddComponent<HingeJointDoor>();
            hingeDoorRight.OverwriteTargetVelocity = true;
            hingeDoorRight.TargetVelocity = 150f;
            hingeDoorRight.TriggerToEverything = false;
            hingeDoorRight.TriggerToPlayer = true;
            hingeDoorRight.TriggerToVehicles = false;

            GameObject vehicleDoorLeft = submarine.FindChild("PointsOfInterest").FindChild("VehicleEntranceLeftFlap");
            vehicleDoorLeft.GetComponentInChildren<HingeJoint>().connectedBody = submarine.GetComponent<Rigidbody>();
            HingeJointDoor hingeVehicleDoorLeft = vehicleDoorLeft.GetOrAddComponent<HingeJointDoor>();
            hingeVehicleDoorLeft.OverwriteTargetVelocity = true;
            hingeVehicleDoorLeft.TargetVelocity = 150f;
            hingeVehicleDoorLeft.TriggerToEverything = false;
            hingeVehicleDoorLeft.TriggerToPlayer = false;
            hingeVehicleDoorLeft.TriggerToVehicles = true;

            GameObject vehicleDoorRight = submarine.FindChild("PointsOfInterest").FindChild("VehicleEntranceRightFlap");
            vehicleDoorRight.GetComponentInChildren<HingeJoint>().connectedBody = submarine.GetComponent<Rigidbody>();
            HingeJointDoor hingeVehicleDoorRight = vehicleDoorRight.GetOrAddComponent<HingeJointDoor>();
            hingeVehicleDoorRight.OverwriteTargetVelocity = true;
            hingeVehicleDoorRight.TargetVelocity = 150f;
            hingeVehicleDoorRight.TriggerToEverything = false;
            hingeVehicleDoorRight.TriggerToPlayer = false;
            hingeVehicleDoorRight.TriggerToVehicles = true;

            GameObject entrancePosition = submarine.FindChild("PointsOfInterest").FindChild("EntranceTeleportSpot");
            GameObject entranceHatch = submarine.FindChild("PointsOfInterest").FindChild("PlayerEntrance").FindChild("Base");
            EntranceHatch entranceHatchTeleport = entranceHatch.GetOrAddComponent<EntranceHatch>();
            entranceHatchTeleport.HoverText = "Board Manta";
            entranceHatchTeleport.HoverHandReticle = HandReticle.IconType.Hand;
            entranceHatchTeleport.TeleportTarget = entrancePosition;
            entranceHatchTeleport.Submarine = mantaSubmarine;
            entranceHatchTeleport.EnteringSubmarine = true;

            GameObject leavePosition = submarine.FindChild("PointsOfInterest").FindChild("LeaveTeleportSpot");
            GameObject leaveHatch = submarine.FindChild("PointsOfInterest").FindChild("PlayerEntrance").FindChild("Top");
            EntranceHatch leaveHatchTeleport = leaveHatch.GetOrAddComponent<EntranceHatch>();
            leaveHatchTeleport.HoverText = "Disembark Manta";
            leaveHatchTeleport.HoverHandReticle = HandReticle.IconType.Hand;
            leaveHatchTeleport.TeleportTarget = leavePosition;
            leaveHatchTeleport.Submarine = mantaSubmarine;
            leaveHatchTeleport.EnteringSubmarine = false;

            GameObject steeringConsolePOI = submarine.FindChild("PointsOfInterest").FindChild("SteeringConsole");
            GameObject steeringConsoleLeftHandTarget = submarine.FindChild("PointsOfInterest").FindChild("SteeringConsole").FindChild("LeftIKTarget");
            GameObject steeringConsoleRightHandTarget = submarine.FindChild("PointsOfInterest").FindChild("SteeringConsole").FindChild("RightIKTarget");
            GameObject playerParentWhilePiloting = submarine.FindChild("PointsOfInterest").FindChild("SteeringConsole").FindChild("PlayerLockedWhileSteeringPosition");
            SteeringConsole steeringConsole = steeringConsolePOI.GetOrAddComponent<SteeringConsole>();
            steeringConsole.MovementController = submarine.GetComponent<MovementController>();
            steeringConsole.ParentWhilePilotingGO = playerParentWhilePiloting;
            steeringConsole.LeftHandIKTarget = steeringConsoleLeftHandTarget;
            steeringConsole.RightHandIKTarget = steeringConsoleRightHandTarget;
            steeringConsole.Submarine = mantaSubmarine;

            CyclopsCollisionSounds collisionSounds = submarine.GetOrAddComponent<CyclopsCollisionSounds>();

            MovementData normalSpeedMovementData = new MovementData
            {
                ForwardAccelerationSpeed = 5f,
                BackwardsAccelerationSpeed = 3f,
                AscendDescendSpeed = 3f,
                RotationSpeed = 0.3f
            };

            EngineManager engineManager = submarine.GetOrAddComponent<EngineManager>();
            engineManager.SetMovementDataForEngineState(EngineState.SLOW, normalSpeedMovementData);
            engineManager.SetMovementDataForEngineState(EngineState.NORMAL, normalSpeedMovementData);
            engineManager.SetMovementDataForEngineState(EngineState.FAST, normalSpeedMovementData);
            engineManager.SetMovementDataForEngineState(EngineState.SILENTRUNNING, normalSpeedMovementData);

            CyclopsStartupPowerDownSequence cyclopsStartupPowerDownSequence = submarine.GetOrAddComponent<CyclopsStartupPowerDownSequence>();
            CyclopsEngineStateChangedCallouts cyclopsEngineStateChangedCallouts = submarine.GetOrAddComponent<CyclopsEngineStateChangedCallouts>();
            movementController.EngineManager = engineManager;
            CyclopsWelcomeCallout cyclopsWelcomeCallout = submarine.GetOrAddComponent<CyclopsWelcomeCallout>();
            CyclopsEngineSound cyclopsEngineSound = submarine.GetOrAddComponent<CyclopsEngineSound>();
            cyclopsEngineSound.RampUpSpeed = 0.2f;
            cyclopsEngineSound.RampDownSpeed = 0.2f;
            movementController.EngineSound = cyclopsEngineSound;

            OxygenReplenishment oxygenReplenishment = submarine.GetOrAddComponent<OxygenReplenishment>();
            oxygenReplenishment.OxygenPerSecond = 4f;
            oxygenReplenishment.OxygenEnergyCost = 0.1f;

            FMODAsset[] fmods = Resources.FindObjectsOfTypeAll<FMODAsset>();
            foreach (FMODAsset fmod in fmods)
            {
                switch (fmod.name.ToLower())
                {
                    case "docking_doors_close":
                        //hingeVehicleDoorLeft.CloseSound = fmod;
                        //hingeVehicleDoorRight.CloseSound = fmod;
                        break;
                    case "docking_doors_open":
                        //hingeVehicleDoorLeft.OpenSound = fmod;
                        //hingeVehicleDoorRight.OpenSound = fmod;
                        break;
                    case "outer_hatch_close":
                        hingeDoorLeft.CloseSound = fmod;
                        hingeDoorRight.CloseSound = fmod;
                        break;
                    case "outer_hatch_open":
                        hingeDoorLeft.OpenSound = fmod;
                        hingeDoorRight.OpenSound = fmod;
                        break;
                    case "impact_solid_medium":
                        collisionSounds.ImpactHitMedium = fmod;
                        break;
                    case "impact_solid_soft":
                        collisionSounds.ImpactHitSoft = fmod;
                        break;
                    case "impact_solid_hard":
                        collisionSounds.ImpactHitHard = fmod;
                        break;
                    case "ai_silent_running":
                        cyclopsEngineStateChangedCallouts.SilentRunningCallout = fmod;
                        break;
                    case "ai_ahead_standard":
                        cyclopsEngineStateChangedCallouts.NormalCallout = fmod;
                        break;
                    case "ai_ahead_slow":
                        cyclopsEngineStateChangedCallouts.SlowCallout = fmod;
                        break;
                    case "ai_ahead_flank":
                        cyclopsEngineStateChangedCallouts.FastCallout = fmod;
                        break;
                    case "ai_engine_up":
                        cyclopsStartupPowerDownSequence.StartupSound = fmod;
                        break;
                    case "ai_engine_down":
                        cyclopsStartupPowerDownSequence.PowerDownCallout = fmod;
                        break;
                    case "ai_welcome":
                        cyclopsWelcomeCallout.WelcomeAboardCallout = fmod;
                        break;
                    case "cyclops_loop_rpm":
                        cyclopsEngineSound.FMODAsset = fmod;
                        break;
                }
            }
        }

        private static void ApplyMaterials(GameObject manta, Renderer[] renderers)
        {
            Shader shader = Shader.Find("MarmosetUBER");
            foreach (Renderer renderer in renderers)
            {
                foreach (Material mat in renderer.materials)
                {
                    if (mat.name.ToLower().Contains("glass") == false)
                    {
                        mat.shader = shader;
                    }
                }
            }

            MeshRenderer exteriorRenderer = manta.FindChild("Model")?.FindChild("Exterior")?.GetComponent<MeshRenderer>();
            Material middleBody = exteriorRenderer.materials[0];
            Material tail = exteriorRenderer.materials[1];
            Material windshield = exteriorRenderer.materials[2];
            Material glass = exteriorRenderer.materials[3];
            Material wings = exteriorRenderer.materials[4];

            tail.shader = shader;
            tail.EnableKeyword("MARMO_SPECMAP");
            tail.EnableKeyword("_ZWRITE_ON");
            tail.SetColor("_Color", Color.white);
            tail.SetColor("_SpecColor", Color.white);
            tail.SetFloat("_SpecInt", 1f);
            tail.SetFloat("_Shininess", 6.5f);
            tail.SetFloat("_Fresnel", 0f);
            tail.SetTexture("_MainTex", MantaAssetLoader.HULL_ONE_MAIN_TEX);
            tail.SetTexture("_SpecTex", MantaAssetLoader.HULL_ONE_SPEC_MAP);
            tail.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            tail.SetTexture("_BumpMap", MantaAssetLoader.HULL_ONE_NORMAL_MAP);
            tail.SetColor("_GlowColor", Color.white);
            tail.SetFloat("_GlowStrength", 1f);
            tail.SetFloat("_EmissionLM", 0f);
            tail.SetVector("_EmissionColor", Vector4.zero);
            tail.SetVector("_Illum_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));

            wings.shader = shader;
            wings.DisableKeyword("_EMISSION");
            wings.EnableKeyword("MARMO_SPECMAP");
            wings.EnableKeyword("_ZWRITE_ON");
            wings.EnableKeyword("MARMO_EMISSION");
            wings.SetColor("_Color", Color.white);
            wings.SetColor("_SpecColor", Color.white);
            wings.SetFloat("_SpecInt", 1f);
            wings.SetFloat("_Shininess", 6.5f);
            wings.SetFloat("_Fresnel", 0f);
            wings.SetTexture("_MainTex", MantaAssetLoader.HULL_TWO_MAIN_TEX);
            wings.SetTexture("_SpecTex", MantaAssetLoader.HULL_TWO_SPEC_MAP);
            wings.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            wings.SetTexture("_BumpMap", MantaAssetLoader.HULL_TWO_NORMAL_MAP);
            wings.SetColor("_GlowColor", Color.white);
            wings.SetFloat("_GlowStrength", 1f);
            wings.SetFloat("_EmissionLM", 0f);
            wings.SetVector("_EmissionColor", Vector4.zero);
            wings.SetTexture("_Illum", MantaAssetLoader.HULL_TWO_EMISSIVE_MAP);
            wings.SetVector("_Illum_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            wings.SetFloat("_EnableGlow", 1.3f);

            middleBody.shader = shader;
            middleBody.EnableKeyword("MARMO_SPECMAP");
            middleBody.EnableKeyword("_ZWRITE_ON");
            middleBody.SetColor("_Color", Color.white);
            middleBody.SetColor("_SpecColor", Color.white);
            middleBody.SetFloat("_SpecInt", 1f);
            middleBody.SetFloat("_Shininess", 6.5f);
            middleBody.SetFloat("_Fresnel", 0f);
            middleBody.SetTexture("_MainTex", MantaAssetLoader.HULL_THREE_MAIN_TEX);
            middleBody.SetTexture("_SpecTex", MantaAssetLoader.HULL_THREE_SPEC_MAP);
            middleBody.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            middleBody.SetTexture("_BumpMap", MantaAssetLoader.HULL_THREE_NORMAL_MAP);
            middleBody.SetColor("_GlowColor", Color.white);
            middleBody.SetFloat("_GlowStrength", 1f);
            middleBody.SetFloat("_EmissionLM", 0f);
            middleBody.SetVector("_EmissionColor", Vector4.zero);
            middleBody.SetVector("_Illum_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));

            windshield.shader = shader;
            windshield.DisableKeyword("_EMISSION");
            windshield.EnableKeyword("MARMO_SPECMAP");
            windshield.EnableKeyword("_ZWRITE_ON");
            windshield.EnableKeyword("MARMO_EMISSION");
            windshield.SetColor("_Color", Color.white);
            windshield.SetColor("_SpecColor", Color.white);
            windshield.SetFloat("_SpecInt", 1f);
            windshield.SetFloat("_Shininess", 6.5f);
            windshield.SetFloat("_Fresnel", 0f);
            windshield.SetTexture("_MainTex", MantaAssetLoader.HULL_FOUR_MAIN_TEX);
            windshield.SetTexture("_SpecTex", MantaAssetLoader.HULL_FOUR_SPEC_MAP);
            windshield.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            windshield.SetTexture("_BumpMap", MantaAssetLoader.HULL_FOUR_NORMAL_MAP);
            windshield.SetColor("_GlowColor", Color.white);
            windshield.SetFloat("_GlowStrength", 1f);
            windshield.SetFloat("_EmissionLM", 0f);
            windshield.SetVector("_EmissionColor", Vector4.zero);
            windshield.SetTexture("_Illum", MantaAssetLoader.HULL_FOUR_EMISSIVE_MAP);
            windshield.SetVector("_Illum_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            windshield.SetFloat("_EnableGlow", 1.3f);

            /*glass.shader = shader;
            glass.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            glass.EnableKeyword("MARMO_SIMPLE_GLASS");
            glass.EnableKeyword("MARMO_SPECMAP");
            glass.EnableKeyword("WBOIT");
            glass.EnableKeyword("_ZWRITE_ON");
            glass.SetColor("_Color", new Color(0.0f, 0.2f, 0.4f, 0.6f));
            glass.SetFloat("_Mode", 3);
            glass.SetFloat("_DstBlend", 1);
            glass.SetFloat("_SrcBlend2", 0);
            glass.SetFloat("_AddDstBlend", 1f);
            glass.SetFloat("_ZWrite", 0);
            glass.SetFloat("_Cutoff", 0);
            glass.SetFloat("_IBLreductionAtNight", 0.92f);
            glass.SetFloat("_EnableSimpleGlass", 1f);
            glass.SetFloat("_MarmoSpecEnum", 2f);
            glass.SetColor("_SpecColor", new Color(1.000f, 1.000f, 1.000f, 1.000f));
            glass.SetFloat("_Shininess", 6.2f);
            glass.SetFloat("_Fresnel", 0.9f);
            glass.renderQueue = 3101;*/
        }
    }
}
