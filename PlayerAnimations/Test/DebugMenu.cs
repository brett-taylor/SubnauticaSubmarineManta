using UnityEngine;

namespace PlayerAnimations.Test
{
    public static class DebugMenu
    {
        public static void DrawPlayerAnimationsMenu()
        {
            if (GUILayout.Button("Toggle Third Person"))
            {
                ThirdPersonController.Toggle();
            }

            if (AssetLoader.AnimationControllers is null)
            {
                GUILayout.Label("Animations not loaded");
                return;
            }

            GUILayout.Label("Found " + AssetLoader.AnimationControllers.Count + " RuntimeAnimatorController");

            foreach(RuntimeAnimatorController controller in AssetLoader.AnimationControllers)
            {
                if (GUILayout.Button("Play: " + controller.name))
                {
                    Core.PlayerAnimations.PlayAnimation(controller);
                }
            }
        }
    }
}
