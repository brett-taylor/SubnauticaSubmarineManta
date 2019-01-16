using UnityEngine;

/**
 * Handles input and submarine movement
 */
namespace Submarines.Movement
{
    public class MovementController : MonoBehaviour
    {
        public bool IsControllable { get; set; }
        public Vector3 LocalVelocity { get; set; }

        private MovementData movementData;
        private Rigidbody rigidbody;
        private Vector3 throttle;

        public void Start()
        {
            rigidbody = gameObject.GetComponent<Rigidbody>();
            movementData = gameObject.GetComponent<Movement.MovementData>();
        }

        public void Update()
        {
            if (IsControllable == false)
            {
                return;
            }

            throttle = GameInput.GetMoveDirection();
        }

        public void FixedUpdate()
        {
            // Throttle: y up/down x left/right z forward/backward
            // Local Velocity: z up/down. y forward/backwrads. x left/right
            LocalVelocity = transform.InverseTransformDirection(rigidbody.velocity);

            if (IsControllable == false || rigidbody == null || rigidbody.isKinematic == true)
            {
                return;
            }

            // Forward
            if (throttle.z > MovementData.CONTROL_DEADZONE)
            {
                rigidbody.AddForce(throttle.z * movementData.ForwardAccelerationSpeed * transform.forward, ForceMode.Acceleration);

            }

            // Backwards
            if (throttle.z < -MovementData.CONTROL_DEADZONE)
            {
                rigidbody.AddForce(throttle.z * movementData.BackwardsAccelerationSpeed * transform.forward, ForceMode.Acceleration);
            }

            /// Ascend Descend
            if (Mathf.Abs(throttle.y) > MovementData.CONTROL_DEADZONE)
            {
                rigidbody.AddForce(throttle.y * movementData.AscendDescendSpeed * transform.up, ForceMode.Acceleration);

            }

            // Rotation
            rigidbody.AddTorque(transform.up * (throttle.x * movementData.RotationSpeed), ForceMode.Acceleration);

            // Tilt DEBUG
            float tilt = Input.GetKey(KeyCode.Q) ? 1f : Input.GetKey(KeyCode.E) ? -1f : 0f;
            rigidbody.AddTorque(transform.forward * (tilt * 0.3f), ForceMode.Acceleration);
        }
    }
}
