using UnityEngine;

namespace PlayerAnimations.Core
{
    public static class PlayerAnimations
    {
        private static Animator playerAnimator;
        private static RuntimeAnimatorController originalAnimationController;

        public static void Initialise()
        {
            playerAnimator = Player.main.GetComponent<Animator>();
            originalAnimationController = playerAnimator.runtimeAnimatorController;
        }

        public static void PlayAnimation(RuntimeAnimatorController runtimeAnimatorController)
        {
            Utilities.Log.Print("Is human: " + Player.main.GetComponentInChildren<Animator>().isHuman);
        }
    }
}
