using Submarines.Movement;
using UnityEngine;

namespace Submarines.Content.Death
{
    public class DestabiliseOnSubDeath : MonoBehaviour
    {
        public void Start()
        {
            if (gameObject.GetComponent<DeathManager>() == null)
            {
                Utilities.Log.Error("DestabiliseOnSubDeath can't find the submarines's DeathManager. Destroying...");
                Destroy(this);
                return;
            }

            if (gameObject.GetComponent<MovementStabiliser>() == null)
            {
                Utilities.Log.Error("DestabiliseOnSubDeath can't find the submarines's MovementStabiliser. Destroying...");
                Destroy(this);
                return;
            }
        }

        public void OnDeathOccuring()
        {
            MovementStabiliser movementStabiliser = GetComponent<MovementStabiliser>();
            if (movementStabiliser != null)
            {
                movementStabiliser.enabled = false;
            }
        }
    }
}
