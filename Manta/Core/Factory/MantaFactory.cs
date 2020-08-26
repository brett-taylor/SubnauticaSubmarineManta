using System.Collections.Generic;
using System.Linq;
using Manta.Components;
using Manta.Core.Factory.Materials;
using Manta.Core.Factory.Materials.Impl;
using Manta.Utilities;
using Submarines.Content;
using Submarines.Content.Damage;
using Submarines.Content.Death;
using Submarines.Content.Lighting;
using Submarines.DefaultCyclopsContent;
using Submarines.Engine;
using Submarines.Miscellaneous;
using Submarines.Movement;
using Submarines.Water;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Manta.Core.Factory
{
    public static class MantaFactory
    {
        public static GameObject CreateManta()
        {
            CyclopsDefaultAssets.LoadDefaultCyclopsContent();
            
            var manta = Object.Instantiate(Assets.MANTA_EXTERIOR);
            var renderers = manta.GetComponentsInChildren<Renderer>();
            SetupMaterials(manta);
            
            var skyApplier = manta.EnsureComponent<SkyApplier>();
            skyApplier.renderers = renderers;
            skyApplier.anchorSky = Skies.Auto;

            manta.EnsureComponent<VFXSurface>();
            manta.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Global;

            var rb = manta.EnsureComponent<Rigidbody>();
            rb.angularDrag = 1f;
            rb.mass = 12000f;
            rb.useGravity = false;
            rb.centerOfMass = new Vector3(-0.1f, 0.8f, -1.7f);

            var forces = manta.EnsureComponent<WorldForces>();
            forces.aboveWaterDrag = 0f;
            forces.aboveWaterGravity = 9.81f;
            forces.handleDrag = true;
            forces.handleGravity = true;
            forces.underwaterDrag = 0.5f;
            forces.underwaterGravity = 0;

            manta.EnsureComponent<TechTag>().type = MantaMod.MANTA_TECH_TYPE;
            manta.EnsureComponent<PrefabIdentifier>().ClassId = MantaMod.UNIQUE_ID;
            manta.EnsureComponent<SubmarineDuplicateFixer>();
            manta.EnsureComponent<MantaSubmarine>();
            manta.EnsureComponent<MovementStabiliser>();
            manta.EnsureComponent<WaterClipProxyModified>().Initialise();
            manta.EnsureComponent<MantaTemporarySteeringHUD>();

            manta.EnsureComponent<MantaSerializationFixer>();
            return manta;
        }

        public static void Setup(GameObject manta)
        {
            var mantaSubmarine = manta.GetComponent<MantaSubmarine>();
            var applyForceLocation = manta.FindChild("PointsOfInterest").FindChild("ForceAppliedLocation").transform;
            var movementController = manta.EnsureComponent<MovementController>();
            movementController.ApplyForceLocation = applyForceLocation;

            var doorLeft = manta.FindChild("PointsOfInterest").FindChild("PlayerEntranceLeftFlap");
            doorLeft.GetComponentInChildren<HingeJoint>().connectedBody = manta.GetComponent<Rigidbody>();
            var hingeDoorLeft = doorLeft.EnsureComponent<HingeJointDoor>();
            hingeDoorLeft.OverwriteTargetVelocity = true;
            hingeDoorLeft.TargetVelocity = 150f;
            hingeDoorLeft.TriggerToEverything = false;
            hingeDoorLeft.TriggerToPlayer = true;
            hingeDoorLeft.TriggerToVehicles = false;
            hingeDoorLeft.OpenSound = CyclopsDefaultAssets.PLAYER_HATCH_OPEN;
            hingeDoorLeft.CloseSound = CyclopsDefaultAssets.PLAYER_HATCH_CLOSE;

            var doorRight = manta.FindChild("PointsOfInterest").FindChild("PlayerEntranceRightFlap");
            doorRight.GetComponentInChildren<HingeJoint>().connectedBody = manta.GetComponent<Rigidbody>();
            var hingeDoorRight = doorRight.EnsureComponent<HingeJointDoor>();
            hingeDoorRight.OverwriteTargetVelocity = true;
            hingeDoorRight.TargetVelocity = 150f;
            hingeDoorRight.TriggerToEverything = false;
            hingeDoorRight.TriggerToPlayer = true;
            hingeDoorRight.TriggerToVehicles = false;
            hingeDoorRight.OpenSound = CyclopsDefaultAssets.PLAYER_HATCH_OPEN;
            hingeDoorRight.CloseSound = CyclopsDefaultAssets.PLAYER_HATCH_CLOSE;

            var vehicleDoorLeft = manta.FindChild("PointsOfInterest").FindChild("VehicleEntranceLeftFlap");
            vehicleDoorLeft.GetComponentInChildren<HingeJoint>().connectedBody = manta.GetComponent<Rigidbody>();
            var hingeVehicleDoorLeft = vehicleDoorLeft.EnsureComponent<HingeJointDoor>();
            hingeVehicleDoorLeft.OverwriteTargetVelocity = true;
            hingeVehicleDoorLeft.TargetVelocity = 150f;
            hingeVehicleDoorLeft.TriggerToEverything = false;
            hingeVehicleDoorLeft.TriggerToPlayer = false;
            hingeVehicleDoorLeft.TriggerToVehicles = true;
            hingeVehicleDoorLeft.OpenSound = CyclopsDefaultAssets.DOCKING_DOORS_OPEN;
            hingeVehicleDoorLeft.CloseSound = CyclopsDefaultAssets.DOCKING_DOORS_CLOSE;

            var vehicleDoorRight = manta.FindChild("PointsOfInterest").FindChild("VehicleEntranceRightFlap");
            vehicleDoorRight.GetComponentInChildren<HingeJoint>().connectedBody = manta.GetComponent<Rigidbody>();
            var hingeVehicleDoorRight = vehicleDoorRight.EnsureComponent<HingeJointDoor>();
            hingeVehicleDoorRight.OverwriteTargetVelocity = true;
            hingeVehicleDoorRight.TargetVelocity = 150f;
            hingeVehicleDoorRight.TriggerToEverything = false;
            hingeVehicleDoorRight.TriggerToPlayer = false;
            hingeVehicleDoorRight.TriggerToVehicles = true;
            hingeVehicleDoorRight.OpenSound = CyclopsDefaultAssets.DOCKING_DOORS_OPEN;
            hingeVehicleDoorRight.CloseSound = CyclopsDefaultAssets.DOCKING_DOORS_CLOSE;

            var entrancePosition = manta.FindChild("PointsOfInterest").FindChild("EntranceTeleportSpot");
            var entranceHatch = manta.FindChild("PointsOfInterest").FindChild("PlayerEntrance").FindChild("Base");
            var entranceHatchTeleport = entranceHatch.EnsureComponent<EntranceHatch>();
            entranceHatchTeleport.HoverText = "Board Manta";
            entranceHatchTeleport.HoverHandReticle = HandReticle.IconType.Hand;
            entranceHatchTeleport.TeleportTarget = entrancePosition;
            entranceHatchTeleport.Submarine = mantaSubmarine;
            entranceHatchTeleport.EnteringSubmarine = true;

            var leavePosition = manta.FindChild("PointsOfInterest").FindChild("LeaveTeleportSpot");
            var leaveHatch = manta.FindChild("PointsOfInterest").FindChild("PlayerEntrance").FindChild("Top");
            var leaveHatchTeleport = leaveHatch.EnsureComponent<EntranceHatch>();
            leaveHatchTeleport.HoverText = "Disembark Manta";
            leaveHatchTeleport.HoverHandReticle = HandReticle.IconType.Hand;
            leaveHatchTeleport.TeleportTarget = leavePosition;
            leaveHatchTeleport.Submarine = mantaSubmarine;
            leaveHatchTeleport.EnteringSubmarine = false;

            var steeringConsolePOI = manta.FindChild("PointsOfInterest").FindChild("SteeringConsole");
            var steeringConsoleLeftHandTarget = manta.FindChild("PointsOfInterest").FindChild("SteeringConsole").FindChild("LeftIKTarget");
            var steeringConsoleRightHandTarget = manta.FindChild("PointsOfInterest").FindChild("SteeringConsole").FindChild("RightIKTarget");
            var playerParentWhilePiloting = manta.FindChild("PointsOfInterest").FindChild("SteeringConsole").FindChild("PlayerLockedWhileSteeringPosition");
            var steeringConsole = steeringConsolePOI.EnsureComponent<SteeringConsole>();
            steeringConsole.MovementController = manta.GetComponent<MovementController>();
            steeringConsole.ParentWhilePilotingGO = playerParentWhilePiloting;
            steeringConsole.LeftHandIKTarget = steeringConsoleLeftHandTarget;
            steeringConsole.RightHandIKTarget = steeringConsoleRightHandTarget;
            steeringConsole.Submarine = mantaSubmarine;

            manta.EnsureComponent<CyclopsCollisionSounds>();

            var normalSpeedMovementData = new MovementData
            {
                ForwardAccelerationSpeed = 5f,
                BackwardsAccelerationSpeed = 3f,
                AscendDescendSpeed = 3f,
                RotationSpeed = 0.3f,
                StrafeSpeed = 2f
            };

            var engineManager = manta.EnsureComponent<EngineManager>();
            engineManager.SetMovementDataForEngineState(EngineState.SLOW, normalSpeedMovementData);
            engineManager.SetMovementDataForEngineState(EngineState.NORMAL, normalSpeedMovementData);
            engineManager.SetMovementDataForEngineState(EngineState.FAST, normalSpeedMovementData);
            engineManager.SetMovementDataForEngineState(EngineState.SPECIAL, normalSpeedMovementData);

            manta.EnsureComponent<CyclopsStartupPowerDownSequence>();
            manta.EnsureComponent<CyclopsEngineStateChangedCallouts>();
            movementController.EngineManager = engineManager;
            manta.EnsureComponent<CyclopsWelcomeCallout>();
            var cyclopsEngineSound = manta.EnsureComponent<CyclopsEngineSound>();
            cyclopsEngineSound.RampUpSpeed = 0.2f;
            cyclopsEngineSound.RampDownSpeed = 0.2f;
            movementController.EngineSound = cyclopsEngineSound;

            var oxygenReplenishment = manta.EnsureComponent<OxygenReplenishment>();
            oxygenReplenishment.OxygenPerSecond = 15f;
            oxygenReplenishment.OxygenEnergyCost = 0.1f;

            var liveMixin = manta.EnsureComponent<LiveMixin>();
            liveMixin.data = CyclopsLiveMixinData.Get();// TO:DO Create a proper health system for the manta.
            liveMixin.data.knifeable = true; // TO:DO remove just here for testing purposes.
            liveMixin.data.maxHealth = 500;

            var externalLights = manta.FindChild("Lights").FindChild("Exterior");
            var internalLights = manta.FindChild("Lights").FindChild("Interior");

            var lightsManager = manta.EnsureComponent<LightsManager>();
            var externalLightsList = externalLights.GetComponentsInChildren<Light>().ToList();
            var internalLightsList = internalLights.GetComponentsInChildren<Light>().ToList();
            
            lightsManager.ExternalLights = externalLightsList;
            lightsManager.InternalLights = internalLightsList;
            lightsManager.ExternalLightsOnIntensity = 1.5f;
            lightsManager.ExternalLightsOffIntensity = 0f;
            lightsManager.InternalLightsOnIntensity = 1.5f;
            lightsManager.InternalLightsOffIntensity = 0f;
            lightsManager.EnableInternalLightsOnStart = true;
            lightsManager.EnableExternalLightsOnStart = true;

            var emergencyLighting = manta.EnsureComponent<CyclopsUnderAttackEmergencyLighting>();
            emergencyLighting.LightsAffected = internalLightsList;

            manta.EnsureComponent<CyclopsUnderAttackCallout>();

            var externalDamageManager = manta.EnsureComponent<ExternalDamageManager>();
            externalDamageManager.DamagePoints = manta.FindChild("PointsOfInterest").FindChild("DamagePoints").transform;
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

            var internalFireManager = manta.EnsureComponent<InternalFireManager>();
            internalFireManager.SubmarineLiveMixin = liveMixin;
            internalFireManager.FirePoints = manta.FindChild("PointsOfInterest").FindChild("FirePoints").transform;
            internalFireManager.FirePrefabs = new List<GameObject>()
            {
                CyclopsDefaultAssets.CYCLOPS_FIRE,
            };
            internalFireManager.DamageDonePerFirePerSecond = 5f;
            internalFireManager.Submarine = mantaSubmarine;
            internalFireManager.ChancePerDamageTakenToSpawnFire = 5;

            var autoRegen = manta.EnsureComponent<AutoRegenConditional>();
            autoRegen.InternalFireManager = internalFireManager;
            autoRegen.ExternalDamageManager = externalDamageManager;
            autoRegen.LiveMixin = liveMixin;
            autoRegen.RegenPerSecond = 2f;

            var internalLeakManage = manta.EnsureComponent<InternalLeakManager>();
            internalLeakManage.LeakPrefabs = new List<GameObject>()
            {
                CyclopsDefaultAssets.WATER_LEAK
            };

            var deathManager = manta.EnsureComponent<DeathManager>();
            deathManager.DeathPreparationTime = 22f;

            var basicDeath = manta.EnsureComponent<BasicDeath>();
            basicDeath.TimeTillDeletionOfSub = 60f;
            basicDeath.FallSpeed = 2f;

            var cyclopsDeathExplosion = manta.EnsureComponent<CyclopsDeathExplosion>();
            cyclopsDeathExplosion.TimeToExplosionAfterDeath = 18f;
            cyclopsDeathExplosion.FMODAsset = CyclopsDefaultAssets.CYCLOPS_EXPLOSION_FMOD;

            var cyclopsAbandonShip = manta.EnsureComponent<CyclopsAbandonShip>();
            cyclopsAbandonShip.TimeToCalloutAfterDeath = 0f;
            cyclopsAbandonShip.FMODAsset = CyclopsDefaultAssets.AI_ABANDON;

            manta.EnsureComponent<DestabiliseOnSubDeath>();
            manta.EnsureComponent<KillPlayerInsideOnSubDeath>();

            var pingType = SMLHelper.V2.Handlers.PingHandler.Main.RegisterNewPingType("SubmarineManta", new Atlas.Sprite(Assets.PING_ICON));
            var pingInstance = manta.EnsureComponent<PingInstance>();
            pingInstance.pingType = pingType;
            pingInstance.maxDist = 15;
            pingInstance.minDist = 20;
            pingInstance.origin = manta.transform;
            pingInstance.SetLabel("HMS Unknown Manta");
        }

        private static IEnumerable<ApplyMaterial> applyMaterials = new List<ApplyMaterial>()
        {
            new ApplyExteriorTailMaterial(),
            new ApplyExteriorBodyMaterial(),
            new ApplyExteriorWingsMaterial()
        };
        
        private static void SetupMaterials(GameObject manta)
        {
            applyMaterials.ForEach(am => am.Apply(manta));
        }
    }
}