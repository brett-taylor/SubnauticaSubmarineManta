using Submarines.Engine;
using System.Collections;
using UnityEngine;

namespace Submarines.DefaultCyclopsContent
{
    /**
     * Adds the engine start up sequence found on the cyclops. Includs sound (if set) and screen shake.
     * 
     * If you wish to set the sounds that the cyclops use.
     * Set the following:
     * StartupSound to the FMOD asset named "ai_engine_up"
     * PowerDownCallout to the FMOD asset named "ai_engine_down"
     */
    public class CyclopsStartupPowerDownSequence : MonoBehaviour
    {
        public FMODAsset StartupSound { get; set; } = null;
        public FMODAsset PowerDownCallout { get; set; } = null;
        public static readonly float TOTAL_START_UP_DELAY = 4.5f;

        public void Start()
        {
            if (EntryPoint.LOAD_DEFAULT_CYCLOPS_ASSETS)
            {
                if (StartupSound == null)
                {
                    StartupSound = CyclopsDefaultAssets.ENGINE_STATE_POWER_UP;
                }

                if (PowerDownCallout == null)
                {
                    PowerDownCallout = CyclopsDefaultAssets.ENGINE_STATE_POWER_DOWN;
                }
            }
        }

        public void OnEngineStartUp()
        {
            if (StartupSound != null)
            {
                Utils.PlayFMODAsset(StartupSound, MainCamera.camera.transform, 20f);
            }

            StartCoroutine(EngineStartCameraShake(0.15f, 4.5f, 0f));
            StartCoroutine(EngineStartCameraShake(1f, -1f, 4.6f));
        }

        public void OnEnginePowerDown()
        {
            if (PowerDownCallout != null)
            {
                Utils.PlayFMODAsset(PowerDownCallout, MainCamera.camera.transform, 20f);
            }
        }

        private IEnumerator EngineStartCameraShake(float intensity, float duration, float delay)
        {
            yield return new WaitForSeconds(delay);
            MainCameraControl.main.ShakeCamera(intensity, duration, MainCameraControl.ShakeMode.Linear, 1f);
            yield break;
        }
    }
}
 