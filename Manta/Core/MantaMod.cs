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
using Submarines.Content.Damage;
using System.Collections.Generic;
using Submarines.Content.Lighting;

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
            CyclopsDefaultAssets.LoadDefaultCyclopsContent();

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
            hingeDoorLeft.OpenSound = CyclopsDefaultAssets.PLAYER_HATCH_OPEN;
            hingeDoorLeft.CloseSound = CyclopsDefaultAssets.PLAYER_HATCH_CLOSE;

            GameObject doorRight = submarine.FindChild("PointsOfInterest").FindChild("PlayerEntranceRightFlap");
            doorRight.GetComponentInChildren<HingeJoint>().connectedBody = submarine.GetComponent<Rigidbody>();
            HingeJointDoor hingeDoorRight = doorRight.GetOrAddComponent<HingeJointDoor>();
            hingeDoorRight.OverwriteTargetVelocity = true;
            hingeDoorRight.TargetVelocity = 150f;
            hingeDoorRight.TriggerToEverything = false;
            hingeDoorRight.TriggerToPlayer = true;
            hingeDoorRight.TriggerToVehicles = false;
            hingeDoorRight.OpenSound = CyclopsDefaultAssets.PLAYER_HATCH_OPEN;
            hingeDoorRight.CloseSound = CyclopsDefaultAssets.PLAYER_HATCH_CLOSE;

            GameObject vehicleDoorLeft = submarine.FindChild("PointsOfInterest").FindChild("VehicleEntranceLeftFlap");
            vehicleDoorLeft.GetComponentInChildren<HingeJoint>().connectedBody = submarine.GetComponent<Rigidbody>();
            HingeJointDoor hingeVehicleDoorLeft = vehicleDoorLeft.GetOrAddComponent<HingeJointDoor>();
            hingeVehicleDoorLeft.OverwriteTargetVelocity = true;
            hingeVehicleDoorLeft.TargetVelocity = 150f;
            hingeVehicleDoorLeft.TriggerToEverything = false;
            hingeVehicleDoorLeft.TriggerToPlayer = false;
            hingeVehicleDoorLeft.TriggerToVehicles = true;
            hingeVehicleDoorLeft.OpenSound = CyclopsDefaultAssets.DOCKING_DOORS_OPEN;
            hingeVehicleDoorLeft.CloseSound = CyclopsDefaultAssets.DOCKING_DOORS_CLOSE;

            GameObject vehicleDoorRight = submarine.FindChild("PointsOfInterest").FindChild("VehicleEntranceRightFlap");
            vehicleDoorRight.GetComponentInChildren<HingeJoint>().connectedBody = submarine.GetComponent<Rigidbody>();
            HingeJointDoor hingeVehicleDoorRight = vehicleDoorRight.GetOrAddComponent<HingeJointDoor>();
            hingeVehicleDoorRight.OverwriteTargetVelocity = true;
            hingeVehicleDoorRight.TargetVelocity = 150f;
            hingeVehicleDoorRight.TriggerToEverything = false;
            hingeVehicleDoorRight.TriggerToPlayer = false;
            hingeVehicleDoorRight.TriggerToVehicles = true;
            hingeVehicleDoorRight.OpenSound = CyclopsDefaultAssets.DOCKING_DOORS_OPEN;
            hingeVehicleDoorRight.CloseSound = CyclopsDefaultAssets.DOCKING_DOORS_CLOSE;

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
                RotationSpeed = 0.3f,
                StrafeSpeed = 2f
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

            LiveMixin liveMixin = submarine.GetOrAddComponent<LiveMixin>();
            liveMixin.data = CyclopsLiveMixinData.Get();// TO:DO Create a proper health system for the manta.
            liveMixin.data.knifeable = true; // TO:DO remove just here for testing purposes.
            liveMixin.data.maxHealth = 100;

            GameObject externalLights = submarine.FindChild("Lights").FindChild("Exterior");
            GameObject internalLights = submarine.FindChild("Lights").FindChild("Interior");

            LightsManager lightsManager = submarine.GetOrAddComponent<LightsManager>();
            List<Light> internalLightsList = new List<Light>();
            List<Light> externalLightsList = new List<Light>();
            foreach(Light light in externalLights.GetComponentsInChildren<Light>())
            {
                externalLightsList.Add(light);
            }
            foreach (Light light in internalLights.GetComponentsInChildren<Light>())
            {
                internalLightsList.Add(light);
            }
            lightsManager.ExternalLights = externalLightsList;
            lightsManager.InternalLights = internalLightsList;
            lightsManager.ExternalLightsOnIntensity = 1.5f;
            lightsManager.ExternalLightsOffIntensity = 0f;
            lightsManager.InternalLightsOnIntensity = 1.5f;
            lightsManager.InternalLightsOffIntensity = 0f;
            lightsManager.EnableInternalLightsOnStart = true;
            lightsManager.EnableExternalLightsOnStart = true;

            CyclopsUnderAttackEmergencyLighting emergencyLighting = submarine.GetOrAddComponent<CyclopsUnderAttackEmergencyLighting>();
            emergencyLighting.LightsAffected = internalLightsList;

            submarine.GetOrAddComponent<CyclopsUnderAttackCallout>();

            ExternalDamageManager externalDamageManager = submarine.GetOrAddComponent<ExternalDamageManager>();
            externalDamageManager.DamagePoints = submarine.FindChild("PointsOfInterest").FindChild("DamagePoints").transform;
            externalDamageManager.SubmarineLiveMixin = liveMixin;
            externalDamageManager.LiveMixinDataForExternalDamagePoints = CyclopsExternalDamagePointLiveMixinData.Get();
            externalDamageManager.DamagePointParticleEffects = new List<GameObject>()
            {
                CyclopsDefaultAssets.EXTERNAL_DAMAGE_POINT_PARTICLES,
            };
            externalDamageManager.DamagePointGameObjects = new List<GameObject>()
            {
                CyclopsDefaultAssets.EXTERNAL_DAMAGE_POINT,
            };

            InternalFireManager internalFireManager = submarine.GetOrAddComponent<InternalFireManager>();
            internalFireManager.SubmarineLiveMixin = liveMixin;
            internalFireManager.FirePoints = submarine.FindChild("PointsOfInterest").FindChild("FirePoints").transform;
            internalFireManager.FirePrefabs = new List<GameObject>()
            {
                CyclopsDefaultAssets.CYCLOPS_FIRE,
            };
            internalFireManager.DamageDonePerFirePerSecond = 5f;
            internalFireManager.Submarine = mantaSubmarine;
            internalFireManager.ChancePerDamageTakenToSpawnFire = 5;

            AutoRegenConditional autoRegen = submarine.GetOrAddComponent<AutoRegenConditional>();
            autoRegen.InternalFireManager = internalFireManager;
            autoRegen.ExternalDamageManager = externalDamageManager;
            autoRegen.LiveMixin = liveMixin;
            autoRegen.RegenPerSecond = 2f;

            InternalLeakManage internalLeakManage = submarine.GetOrAddComponent<InternalLeakManage>();
            internalLeakManage.LeakPrefabs = new List<GameObject>()
            {
                CyclopsDefaultAssets.WATER_LEAK
            };

            DeathManager deathManager = submarine.GetOrAddComponent<DeathManager>();
            deathManager.DeathPreparationTime = 10f;
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

            GameObject model = manta.FindChild("Model");
            MeshRenderer exteriorRenderer = model.FindChild("Exterior").GetComponent<MeshRenderer>();
            Material middleBody = exteriorRenderer.materials[0];
            Material tail = exteriorRenderer.materials[1];
            Material windshield = exteriorRenderer.materials[2];
            Material glass = exteriorRenderer.materials[3];
            Material wings = exteriorRenderer.materials[4];
            Material[] decals = manta.FindChild("Model").FindChild("ExteriorDecals").GetComponent<MeshRenderer>().materials;
            Material floor = model.FindChild("Floor").GetComponent<MeshRenderer>().material;
            Material floorDecals = model.FindChild("LightsAndFloorDecals").FindChild("FloorDecals").GetComponent<MeshRenderer>().material;
            Material lightDecals = model.FindChild("LightsAndFloorDecals").FindChild("LightDeclas").GetComponent<MeshRenderer>().material;
            Material wallDoorDecals = model.FindChild("WallDoorDecals").GetComponent<MeshRenderer>().material;
            Material walls = model.FindChild("Walls").GetComponent<MeshRenderer>().material;
            Material doorWays = model.FindChild("Doorways").GetComponent<MeshRenderer>().material;
            Material propeller = model.FindChild("Propeller").GetComponent<MeshRenderer>().material;
            Material[] moreInteriorDecals = model.FindChild("MoreInteriorDecals").GetComponent<MeshRenderer>().materials;
            Material backDoor = model.FindChild("BackDoor").GetComponent<MeshRenderer>().material;

            tail.shader = shader;
            tail.EnableKeyword("MARMO_SPECMAP");
            tail.EnableKeyword("_ZWRITE_ON");
            tail.SetColor("_Color", Color.white);
            tail.SetColor("_SpecColor", Color.white);
            tail.SetFloat("_SpecInt", 1f);
            tail.SetFloat("_Shininess", 6.5f);
            tail.SetFloat("_Fresnel", 0f);
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

            foreach (Material decalMaterial in decals)
            {
                decalMaterial.shader = shader;
                decalMaterial.EnableKeyword("_ZWRITE_ON");
                decalMaterial.EnableKeyword("MARMO_ALPHA");
                decalMaterial.EnableKeyword("MARMO_ALPHA_CLIP");
                decalMaterial.SetColor("_Color", Color.white);
            }
            decals[1].SetTexture("_BumpMap", MantaAssetLoader.EXTERIOR_DECALS_NORMAL_MAP);

            floor.shader = shader;
            floor.EnableKeyword("MARMO_SPECMAP");
            floor.EnableKeyword("_ZWRITE_ON");
            floor.SetColor("_Color", Color.white);
            floor.SetColor("_SpecColor", Color.white);
            floor.SetFloat("_SpecInt", 1f);
            floor.SetFloat("_Shininess", 6.5f);
            floor.SetFloat("_Fresnel", 0f);
            floor.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));

            wallDoorDecals.shader = shader;
            wallDoorDecals.EnableKeyword("MARMO_SPECMAP");
            wallDoorDecals.EnableKeyword("_ZWRITE_ON");
            wallDoorDecals.SetFloat("_SpecInt", 1f);
            wallDoorDecals.SetFloat("_Shininess", 6.5f);
            wallDoorDecals.SetFloat("_Fresnel", 0f);
            wallDoorDecals.SetVector("_SpecTex_ST", new Vector4(1f, 1.0f, 0.0f, 0.0f));

            backDoor.shader = shader;
            backDoor.EnableKeyword("MARMO_SPECMAP");
            backDoor.EnableKeyword("_ZWRITE_ON");
            backDoor.SetFloat("_SpecInt", 1f);
            backDoor.SetFloat("_Shininess", 6.5f);
            backDoor.SetFloat("_Fresnel", 0f);
            backDoor.SetVector("_SpecTex_ST", new Vector4(1f, 1.0f, 0.0f, 0.0f));

            walls.shader = shader;
            walls.EnableKeyword("MARMO_SPECMAP");
            walls.EnableKeyword("_ZWRITE_ON");
            walls.SetColor("_Color", Color.white);
            walls.SetColor("_SpecColor", Color.white);
            walls.SetFloat("_SpecInt", 1f);
            walls.SetFloat("_Shininess", 6.5f);

            doorWays.shader = shader;
            doorWays.EnableKeyword("MARMO_SPECMAP");
            doorWays.EnableKeyword("_ZWRITE_ON");
            doorWays.SetColor("_Color", Color.white);
            doorWays.SetColor("_SpecColor", Color.white);
            doorWays.SetFloat("_SpecInt", 1f);
            doorWays.SetFloat("_Shininess", 6.5f);

            propeller.shader = shader;
            propeller.EnableKeyword("MARMO_SPECMAP");
            propeller.EnableKeyword("_ZWRITE_ON");
            propeller.SetColor("_Color", Color.white);
            propeller.SetColor("_SpecColor", Color.white);
            propeller.SetFloat("_SpecInt", 5f);
            propeller.SetFloat("_Shininess", 10f);
            propeller.SetVector("_SpecTex_ST", new Vector4(1f, 1.0f, 0.0f, 0.0f));
            propeller.SetFloat("_Fresnel", 5f);

            floorDecals.shader = shader;
            floorDecals.EnableKeyword("MARMO_SPECMAP");
            floorDecals.EnableKeyword("_ZWRITE_ON");
            floorDecals.SetColor("_Color", Color.white);
            floorDecals.SetColor("_SpecColor", Color.white);
            floorDecals.SetFloat("_SpecInt", 1f);
            floorDecals.SetFloat("_Shininess", 6.5f);
            floorDecals.SetFloat("_Fresnel", 0f);
            floorDecals.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            floorDecals.SetTexture("_BumpMap", MantaAssetLoader.FLOOR_NORMAL_MAP);

            lightDecals.shader = shader;
            lightDecals.DisableKeyword("_EMISSION");
            lightDecals.EnableKeyword("MARMO_ALPHA");
            lightDecals.EnableKeyword("MARMO_ALPHA_CLIP");
            lightDecals.EnableKeyword("MARMO_SPECMAP");
            lightDecals.EnableKeyword("_ZWRITE_ON");
            lightDecals.EnableKeyword("MARMO_EMISSION");
            lightDecals.SetColor("_Color", Color.white);
            lightDecals.SetColor("_SpecColor", Color.white);
            lightDecals.SetFloat("_SpecInt", 1f);
            lightDecals.SetFloat("_Shininess", 6.5f);
            lightDecals.SetFloat("_Fresnel", 0f);
            lightDecals.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            lightDecals.SetColor("_GlowColor", Color.white);
            lightDecals.SetFloat("_GlowStrength", 1f);
            lightDecals.SetFloat("_EmissionLM", 0f);
            lightDecals.SetVector("_EmissionColor", Vector4.zero);
            lightDecals.SetTexture("_Illum", MantaAssetLoader.LIGHT_EMISSIVE_MAP);
            lightDecals.SetVector("_Illum_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            lightDecals.SetFloat("_EnableGlow", 1.3f);

            moreInteriorDecals[0].shader = shader;
            moreInteriorDecals[0].EnableKeyword("_ZWRITE_ON");
            moreInteriorDecals[0].EnableKeyword("MARMO_ALPHA");
            moreInteriorDecals[0].EnableKeyword("MARMO_ALPHA_CLIP");
            moreInteriorDecals[0].EnableKeyword("MARMO_EMISSION");
            moreInteriorDecals[0].SetTexture("_BumpMap", MantaAssetLoader.WALL_DECALS_ONE_NORMAL_MAP);
            moreInteriorDecals[0].SetColor("_GlowColor", Color.white);
            moreInteriorDecals[0].SetFloat("_GlowStrength", 1f);
            moreInteriorDecals[0].SetFloat("_EmissionLM", 0f);
            moreInteriorDecals[0].SetVector("_EmissionColor", Vector4.zero);
            moreInteriorDecals[0].SetTexture("_Illum", MantaAssetLoader.WALL_DECALS_ONE_EMISSIVE_MAP);
            moreInteriorDecals[0].SetVector("_Illum_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            moreInteriorDecals[0].SetFloat("_EnableGlow", 1.3f);

            moreInteriorDecals[1].shader = shader;
            moreInteriorDecals[1].EnableKeyword("_ZWRITE_ON");
            moreInteriorDecals[1].EnableKeyword("MARMO_ALPHA");
            moreInteriorDecals[1].EnableKeyword("MARMO_ALPHA_CLIP");
            moreInteriorDecals[1].EnableKeyword("MARMO_EMISSION");
            moreInteriorDecals[1].SetTexture("_BumpMap", MantaAssetLoader.WALL_DECALS_TWO_NORMAL_MAP);
            moreInteriorDecals[1].SetColor("_GlowColor", Color.white);
            moreInteriorDecals[1].SetFloat("_GlowStrength", 1f);
            moreInteriorDecals[1].SetFloat("_EmissionLM", 0f);
            moreInteriorDecals[1].SetVector("_EmissionColor", Vector4.zero);
            moreInteriorDecals[1].SetTexture("_Illum", MantaAssetLoader.WALL_DECALS_TWO_EMISSIVE_MAP);
            moreInteriorDecals[1].SetVector("_Illum_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            moreInteriorDecals[1].SetFloat("_EnableGlow", 1.3f);

            moreInteriorDecals[2].shader = shader;
            moreInteriorDecals[2].EnableKeyword("_ZWRITE_ON");
            moreInteriorDecals[2].EnableKeyword("MARMO_ALPHA");
            moreInteriorDecals[2].EnableKeyword("MARMO_ALPHA_CLIP");
            moreInteriorDecals[2].SetTexture("_BumpMap", MantaAssetLoader.WALL_DECALS_THREE_NORMAL_MAP);
        }
    }
}
