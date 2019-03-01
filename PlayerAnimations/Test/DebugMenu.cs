using UnityEngine;

namespace PlayerAnimations.Test
{
    public static class DebugMenu
    {
        private static string animationClip = "Laser_cutter_first_use";
        private static int animationClipLayer = 0;

        public static void DrawPlayerAnimationsMenu()
        {
            if (GUILayout.Button("Toggle Third Person"))
            {
                ThirdPersonController.Toggle();
            }

            if (AssetLoader.AnimationControllers == null)
            {
                Utilities.Log.Print("Called 2");
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

            GUILayout.BeginHorizontal();
            animationClip = GUILayout.TextField(animationClip);
            animationClipLayer = int.Parse(GUILayout.TextField(animationClipLayer + ""));
            if (GUILayout.Button("Play Animation Clip"))
            {
                Core.PlayerAnimations.PlayAnimation(animationClip, animationClipLayer);
            }
            GUILayout.EndHorizontal();
        }
    }
}
