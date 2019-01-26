using Submarines.Engine;
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
        public Transform ApplyForceLocation { get; set; }
        public MovementData MovementData { get; set; }
        public EngineManager EngineManager { get; set; }

        private Rigidbody rigidbody;
        private Vector3 throttle;

        public void Start()
        {
            rigidbody = gameObject.GetComponent<Rigidbody>();

            if (MovementData == null && EngineManager == null)
            {
                Utilities.Log.Error("MovementController - Both MovementData and EngineManager are null. One of them has to be valid");
            }
        }

        public void Update()
        {
            if (IsControllable == false)
            {
                return;
            }

            throttle = GameInput.GetMoveDirection();
        }

        private MovementData GetCurrentMovementData()
        {
            if (EngineManager != null)
            {
                return EngineManager.CurrentMovementData;
            }

            if (MovementData != null)
            {
                return MovementData;
            }

            return MovementData.Zero();
        }

        public void FixedUpdate()
        {
            // Throttle: y up/down x left/right z forward/backward
            // Local Velocity: z up/down. y forward/backwrads. x left/right
            LocalVelocity = transform.InverseTransformDirection(rigidbody.velocity);
            MovementData movementData = GetCurrentMovementData();

            if (IsControllable == false || rigidbody == null || rigidbody.isKinematic == true)
            {
                return;
            }

            // Forward
            if (throttle.z > MovementData.CONTROL_DEADZONE)
            {
                if (ApplyForceLocation == null)
                {
                    rigidbody.AddForce(throttle.z * movementData.ForwardAccelerationSpeed * transform.forward, ForceMode.Acceleration);
                }
                else
                {
                    rigidbody.AddForceAtPosition(throttle.z * movementData.ForwardAccelerationSpeed * transform.forward, ApplyForceLocation.position, ForceMode.Acceleration);
                }
            }

            // Backwards
            if (throttle.z < -MovementData.CONTROL_DEADZONE)
            {
                if (ApplyForceLocation == null)
                {
                    rigidbody.AddForce(throttle.z * movementData.BackwardsAccelerationSpeed * transform.forward, ForceMode.Acceleration);
                }
                else
                {
                    rigidbody.AddForceAtPosition(throttle.z * movementData.BackwardsAccelerationSpeed * transform.forward, ApplyForceLocation.position, ForceMode.Acceleration);
                }
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
