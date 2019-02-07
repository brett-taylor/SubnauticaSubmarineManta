using Submarines.Engine;
using UnityEngine;

namespace Submarines.DefaultCyclopsContent
{
    /**
     * Adds the AI call outs that are found on the cyclops.
     * 
     * If you wish to set the sounds that the cyclops use.
     * Set the following:
     * SilentRunningCallout to the FMOD asset named "ai_silent_running"
     * NormalCallout to the FMOD asset named "ai_ahead_standard"
     * SlowCallout to the FMOD asset named "ai_ahead_slow"
     * FastCallout to the FMOD asset named "ai_ahead_flank"
     */
    public class CyclopsEngineStateChangedCallouts : MonoBehaviour
    {
        public FMODAsset SilentRunningCallout { get; set; } = null;
        public FMODAsset NormalCallout { get; set; } = null;
        public FMODAsset SlowCallout { get; set; } = null;
        public FMODAsset FastCallout { get; set; } = null;
        private EngineManager engineManager = null;

        public void Start()
        {
            engineManager = GetComponent<EngineManager>();
            if (engineManager == null)
            {
                Utilities.Log.Error("CyclopsEngineStateChangedCallouts can not find EngineManager - deleting");
                Destroy(this);
            }

            if (EntryPoint.LOAD_DEFAULT_CYCLOPS_ASSETS)
            {
                if (SilentRunningCallout == null)
                {
                    SilentRunningCallout = CyclopsDefaultAssets.ENGINE_STATE_SILENT_RUNNING;
                }

                if (NormalCallout == null)
                {
                    NormalCallout = CyclopsDefaultAssets.ENGINE_STATE_AHEAD_STANDARD;
                }

                if (SlowCallout == null)
                {
                    SlowCallout = CyclopsDefaultAssets.ENGINE_STATE_AHEAD_SLOW;
                }

                if (FastCallout == null)
                {
                    FastCallout = CyclopsDefaultAssets.ENGINE_STATE_AHEAD_FAST;
                }
            }
        }

        public void OnEngineChangeState(EngineState oldEngineState)
        {
            if (engineManager.EngineState == EngineState.SLOW && SlowCallout != null)
            {
                Utils.PlayFMODAsset(SlowCallout, MainCamera.camera.transform, 20f);
            }
            else if (engineManager.EngineState == EngineState.NORMAL && NormalCallout != null)
            {
                Utils.PlayFMODAsset(NormalCallout, MainCamera.camera.transform, 20f);
            }
            else if (engineManager.EngineState == EngineState.FAST && FastCallout != null)
            {
                Utils.PlayFMODAsset(FastCallout, MainCamera.camera.transform, 20f);
            }
            else if (engineManager.EngineState == EngineState.SPECIAL && SilentRunningCallout != null)
            {
                Utils.PlayFMODAsset(SilentRunningCallout, MainCamera.camera.transform, 20f);
            }
            else if (engineManager.EngineState == EngineState.SPECIAL && SilentRunningCallout != null)
            {
                Utils.PlayFMODAsset(SilentRunningCallout, MainCamera.camera.transform, 20f);
            }
            else if (engineManager.EngineState == EngineState.SPECIAL && SilentRunningCallout != null)
            {
                Utils.PlayFMODAsset(SilentRunningCallout, MainCamera.camera.transform, 20f);
            }
        }
    }
}
