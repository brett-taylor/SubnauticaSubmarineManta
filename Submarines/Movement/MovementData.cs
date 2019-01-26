using UnityEngine;

/**
* Stores information about movement speed and restrictions.
* Come with some handy default settings
*/
namespace Submarines.Movement
{
    public class MovementData
    {
        public static float CONTROL_DEADZONE { get; set; } = 0.05f; 
        public float ForwardAccelerationSpeed { get; set; } = 5f;
        public float BackwardsAccelerationSpeed { get; set; } = 3f;
        public float AscendDescendSpeed { get; set; } = 1.4f;
        public float RotationSpeed { get; set; } = 0.3f;

        public static MovementData Zero()
        {
            return new MovementData
            {
                ForwardAccelerationSpeed = 0f,
                BackwardsAccelerationSpeed = 0f,
                AscendDescendSpeed = 0f,
                RotationSpeed = 0f
            };
        }
    }
}
