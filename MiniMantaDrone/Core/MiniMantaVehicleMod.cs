using MiniMantaDrone.Utilities;
using SMLHelper.V2.Assets;
using Submarines.Utilities.Extension;
using UnityEngine;

namespace MiniMantaDrone.Core
{
    /**
    * The Mini Manta Drone
    */
    public class MiniMantaDroneMod : Spawnable
    {
        public static TechType MINI_MANTA_DRONE_TECH_TYPE = new MiniMantaDroneMod().TechType;
        public override string AssetsFolder => EntryPoint.ASSET_FOLDER_LOCATION;
        public override string IconFileName => "MantaIcon.png";

        public MiniMantaDroneMod() : base("SubmarineMiniMantaDrone", "Mini Manta Drone", "A drone built for the Manta submarine.")
        {
        }

        public override GameObject GetGameObject()
        {
            return CreateMiniMantaVehicle();
        }

        /*
        * The first part of setting up the mini manta drone. If the components are self contained and do not rely on other components
        * adding it here works fine.
        */
        public static GameObject CreateMiniMantaVehicle()
        {
            GameObject vehicle = Object.Instantiate(MiniMantaDroneAssetLoader.MINI_MANTA_DRONE_EXTERIOR);
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

            vehicle.GetOrAddComponent<TechTag>().type = MINI_MANTA_DRONE_TECH_TYPE;
            vehicle.GetOrAddComponent<PrefabIdentifier>().ClassId = "SubmarineMiniMantaDrone";
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
