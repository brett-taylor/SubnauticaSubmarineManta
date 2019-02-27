using PlayerAnimations.Core;

namespace PlayerAnimations
{
    /**
     * Entry point into this library
     */
    public static class ThirdPersonController
    {
        private static ThirdPersonCameraController thirdPersonCameraController;

        private static void Initialise()
        {
            if (thirdPersonCameraController == null)
            {
                thirdPersonCameraController = MainCamera.camera.gameObject.AddComponent<ThirdPersonCameraController>();
                EnableFirstPerson();
            }
        }

        public static void Toggle()
        {
            Initialise();
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
            Initialise();
            thirdPersonCameraController.enabled = true;
        }

        public static void EnableFirstPerson()
        {
            Initialise();
            thirdPersonCameraController.enabled = false;
        }

        public static bool IsInThirdPerson()
        {
            Initialise();
            return thirdPersonCameraController.enabled;
        }
    }
}
