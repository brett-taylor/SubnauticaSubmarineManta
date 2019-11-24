using Manta.Utilities;
using SMLHelper.V2.Assets;
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
using Submarines.Content.Death;
using System.Collections;
using Submarines.Content.Beacon;
using Submarines.Utilities.Helpers;

namespace Manta.Core
{
    /**
     * The Mantaaaaaaaaa
     */
    public class MantaMod : Spawnable
    {
        public static readonly string UNIQUE_ID = "SubmarineManta";
        public static readonly string NAME = "Manta";
        public static readonly string DESCRIPTION = "A high-speed, average-armour industrial resource collecting submarine.";

        public static TechType MANTA_TECH_TYPE = new MantaMod().TechType;
        public override string AssetsFolder => EntryPoint.MOD_FOLDER_NAME + EntryPoint.ASSET_FOLDER_NAME;
        public override string IconFileName => "MantaIcon.png";

        public MantaMod() : base(UNIQUE_ID, NAME, DESCRIPTION)
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

            GameObject submarine = Object.Instantiate(Assets.MANTA_EXTERIOR);
            Renderer[] renderers = submarine.GetComponentsInChildren<Renderer>();
            ApplyMaterials(submarine);

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
            submarine.GetOrAddComponent<PrefabIdentifier>().ClassId = UNIQUE_ID;
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
            liveMixin.data.maxHealth = 500;

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

            InternalLeakManager internalLeakManage = submarine.GetOrAddComponent<InternalLeakManager>();
            internalLeakManage.LeakPrefabs = new List<GameObject>()
            {
                CyclopsDefaultAssets.WATER_LEAK
            };

            DeathManager deathManager = submarine.GetOrAddComponent<DeathManager>();
            deathManager.DeathPreparationTime = 22f;

            BasicDeath basicDeath = submarine.GetOrAddComponent<BasicDeath>();
            basicDeath.TimeTillDeletionOfSub = 60f;
            basicDeath.FallSpeed = 2f;

            CyclopsDeathExplosion cyclopsDeathExplosion = submarine.GetOrAddComponent<CyclopsDeathExplosion>();
            cyclopsDeathExplosion.TimeToExplosionAfterDeath = 18f;
            cyclopsDeathExplosion.FMODAsset = CyclopsDefaultAssets.CYCLOPS_EXPLOSION_FMOD;

            CyclopsAbandonShip cyclopsAbandonShip = submarine.GetOrAddComponent<CyclopsAbandonShip>();
            cyclopsAbandonShip.TimeToCalloutAfterDeath = 0f;
            cyclopsAbandonShip.FMODAsset = CyclopsDefaultAssets.AI_ABANDON;

            DestabiliseOnSubDeath destabiliseOnSubDeath = submarine.GetOrAddComponent<DestabiliseOnSubDeath>();
            KillPlayerInsideOnSubDeath killPlayerInsideOnSubDeath = submarine.GetOrAddComponent<KillPlayerInsideOnSubDeath>();

            SMLHelper.V2.Handlers.TechTypeHandler.TryGetModdedTechType(UNIQUE_ID, out TechType mantaTechType);
            PingType mantaPingType = CustomBeaconManager.RegisterNewPingType(mantaTechType, NAME, Assets.PING_ICON);
            PingInstance pingInstance = CustomBeaconManager.AddNewBeacon(submarine, mantaPingType, "HMS Unknown Manta");
        }

