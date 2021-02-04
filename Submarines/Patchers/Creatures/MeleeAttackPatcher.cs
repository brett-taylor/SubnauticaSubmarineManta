using HarmonyLib;
using UnityEngine;

namespace Submarines.Patchers.Creatures
{
    /**
     * Patch into MeleeAttack::CanBite to add our custom submarines into what can be biten.
     */
    [HarmonyPatch(typeof(MeleeAttack))]
    [HarmonyPatch("CanBite")]
    public class MeleeAttackCanBitePatcher
    {
        public static bool Prefix(MeleeAttack __instance, GameObject target, ref bool __result)
        {
            // TODO: "__instance is ReaperMeleeAttack" was added without testing, originally it patched `ReaperMeleeAttack`. Test if this works. 
            if (__instance is ReaperMeleeAttack && target.GetComponent<Content.Submarine>() != null)
            {
                __result = true;
                return false;
            }

            return true;
        }
    }
}