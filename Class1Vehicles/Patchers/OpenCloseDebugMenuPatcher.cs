using UnityEngine;
using Harmony;

namespace Class1Vehicles.Patchers
{
    /**
     * Patches into Player::Update() so we can track if our debug key has been pressed.
     */
    [HarmonyPatch(typeof(Player))]
    [HarmonyPatch("Update")]
    public class OpenCloseDebugMenuPatcher
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            if (Input.GetKey(KeyCode.CapsLock) && Input.GetKeyDown(KeyCode.W))
            {
                Utilities.DebugMenu.main.Toggle();
            }
        }
    }
}