        private static void ApplyMaterials(GameObject manta)
        {
            GameObject model = manta.FindChild("Model");
            GameObject exterior = model.FindChild("Exterior");
            GameObject exteriorModel = exterior.FindChild("ExteriorModel");
            GameObject interior = model.FindChild("Interior");
            GameObject poi = manta.FindChild("PointsOfInterest");
            GameObject moonpool = interior.FindChild("MoonPool");

            // Tail
            MaterialHelper.ApplyNormalShader(exteriorModel, "Exterior-tail", Assets.HULL_ONE_NORMAL_MAP);
            MaterialHelper.ApplySpecShader(exteriorModel, "Exterior-tail", Assets.HULL_ONE_SPEC_MAP, 1f, 6.5f);

            // Wings
            MaterialHelper.ApplyNormalShader(exteriorModel, "Exterior-wings", Assets.HULL_TWO_NORMAL_MAP);
            MaterialHelper.ApplySpecShader(exteriorModel, "Exterior-wings", Assets.HULL_TWO_SPEC_MAP, 1f, 6.5f);
            MaterialHelper.ApplyEmissionShader(exteriorModel, "Exterior-wings", Assets.HULL_TWO_EMISSIVE_MAP, Color.white);

            // Middle Body
            MaterialHelper.ApplyNormalShader(exteriorModel, "Exterior-middleBody", Assets.HULL_THREE_NORMAL_MAP);
            MaterialHelper.ApplySpecShader(exteriorModel, "Exterior-middleBody", Assets.HULL_THREE_SPEC_MAP, 1f, 6.5f);

            // Windshield
            MaterialHelper.ApplyNormalShader(exteriorModel, "Exterior-windshield", Assets.HULL_FOUR_NORMAL_MAP);
            MaterialHelper.ApplySpecShader(exteriorModel, "Exterior-windshield", Assets.HULL_FOUR_SPEC_MAP, 1f, 6.5f);
            MaterialHelper.ApplyEmissionShader(exteriorModel, "Exterior-windshield", Assets.HULL_FOUR_EMISSIVE_MAP, Color.white);

            // Exterior Decals
            GameObject exteriorDecals = exterior.FindChild("ExteriorDecals");
            MaterialHelper.ApplyNormalShader(exteriorDecals, "ExteriorDecalsOne", Assets.EXTERIOR_DECALS_NORMAL_MAP);
            MaterialHelper.ApplyAlphaShader(exteriorDecals, "ExteriorDecalsOne");
            MaterialHelper.ApplyAlphaShader(exteriorDecals, "ExteriorDecalsTwo");
            MaterialHelper.ApplyAlphaShader(exteriorDecals, "ExteriorDecalsThree");

            // Propeller
            MaterialHelper.ApplySpecShader(exterior.FindChild("Propeller"), "Propeller", null, 3f, 20f);

            // Windshield
            GameObject windshieldGlass = exterior.FindChild("WindshieldGlass");
            MaterialHelper.ChangeMaterialColor(windshieldGlass, "Glass", new Color(0f, 0.2f, 0.4f, 0.4f));
            MaterialHelper.ApplyGlassShader(windshieldGlass, "Glass");

            // Exterior Doors
            MaterialHelper.ApplySpecShader(poi.FindChild("PlayerEntranceLeftFlap"), "Exterior-doors", null, 1f, 6.5f);
            MaterialHelper.ApplySpecShader(poi.FindChild("PlayerEntranceRightFlap"), "Exterior-doors", null, 1f, 6.5f);
            MaterialHelper.ApplySpecShader(poi.FindChild("VehicleEntranceLeftFlap"), "Exterior-doors", null, 1f, 6.5f);
            MaterialHelper.ApplySpecShader(poi.FindChild("VehicleEntranceRightFlap"), "Exterior-doors", null, 1f, 6.5f);

            // Internal Walls
            GameObject walls = interior.FindChild("InternalWalls");
            MaterialHelper.ApplyNormalShader(walls, "Interior-Walls", Assets.WALLS_NORMAL_MAP);
            MaterialHelper.ApplySpecShader(walls, "Interior-Walls", Assets.WALLS_SPEC_MAP, 1f, 6.5f);

            // Player Hatch
            GameObject playerHatch = interior.FindChild("PlayerHatch");
            MaterialHelper.ApplySpecShader(playerHatch, "Hatch", null, 0.6f, 20f);
            MaterialHelper.ApplySpecShader(playerHatch, "Hatch Base", null, 0.6f, 20f);

            // Floors
            MaterialHelper.ApplySpecShader(interior.FindChild("BaseFloor"), "Floor", null, 1f, 6.5f);
            MaterialHelper.ApplySpecShader(interior.FindChild("FloorOverlay"), "Floor-Overlay", null, 1f, 6.5f);

            // Lockers
            MaterialHelper.ApplySpecShader(interior.FindChild("Lockers"), "Lockers", null, 1f, 6.5f);

            // Doorways
            MaterialHelper.ApplySpecShader(interior.FindChild("Doorways"), "Dividers", null, 1f, 6.5f);
            MaterialHelper.SetMarmosetShader(interior.FindChild("WallDoorDecals"), "WallDecals");

            // Back door
            MaterialHelper.ApplySpecShader(interior.FindChild("BackDoor"), "Door", null, 1f, 6.5f);

            // Pillars
            MaterialHelper.ApplySpecShader(interior.FindChild("Pillars"), "Pillars", null, 1f, 6.5f);

            // Light Decals
            GameObject lights = interior.FindChild("LightDecals");
            MaterialHelper.ApplyAlphaShader(lights, "Light");
            MaterialHelper.ApplyEmissionShader(lights, "Light", Assets.LIGHT_EMISSIVE_MAP, Color.white);

            // Interior Decals
            GameObject interiorDecals = interior.FindChild("InteriorDecals");
            MaterialHelper.ApplyNormalShader(interior, "WallDecalsOne", Assets.WALL_DECALS_ONE_NORMAL_MAP);
            MaterialHelper.ApplyEmissionShader(interior, "WallDecalsOne", Assets.WALL_DECALS_ONE_EMISSIVE_MAP, Color.white);
            MaterialHelper.ApplyAlphaShader(interior, "WallDecalsOne");
            MaterialHelper.ApplyNormalShader(interior, "WallDecalsTwo", Assets.WALL_DECALS_TWO_NORMAL_MAP);
            MaterialHelper.ApplyEmissionShader(interior, "WallDecalsTwo", Assets.WALL_DECALS_TWO_EMISSIVE_MAP, Color.white);
            MaterialHelper.ApplyAlphaShader(interior, "WallDecalsTwo");
            MaterialHelper.ApplyNormalShader(interior, "WallDecalsThree", Assets.WALL_DECALS_THREE_NORMAL_MAP);
            MaterialHelper.ApplyAlphaShader(interior, "WallDecalsThree");

            // Moonpool Glass
            GameObject moonpoolGlass = moonpool.FindChild("MoonPoolGlass");
            MaterialHelper.ChangeMaterialColor(moonpoolGlass, "Moonpool-glass", new Color(0f, 0.2f, 0.4f, 0.4f));
            MaterialHelper.ApplyGlassShader(moonpoolGlass, "Moonpool-glass");

            // MoonPool Hatch 
            MaterialHelper.ApplySpecShader(moonpool.FindChild("MoonPoolHatch"), "Moonpool-hatch", null, 0.6f, 20f);

            // Moonpool hatch decal
            MaterialHelper.SetMarmosetShader(moonpool.FindChild("MoonPoolHatchDecal"), "Moonpool-hatch-decal");

            // Moonpool pipes
            MaterialHelper.ApplySpecShader(moonpool.FindChild("MoonPoolInsideFrontDecal"), "Moonpool-pipes", null, 0.6f, 20f);

            // Moonpool Arms
            MaterialHelper.ApplySpecShader(moonpool.FindChild("MoonPoolArms"), "Moonpool-arms", null, 0.6f, 20f);

            // Moonpool walls
            MaterialHelper.ApplySpecShader(moonpool.FindChild("MoonPoolWalls"), "Moonpool-walls", null, 0.6f, 20f);

            // Moonpool Walkway
            MaterialHelper.ApplySpecShader(moonpool.FindChild("MoonPoolWalkWay"), "Floor", null, 1f, 6.5f);
            MaterialHelper.ApplySpecShader(moonpool.FindChild("MoonPoolGlassProtection"), "Floor", null, 1f, 6.5f);

            // Console
            GameObject steeringConsole = poi.FindChild("SteeringConsole");
            MaterialHelper.ApplySpecShader(steeringConsole, "Console-body", null, 1f, 6.5f);
            MaterialHelper.SetMarmosetShader(steeringConsole, "Console-screen");
            MaterialHelper.SetMarmosetShader(steeringConsole, "Console-screen-bevel");
            MaterialHelper.ApplyEmissionShader(steeringConsole, "Console-emission", Assets.CONSOLE_SCREEN_EMISSION, Color.white);
        }
    }
}
