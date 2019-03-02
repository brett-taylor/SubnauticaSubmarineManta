using System.Collections;
using UnityEngine;

namespace Submarines.Content.Death
{
    /**
     * A basic death.
     * Listens to a death event and lets the sub fall to the floor and removes it X amount of seconds afterwards.
     */
    public class BasicDeath : MonoBehaviour
    {
        public float TimeTillDeletionOfSub { get; set; } = 30f;
        public float FallSpeed { get; set; } = 2f;

        public void Start()
        {
            if (gameObject.GetComponent<DeathManager>() == null)
            {
                Utilities.Log.Error("BasicDeath can't find the submarines's DeathManager. Destroying...");
                Destroy(this);
                return;
            }

            if (gameObject.GetComponent<WorldForces>() == null)
            {
                Utilities.Log.Error("BasicDeath can't find the submarines's WorldForces. Destroying...");
                Destroy(this);
                return;
            }
        }

        public void OnDeathOccuring()
        {
            StartCoroutine(DoBasicDeath());
        }

        private IEnumerator DoBasicDeath()
        {
            GetComponent<WorldForces>().underwaterGravity = FallSpeed;
            yield return new WaitForSeconds(TimeTillDeletionOfSub);
            Destroy(gameObject);
        }
    }
}
