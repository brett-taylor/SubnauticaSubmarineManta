using Submarines.Content.Death;
using System.Collections;
using UnityEngine;

namespace Submarines.DefaultCyclopsContent
{
    /**
     * AI callout to abandon the ship.
     */ 
    public class CyclopsAbandonShip : MonoBehaviour
    {
        public float TimeToCalloutAfterDeath { get; set; } = 0f;
        public FMODAsset FMODAsset { get; set; }
        private bool isInside = false;

        public void Start()
        {
            if (gameObject.GetComponent<DeathManager>() == null)
            {
                Utilities.Log.Error("CyclopsAbandonShip can't find the submarines's DeathManager. Destroying...");
                Destroy(this);
                return;
            }

            if (EntryPoint.LOAD_DEFAULT_CYCLOPS_ASSETS && FMODAsset == null)
            {
                FMODAsset = CyclopsDefaultAssets.AI_ABANDON;
            }

            if (FMODAsset == null)
            {
                Utilities.Log.Error("CyclopsDeathExplosion::FMODAsset is not set. Destroying self");
                Destroy(this);
                return;
            }
        }

        public void OnPlayerEnteredSubmarine()
        {
            isInside = true;
        }

        public void OnPlayerExitedSubmarine()
        {
            isInside = false;
        }

        public void OnDeathPrepare()
        {
            if (isInside)
            {
                StartCoroutine(SartCallout());
            }
        }

        private IEnumerator SartCallout()
        {
            yield return new WaitForSeconds(TimeToCalloutAfterDeath);
            Utils.PlayFMODAsset(FMODAsset, MainCamera.camera.transform, 20f);
        }
    }
}
