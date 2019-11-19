using UnityEngine;

namespace PlayerAnimations.Test
{
    public static class DebugMenu
    {
        private static string animationClip = "Laser_cutter_first_use";

        public static void DrawPlayerAnimationsMenu()
        {
            if (GUILayout.Button("Toggle Third Person"))
            {
                ThirdPersonController.Toggle();
            }

            if (Assets.AnimationControllers == null)
            {
                GUILayout.Label("Animations not loaded");
                return;
            }

            GUILayout.Label("Found " + Assets.AnimationControllers.Count + " RuntimeAnimatorController");

            foreach(RuntimeAnimatorController controller in Assets.AnimationControllers)
            {
                foreach(AnimationClip ac in controller.animationClips)
                {
                    if (GUILayout.Button("Play: " + ac.name))
                    {
                        Animator animator = Player.main.gameObject.GetComponentInChildren<Animator>();
                        animator.runtimeAnimatorController = controller;
                        //animator.Play("test_animation", 0);
                        Utilities.Log.Print("Length GetCurrentAnimatorClipInfo: " + animator.GetCurrentAnimatorClipInfo(0).Length);
                        Utilities.Log.Print("Length GetNextAnimatorClipInfo: " + animator.GetNextAnimatorClipInfo(0).Length);

                        foreach (var ac2 in animator.GetCurrentAnimatorClipInfo(0))
                        {
                            Utilities.Log.Print("1: " + ac2.clip.name);
                            Utilities.Log.Print("2: " + ac2.clip.length);
                        }
                    }
                }
            }

            GUILayout.BeginHorizontal();
            animationClip = GUILayout.TextField(animationClip);
            if (GUILayout.Button("Play Animation Clip"))
            {
                Core.PlayerAnimations.PlayAnimation(animationClip);
            }
            GUILayout.EndHorizontal();
        }
    }
}
