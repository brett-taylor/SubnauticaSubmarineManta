using UnityEngine;

namespace Submarines.PointsOfInterest
{
    /**
     * Add component to create the flap seen on the cyclops where the person or a vehicle enters.
     * Relies on the motor being set on the hinge joint and limits being set.
     */
    public class HingeJointDoor : MonoBehaviour
    {
        public bool TriggerToEverything = false;
        public bool TriggerToPlayer = true;
        public bool TriggerToVehicles = false;
        public float TargetVelocity { get; set; }
        public bool OverwriteTargetVelocity { get; set; } = false;
        public FMODAsset OpenSound { get; set; } = null;
        public FMODAsset CloseSound { get; set; } = null;

        private HingeJoint hingeJoint;

        public void Start()
        {
            hingeJoint = GetComponentInChildren<HingeJoint>();
            if (hingeJoint == null)
            {
                Utilities.Log.Error("HingeJoint for HingeJointDoor can not be found in any child - deleting.");
                Destroy(this);
            }

            if (OverwriteTargetVelocity == false)
            {
                TargetVelocity = hingeJoint.motor.targetVelocity;
            }

            SetMotor(-TargetVelocity);
        }

        public void Open()
        {
            SetMotor(TargetVelocity);
        }

        public void Close()
        {
            SetMotor(-TargetVelocity);
        }

        public void Stop()
        {
            SetMotor(0);
        }

        private void SetMotor(float targetVelocity)
        {
            JointMotor motor = hingeJoint.motor;
            motor.targetVelocity = targetVelocity;
            hingeJoint.motor = motor;
            hingeJoint.useMotor = true;
        }

        public void OnTriggerEnter(Collider c)
        {
            bool shouldExecute = true;

            if (TriggerToEverything == false)
            {
                if (IsPlayerOrVehicle(c) == false)
                {
                    return;
                }

                if (TriggerToPlayer == false && IsPlayer(c))
                {
                    return;
                }

                if (TriggerToVehicles == false && IsVehicle(c))
                {
                    return;
                }
            }

            if (shouldExecute)
            {
                Open();
                if (OpenSound != null)
                {
                    Utils.PlayFMODAsset(OpenSound, gameObject.transform.position);
                }
            }
        }

        public void OnTriggerExit(Collider c)
        {
            bool shouldExecute = true;

            if (TriggerToEverything == false)
            {
                if (IsPlayerOrVehicle(c) == false)
                {
                    return;
                }

                if (TriggerToPlayer == false && IsPlayer(c))
                {
                    return;
                }

                if (TriggerToVehicles == false && IsVehicle(c))
                {
                    return;
                }
            }

            if (shouldExecute)
            {
                Close();
                if (CloseSound != null)
                {
                    Utils.PlayFMODAsset(CloseSound, gameObject.transform.position);
                }
            }
        }

        private bool IsPlayerOrVehicle(Collider c)
        {
            return IsPlayer(c) || IsVehicle(c);
        }

        private bool IsPlayer(Collider c)
        {
            return c.tag == "Player";
        }

        private bool IsVehicle(Collider c)
        {
            return c.tag == "Untagged" && (Player.main.GetVehicle() != null);
        }
    }
}
