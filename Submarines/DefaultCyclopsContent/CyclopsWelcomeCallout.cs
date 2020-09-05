using UnityEngine;

namespace Submarines.DefaultCyclopsContent
{
    /**
     * Adds the welcome aboard message when the play enters.
     * 
     * If you wish to set the sounds that the cyclops use.
     * Set the following:
     * WelcomeAboardCallout to the FMOD asset named "AI_welcome"
     */
    public class CyclopsWelcomeCallout : MonoBehaviour
    {
        public FMODAsset WelcomeAboardCallout { get; set; } = null;

        public void Start()
        {
            if (EntryPoint.LOAD_DEFAULT_CYCLOPS_ASSETS && WelcomeAboardCallout == null)
            {
                WelcomeAboardCallout = CyclopsDefaultAssets.AI_WELCOME_ABOARD_GOOD;
            }
        }

        public void OnPlayerEnteredSubmarine()
        {
            if (WelcomeAboardCallout != null)
            {
                Utils.PlayFMODAsset(WelcomeAboardCallout, MainCamera.camera.transform, 20f);
            }
        }
    }
}
 