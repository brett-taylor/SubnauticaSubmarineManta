using UnityEngine;

/**
* Stores information about movement speed and restrictions.
* Come with some handy default settings
*/
namespace Submarines.Movement
{
    public class MovementData : MonoBehaviour
    {
        public static float CONTROL_DEADZONE { get; set; } = 0.05f; 
        public float ForwardAccelerationSpeed { get; set; } = 3f;
        public float BackwardsAccelerationSpeed { get; set; } = 1.6f;
        public float AscendDescendSpeed { get; set; } = 1.4f;
        public float RotationSpeed { get; set; } = 0.6f;
    }
}
