using HarmonyLib;
using System;

namespace Submarines.Content.Beacon
{
    [HarmonyPatch(typeof(SpriteManager))]
    [HarmonyPatch("Get")]
    [HarmonyPatch(new Type[] { typeof(SpriteManager.Group), typeof(string) })]
    public class SpriteManagerGetPatch
    {
        private static bool Prefix(SpriteManager.Group group, string name, ref Atlas.Sprite __result)
        {
            if (group == SpriteManager.Group.Pings)
            {
                if (CustomBeaconManager.DoesCustomPingExist(name))
                {
                    __result = CustomBeaconManager.GetSprite(name);
                    return false;
                }
            }

            return true;
        }
    }
}
