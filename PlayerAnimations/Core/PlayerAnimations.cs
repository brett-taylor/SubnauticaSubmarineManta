using UnityEngine;

namespace PlayerAnimations.Core
{
    public class PlayerAnimations
    {
        private static Animator playerAnimator;
        private static RuntimeAnimatorController originalAnimationController;

        public static void Initialise(Player player)
        {
            playerAnimator = player.gameObject.GetComponentInChildren<Animator>();
            originalAnimationController = playerAnimator.runtimeAnimatorController;
        }

        public static void PlayAnimation(string animationclipName)
        {
            playerAnimator.Play(animationclipName);
        }

        public static void PlayAnimation(RuntimeAnimatorController runtimeAnimatorController)
        {
            foreach (var t in runtimeAnimatorController.animationClips)
            {
                Utilities.Log.Print(t.name);
            }

        }
    }
}
