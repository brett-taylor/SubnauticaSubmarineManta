using Manta.Utilities;
using SMLHelper.V2.Assets;
using Submarines;
using Submarines.Miscellaneous;
using Submarines.Movement;
using Submarines.PointsOfInterest;
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

            Shader shader = Shader.Find("MarmosetUBER");
            Renderer[] renderers = submarine.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                foreach(Material mat in renderer.materials)
                {
                    if (renderer.gameObject.name.ToLower().Contains("glass") == false)
                    {
                        mat.shader = shader;
                    }
                }
            }

            // Restore stuff that is lost when we swap shader.
            MeshRenderer exteriorRenderer = submarine.FindChild("Model")?.FindChild("Exterior")?.GetComponent<MeshRenderer>();
            Material hullOne = exteriorRenderer.materials[1]; // "hull" -> tail component
            Material hullTwo = exteriorRenderer.materials[4]; // "hull2" -> Wings
            Material hullThree = exteriorRenderer.materials[0]; // "hull3" -> Midsection
            Material hullFour = exteriorRenderer.materials[2]; // "hull4" -> Windshield
            Material hullGlass = exteriorRenderer.materials[3]; // Glass

            hullOne.EnableKeyword("MARMO_SPECMAP");
            hullOne.EnableKeyword("_ZWRITE_ON");
            hullOne.SetColor("_Color", Color.white);
            hullOne.SetColor("_SpecColor", Color.white);
            hullOne.SetFloat("_SpecInt", 1f);
            hullOne.SetFloat("_Shininess", 6.5f);
            hullOne.SetFloat("_Fresnel", 0f);
            hullOne.SetTexture("_MainTex", MantaAssetLoader.HULL_ONE_MAIN_TEX);
            hullOne.SetTexture("_SpecTex", MantaAssetLoader.HULL_ONE_SPEC_MAP);
            hullOne.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            hullOne.SetTexture("_BumpMap", MantaAssetLoader.HULL_ONE_NORMAL_MAP);
            hullOne.SetColor("_GlowColor", Color.white);
            hullOne.SetFloat("_GlowStrength", 1f);
            hullOne.SetFloat("_EmissionLM", 0f);
            hullOne.SetVector("_EmissionColor", Vector4.zero);
            hullOne.SetVector("_Illum_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));

            hullTwo.DisableKeyword("_EMISSION");
            hullTwo.EnableKeyword("MARMO_SPECMAP");
            hullTwo.EnableKeyword("_ZWRITE_ON");
            hullTwo.EnableKeyword("MARMO_EMISSION");
            hullTwo.SetColor("_Color", Color.white);
            hullTwo.SetColor("_SpecColor", Color.white);
            hullTwo.SetFloat("_SpecInt", 1f);
            hullTwo.SetFloat("_Shininess", 6.5f);
            hullTwo.SetFloat("_Fresnel", 0f);
            hullTwo.SetTexture("_MainTex", MantaAssetLoader.HULL_TWO_MAIN_TEX);
            hullTwo.SetTexture("_SpecTex", MantaAssetLoader.HULL_TWO_SPEC_MAP);
            hullTwo.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            hullTwo.SetTexture("_BumpMap", MantaAssetLoader.HULL_TWO_NORMAL_MAP);
            hullTwo.SetColor("_GlowColor", Color.white);
            hullTwo.SetFloat("_GlowStrength", 1f);
            hullTwo.SetFloat("_EmissionLM", 0f);
            hullTwo.SetVector("_EmissionColor", Vector4.zero);
            hullTwo.SetTexture("_Illum", MantaAssetLoader.HULL_TWO_EMISSIVE_MAP);
            hullTwo.SetVector("_Illum_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            hullTwo.SetFloat("_EnableGlow", 1.3f);

            hullThree.EnableKeyword("MARMO_SPECMAP");
            hullThree.EnableKeyword("_ZWRITE_ON");
            hullThree.SetColor("_Color", Color.white);
            hullThree.SetColor("_SpecColor", Color.white);
            hullThree.SetFloat("_SpecInt", 1f);
            hullThree.SetFloat("_Shininess", 6.5f);
            hullThree.SetFloat("_Fresnel", 0f);
            hullThree.SetTexture("_MainTex", MantaAssetLoader.HULL_THREE_MAIN_TEX);
            hullThree.SetTexture("_SpecTex", MantaAssetLoader.HULL_THREE_SPEC_MAP);
            hullThree.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            hullThree.SetTexture("_BumpMap", MantaAssetLoader.HULL_THREE_NORMAL_MAP);
            hullThree.SetColor("_GlowColor", Color.white);
            hullThree.SetFloat("_GlowStrength", 1f);
            hullThree.SetFloat("_EmissionLM", 0f);
            hullThree.SetVector("_EmissionColor", Vector4.zero);
            hullThree.SetVector("_Illum_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));

            hullFour.DisableKeyword("_EMISSION");
            hullFour.EnableKeyword("MARMO_SPECMAP");
            hullFour.EnableKeyword("_ZWRITE_ON");
            hullFour.EnableKeyword("MARMO_EMISSION");
            hullFour.SetColor("_Color", Color.white);
            hullFour.SetColor("_SpecColor", Color.white);
            hullFour.SetFloat("_SpecInt", 1f);
            hullFour.SetFloat("_Shininess", 6.5f);
            hullFour.SetFloat("_Fresnel", 0f);
            hullFour.SetTexture("_MainTex", MantaAssetLoader.HULL_FOUR_MAIN_TEX);
            hullFour.SetTexture("_SpecTex", MantaAssetLoader.HULL_FOUR_SPEC_MAP);
            hullFour.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            hullFour.SetTexture("_BumpMap", MantaAssetLoader.HULL_FOUR_EMISSIVE_MAP);
            hullFour.SetColor("_GlowColor", Color.white);
            hullFour.SetFloat("_GlowStrength", 1f);
            hullFour.SetFloat("_EmissionLM", 0f);
            hullFour.SetVector("_EmissionColor", Vector4.zero);
            hullFour.SetTexture("_Illum", MantaAssetLoader.HULL_FOUR_EMISSIVE_MAP);
            hullFour.SetVector("_Illum_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            hullFour.SetFloat("_EnableGlow", 1.3f);

            hullGlass.EnableKeyword("MARMO_SIMPLE_GLASS");
            hullGlass.SetColor("_Color", new Color(0.0f, 0.2f, 0.4f, 0.6f));
            hullGlass.SetFloat("_Mode", 3);
            hullGlass.SetFloat("_DstBlend", 1);
            hullGlass.SetFloat("_SrcBlend2", 0);
            hullGlass.SetFloat("_AddDstBlend", 1f);
            hullGlass.SetFloat("_ZWrite", 0);
            hullGlass.SetFloat("_Cutoff", 0);
            hullGlass.SetFloat("_IBLreductionAtNight", 0.92f);
            hullGlass.SetFloat("_EnableSimpleGlass", 1f);
            hullGlass.SetFloat("_MarmoSpecEnum ", 2f);
            hullGlass.SetColor("_SpecColor", new Color(1.000f, 1.000f, 1.000f, 1.000f));
            hullGlass.SetFloat("_Shininess", 6.2f);
            hullGlass.SetFloat("_Fresnel", 0.9f);

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
            Submarine submarineScript = submarine.GetOrAddComponent<MantaSubmarine>();

            submarine.GetOrAddComponent<MovementStabiliser>();
            submarine.GetOrAddComponent<WaterClipProxyModified>().Initialise();
            submarine.GetOrAddComponent<Components.MantaTemporarySteeringHUD>();

            submarine.GetOrAddComponent<Components.MantaSerializationFixer>();
            return submarine;
        }

        /**
         * If the component requires other custom components then do it here.
         * Read the comment on Components.MantaseializationFixer if you wish to understand why this horrible system exists.
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
                }
            }
        }
    }
}
