using Harmony;
using Submarines.Creatures;

namespace Submarines.Patchers.Creatures
{
    /**
     * Following methods patch into methods to add the components to creature necessary to make them aggressive to custom submarines.
     */
    [HarmonyPatch(typeof(MeleeAttack))]
    [HarmonyPatch("OnEnable")]
    public class MeleeAttackOnEnablePatcher
    {
        public static bool Prefix(MeleeAttack __instance)
        {
            if (__instance is ReaperMeleeAttack)
            {
                if (__instance.gameObject.GetComponent<AttackMannedSubmarineWithinDistance>() == null)
                {
                    __instance.gameObject.AddComponent<AttackMannedSubmarineWithinDistance>();
                    __instance.creature.ScanCreatureActions();
                }
            }

            return true;
        }
    }
}
