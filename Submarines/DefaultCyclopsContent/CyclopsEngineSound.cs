using UnityEngine;

namespace Submarines.DefaultCyclopsContent
{
    /**
     * Adds the engine rpm sound as found on the cyclops. 
     * 
     * If youre using a MovementController then you must set MovementController.EngineSound to this.
     * If not you must call AccelerateInput
     * If you wish to set it to the cyclops sounds:
     * Set FMODAsset to the FMod asset called "cyclops_loop_rpm".
     */
    public class CyclopsEngineSound : MonoBehaviour
    {
        public FMODAsset FMODAsset { get; set; }
        public float RampUpSpeed { get; set; } = 0.2f;
        public float RampDownSpeed { get; set; } = 0.2f;

        private FMOD_CustomEmitter engineEmitter;
        private int rpmIndex;
        private bool accelerating;
        private float topClampSpeed = 1f;
        private float rpmSpeed;

        public void Start()
        {
            if (EntryPoint.LOAD_DEFAULT_CYCLOPS_ASSETS && FMODAsset == null)
            {
                FMODAsset = CyclopsDefaultAssets.ENGINE_LOOP;
            }

            if (FMODAsset == null)
            {
                Utilities.Log.Error("CyclopsEngineSound::FMODAsset is not set. Destroying self");
                Destroy(this);
                return;
            }

            engineEmitter = gameObject.AddComponent<FMOD_CustomLoopingEmitter>();
            engineEmitter.asset = FMODAsset;
            rpmIndex = engineEmitter.GetParameterIndex("rpm");
        }

        public void Update()
        {
            if (accelerating)
            {
                rpmSpeed = Mathf.MoveTowards(rpmSpeed, topClampSpeed, Time.deltaTime * RampUpSpeed);
            }
            else
            {
                rpmSpeed = Mathf.MoveTowards(rpmSpeed, 0f, Time.deltaTime * RampDownSpeed);
            }

            if (rpmSpeed > 0f)
            {
                engineEmitter.Play();
            }
            else
            {
                engineEmitter.Stop();
            }

            engineEmitter.SetParameterValue(rpmIndex, rpmSpeed);
            accelerating = false;
        }

        public void AccelerateInput(float topClamp = 1f)
        {
            accelerating = true;
            topClampSpeed = topClamp;
        }
    }
}
