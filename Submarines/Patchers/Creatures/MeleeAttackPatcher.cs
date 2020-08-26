using HarmonyLib;
using UnityEngine;

namespace Submarines.Patchers.Creatures
{
    /**
     * Patch into MeleeAttack::CanBite to add our custom submarines into what can be biten.
     */
    [HarmonyPatch(typeof(ReaperMeleeAttack))]
    [HarmonyPatch("CanBite")]
    public class MeleeAttackCanBitePatcher
    {
        public static bool Prefix(MeleeAttack __instance, GameObject target, ref bool __result)
        {
            if (target.GetComponent<Content.Submarine>() != null)
            {
                __result = true;
                return false;
            }

            return true;
        }
    }
}
