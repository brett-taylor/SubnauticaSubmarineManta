using Harmony;

namespace PlayerAnimations.Core
{
    [HarmonyPatch(typeof(Player))]
    [HarmonyPatch("Awake")]
    public class PlayerAwakePatcher
    {
        public static void Postfix()
        {
            ThirdPersonController.Initialise();
            PlayerAnimations.Initialise();
        }
    }
}
