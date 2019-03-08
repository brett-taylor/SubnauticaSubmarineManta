using Submarines.DefaultCyclopsContent;
using Submarines.Engine;
using UnityEngine;

/**
 * Handles input and submarine movement
 */
namespace Submarines.Movement
{
    public class MovementController : MonoBehaviour
    {
        private static readonly KeyCode MODIFIER_KEY_CODE = KeyCode.LeftShift;
        public bool IsControllable { get; set; }
        public Vector3 LocalVelocity { get; set; }
        public Transform ApplyForceLocation { get; set; }
        public MovementData MovementData { get; set; }
        public EngineManager EngineManager { get; set; }
        public CyclopsEngineSound EngineSound { get; set; }

        private Rigidbody rigidbody;
        private Vector3 throttle;
        private bool isModifierDown = false;

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
            isModifierDown = Input.GetKey(MODIFIER_KEY_CODE);
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
            if (!isModifierDown)
            {
                rigidbody.AddTorque(transform.up * (throttle.x * movementData.RotationSpeed), ForceMode.Acceleration);
            }

            // Strafe
            if (isModifierDown)
            {
                rigidbody.AddForce(throttle.x * movementData.StrafeSpeed * transform.right, ForceMode.Acceleration);
            }

            if (EngineSound != null)
            {
                EngineSound.AccelerateInput(throttle.magnitude >= 1 ? movementData.RPMSpeedThrottleDown : 0f);
            }
        }
    }
}
