using UnityEngine;

namespace Submarines.Movement
{
    /**
     * Stabilises the submarine in terms of roll and pitch. Yaw is controllable so is not affected by the Stabiliser.
     */
    public class MovementStabiliser : MonoBehaviour
    {
        public bool IsStabilising { get; set; } = true;
        private Rigidbody rigidbody;
        private MovementData movementData;

        public void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            movementData = gameObject.GetComponent<MovementData>();
        }

        public void FixedUpdate()
        {
            if (rigidbody == null)
            {
                return;
            }

            if (IsStabilising && rigidbody.isKinematic == false)
            {
                // Was working with transform.forward
                Vector3 vector = rigidbody.transform.position + rigidbody.transform.up;
                Vector3 force = 10f * ((rigidbody.transform.position + Vector3.up) - vector);
                rigidbody.AddForceAtPosition(force, vector, ForceMode.Acceleration);

                vector = rigidbody.transform.position - rigidbody.transform.up;
                force = 10f * ((rigidbody.transform.position - Vector3.up) - vector);
                rigidbody.AddForceAtPosition(force, vector, ForceMode.Acceleration);
            }
        }
    }
}
