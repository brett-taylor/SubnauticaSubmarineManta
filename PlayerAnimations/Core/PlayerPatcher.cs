using Harmony;

namespace PlayerAnimations.Core
{
    [HarmonyPatch(typeof(Player))]
    [HarmonyPatch("Awake")]
    public class PlayerAwakePatcher
    {
        public static void Postfix(Player __instance)
        {
            ThirdPersonController.Initialise();
            PlayerAnimations.Initialise(__instance);
        }
    }
}
