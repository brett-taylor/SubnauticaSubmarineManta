using MiniMantaVehicle.Utilities;
using SMLHelper.V2.Assets;
using Submarines.Utilities.Extension;
using UnityEngine;

namespace MiniMantaVehicle.Core
{
    /**
    * The Mini Manta Vehicle
    */
    public class MiniMantaVehicleMod : Spawnable
    {
        public static TechType MINI_MANTA_VEHICLE_TECH_TYPE = new MiniMantaVehicleMod().TechType;
        public override string AssetsFolder => EntryPoint.MOD_FOLDER_NAME + EntryPoint.ASSET_FOLDER_NAME;
        public override string IconFileName => "MantaIcon.png";

        public MiniMantaVehicleMod() : base("SubmarineMiniMantaVehicle", "Mini Manta Vehicle", "A driviable vehicle named the Mini Manta")
        {
        }

        public override GameObject GetGameObject()
        {
            return CreateMiniMantaVehicle();
        }

        /*
        * The first part of setting up the mini manta vehicle. If the components are self contained and do not rely on other components
        * adding it here works fine.
        */
        public static GameObject CreateMiniMantaVehicle()
        {
            GameObject vehicle = Object.Instantiate(MiniMantaVehicleAssetLoader.MINI_MANTA_VEHICLE_EXTERIOR);
            ApplyMaterials(vehicle);

            SkyApplier skyApplier = vehicle.GetOrAddComponent<SkyApplier>();
            skyApplier.renderers = vehicle.GetComponentsInChildren<Renderer>();
            skyApplier.anchorSky = Skies.Auto;

            vehicle.GetOrAddComponent<VFXSurface>();
            vehicle.GetOrAddComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Global;

            Rigidbody rb = vehicle.GetOrAddComponent<Rigidbody>();
            rb.angularDrag = 1f;
            rb.mass = 12000f;
            rb.useGravity = false;
            rb.centerOfMass = new Vector3(-0.1f, 0.8f, -1.7f);

            WorldForces forces = vehicle.GetOrAddComponent<WorldForces>();
            forces.aboveWaterDrag = 0f;
            forces.aboveWaterGravity = 9.81f;
            forces.handleDrag = true;
            forces.handleGravity = true;
            forces.underwaterDrag = 0.5f;
            forces.underwaterGravity = 0;

            vehicle.GetOrAddComponent<TechTag>().type = MINI_MANTA_VEHICLE_TECH_TYPE;
            vehicle.GetOrAddComponent<PrefabIdentifier>().ClassId = "SubmarineMiniMantaVehicle";
            return vehicle;
        }

        /**
         * Apply the materials
         */
        private static void ApplyMaterials(GameObject vehicle)
        {
            Shader shader = Shader.Find("MarmosetUBER");
            Renderer[] renderers = vehicle.GetAllComponentsInChildren<Renderer>();
            foreach(Renderer renderer in renderers)
            {
                foreach(Material mat in renderer.materials)
                {
                    mat.shader = shader;
                }
            }
        }
    }
}
