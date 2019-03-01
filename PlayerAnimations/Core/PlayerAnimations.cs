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
            player.gameObject.AddComponent<AnimatorDebugger>();
        }

        public static void PlayAnimation(string animationclipName, int layer)
        {
            playerAnimator.Play(animationclipName, layer);
        }

        public static void PlayAnimation(RuntimeAnimatorController runtimeAnimatorController)
        {
            AnimationClip animationClip = runtimeAnimatorController.animationClips[0];
            Utilities.Log.Print("Found animation clip: " + animationClip);
            playerAnimator.runtimeAnimatorController = runtimeAnimatorController;
            Utilities.Log.Print("Currently on: " + playerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
            Utilities.Log.Print("length: " + playerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
            playerAnimator.Play(playerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name, 0, 1f);
        }
    }
}
