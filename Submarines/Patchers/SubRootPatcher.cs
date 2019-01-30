using Harmony;

namespace Submarines.Patchers
{
    /**
     * Our "Submarine" is stored as a "SubRoot" in the player class and so "SubRoot::OnPlayerEntered" will be called even when our instance is actually a Submarine
     * We check this and dont alllow SubRoot::OnPlayerEnter to run if our SubRoot instance is actually a "Submarine" instance
     */
    [HarmonyPatch(typeof(SubRoot))]
    [HarmonyPatch("OnPlayerEntered")]
    public class SubRootOnPlayerEnteredPatcher
    {
        public static bool Prefix(SubRoot __instance, Player player)
        {
            if (__instance is Submarine)
            {
                Submarine submarine = (Submarine)__instance;
                if (submarine != null)
                {
                    submarine.OnPlayerEntered(player);
                    return false;
                }
            }

            return true;
        }
    }

    /**
    * Our "Submarine" is stored as a "SubRoot" in the player class and so "SubRoot::OnPlayerExited" will be called even when our instance is actually a Submarine
    * We check this and dont alllow "SubRoot::OnPlayerExited" to run if our SubRoot instance is actually a "Submarine" instance
    */
    [HarmonyPatch(typeof(SubRoot))]
    [HarmonyPatch("OnPlayerExited")]
    public class SubRootOnPlayerExitedPatcher
    {
        public static bool Prefix(SubRoot __instance, Player player)
        {
            if (__instance is Submarine)
            {
                Submarine submarine = (Submarine)__instance;
                if (submarine != null)
                {
                    submarine.OnPlayerExited(player);
                    return false;
                }
            }

            return true;
        }
    }
}