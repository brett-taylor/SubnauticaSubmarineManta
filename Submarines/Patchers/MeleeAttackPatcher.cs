using Harmony;
using UnityEngine;

namespace Submarines.Patchers
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
                Utilities.Log.Print("ReaperMeleeAttack Submarine attack: " + target.name + " from: " + __instance.gameObject.name);
                __result = true;
                return false;
            }

            return true;
        }
    }
}
