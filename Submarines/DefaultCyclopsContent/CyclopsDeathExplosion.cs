using Submarines.Content.Death;
using System.Collections;
using UnityEngine;

namespace Submarines.DefaultCyclopsContent
{
    /**
     * Listens to a death event and spawns the default cyclops explosion.
     */
    public class CyclopsDeathExplosion : MonoBehaviour
    {
        public float TimeToExplosionAfterDeath { get; set; } = 2f;
        public FMODAsset FMODAsset { get; set; }

        public void Start()
        {
            if (gameObject.GetComponent<DeathManager>() == null)
            {
                Utilities.Log.Error("CyclopsDeathExplosion can't find the submarines's DeathManager. Destroying...");
                Destroy(this);
                return;
            }

            if (EntryPoint.LOAD_DEFAULT_CYCLOPS_ASSETS && FMODAsset == null)
            {
                FMODAsset = CyclopsDefaultAssets.CYCLOPS_EXPLOSION_FMOD;
            }

            if (FMODAsset == null)
            {
                Utilities.Log.Error("CyclopsDeathExplosion::FMODAsset is not set. Destroying self");
                Destroy(this);
                return;
            }
        }


        public void OnDeathPrepare()
        {
            StartCoroutine(StartExplosion());
        }

        private IEnumerator StartExplosion()
        {
            yield return new WaitForSeconds(TimeToExplosionAfterDeath);
            Utils.PlayFMODAsset(FMODAsset, MainCamera.camera.transform, 20f);
            // Spawn explosion
            // Do sound effects.
        }
    }
}
