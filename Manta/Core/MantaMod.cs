using Manta.Utilities;
using SMLHelper.V2.Assets;
using Submarines;
using Submarines.Miscellaneous;
using Submarines.Movement;
using Submarines.PointsOfInterest;
using Submarines.Utilities.Extension;
using Submarines.Water;
using UnityEngine;

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
            Log.Print("Created Manta");
            return CreateManta();
        }

        /*
         * The first part of setting up the manta. If the components are self contained and do not rely on other components
         * adding it here works fine.
         */
        public static GameObject CreateManta()
        {
            GameObject submarine = Object.Instantiate(MantaAssetLoader.MANTA_EXTERIOR);

            Shader shader = Shader.Find("MarmosetUBER");
            Renderer[] renderers = submarine.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                renderer.material.shader = shader;
            }

            MeshRenderer exteriorRenderer = submarine.FindChild("Model")?.FindChild("Exterior Walls")?.GetComponent<MeshRenderer>();
            if (exteriorRenderer != null)
            {
                exteriorRenderer.material.EnableKeyword("MARMO_EMISSION");
                exteriorRenderer.material.SetTexture("_Illum", MantaAssetLoader.MANTA_EXTERIOR_EMISSION_MAP);
                exteriorRenderer.material.SetFloat("_EnableGlow", 1f);
                exteriorRenderer.UpdateGIMaterials();
            }

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
            submarine.GetOrAddComponent<MovementData>();
            submarine.GetOrAddComponent<MovementController>();
            submarine.GetOrAddComponent<MovementStabiliser>();
            submarine.GetOrAddComponent<WaterClipProxyModified>().Initialise();

            submarine.GetOrAddComponent<Components.MantaSerializationFixer>();
            return submarine;
        }

        /**
         * If the component requires other custom components then do it here.
         * Read the comment on Components.MantaseializationFixer if you wish to understand why this horrible system exists.
         */
        public static void SetUpManta(GameObject submarine)
        {
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
            entranceHatchTeleport.Submarine = submarine.GetComponent<MantaSubmarine>();
            entranceHatchTeleport.EnteringSubmarine = true;

            GameObject leavePosition = submarine.FindChild("PointsOfInterest").FindChild("LeaveTeleportSpot");
            GameObject leaveHatch = submarine.FindChild("PointsOfInterest").FindChild("PlayerEntrance").FindChild("Top");
            EntranceHatch leaveHatchTeleport = leaveHatch.GetOrAddComponent<EntranceHatch>();
            leaveHatchTeleport.HoverText = "Disembark Manta";
            leaveHatchTeleport.HoverHandReticle = HandReticle.IconType.Hand;
            leaveHatchTeleport.TeleportTarget = leavePosition;
            leaveHatchTeleport.Submarine = submarine.GetComponent<MantaSubmarine>();
            leaveHatchTeleport.EnteringSubmarine = false;

            GameObject steeringConsolePOI = submarine.FindChild("PointsOfInterest").FindChild("SteeringConsole");
            GameObject playerParentWhilePiloting = submarine.FindChild("PointsOfInterest").FindChild("PlayerLockedWhileSteeringPosition");
            SteeringConsole steeringConsole = steeringConsolePOI.GetOrAddComponent<SteeringConsole>();
            steeringConsole.MovementController = submarine.GetComponent<MovementController>();
            steeringConsole.ParentWhilePilotingGO = playerParentWhilePiloting;
        }
    }
}
