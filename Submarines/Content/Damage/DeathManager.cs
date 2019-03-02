using System.Collections;
using UnityEngine;

namespace Submarines.Content.Damage
{
    /**
     * Handles dispatching the events that alot of our components that do stuff with the death of the submarine require.
     */
    public class DeathManager : MonoBehaviour
    {
        public float DeathPreparationTime { get; set; } = 10f;
        private bool isDyingAlready = false;

        public void Start()
        {
            LiveMixin liveMixin = gameObject.GetComponent<LiveMixin>();
            if (liveMixin == null)
            {
                Utilities.Log.Error("DeathManager can't find the submarines's livemixin. Destroying...");
                Destroy(this);
                return;
            }

            if (liveMixin.broadcastKillOnDeath == false)
            {
                Utilities.Log.Error("Deathmanager requires the submarine's broadcastKillOnDeath field to be set to true. Destroying...");
                Destroy(this);
                return;
            }
        }

        public void OnKill()
        {
            StartDeath();
        }

        public void StartDeath()
        {
            if (isDyingAlready)
                return;

            isDyingAlready = true;
            StartCoroutine(HandleDeath());
        }

        private IEnumerator HandleDeath()
        {
            SendMessage("OnDeathPrepare", SendMessageOptions.DontRequireReceiver);
            yield return new WaitForSeconds(DeathPreparationTime);
            SendMessage("OnDeathOccuring", SendMessageOptions.DontRequireReceiver);
        }
    }
}
