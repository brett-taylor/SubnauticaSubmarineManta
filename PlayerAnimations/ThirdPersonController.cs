using PlayerAnimations.Core;
using UnityEngine;

namespace PlayerAnimations
{
    /**
     * Entry point into this library
     */
    public static class ThirdPersonController
    {
        private static ThirdPersonCameraController thirdPersonCameraController;

        public static void Initialise()
        {
            if (thirdPersonCameraController == null)
            {
                thirdPersonCameraController = Camera.main.gameObject.AddComponent<ThirdPersonCameraController>();
                EnableFirstPerson();
            }
        }

        public static void Toggle()
        {
            if (IsInThirdPerson())
            {
                EnableFirstPerson();
            }
            else
            {
                EnableThirdPerson();
            }
        }

        public static void EnableThirdPerson()
        {
            thirdPersonCameraController.enabled = true;
        }

        public static void EnableFirstPerson()
        {
            thirdPersonCameraController.enabled = false;
        }

        public static bool IsInThirdPerson()
        {
            return thirdPersonCameraController.enabled;
        }
    }
}
