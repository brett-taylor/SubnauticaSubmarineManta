using Harmony;

namespace Submarines.Patchers
{
   /* [HarmonyPatch(typeof(PilotingChair))]
    [HarmonyPatch("ReleaseBy")]
    public class PilotingChairTryEjectPatcher
    {
        public static bool Prefix(PilotingChair __instance, Player player)
        {
            Utilities.Log.Print("Pilotingchair::ReleaseBy called");
            if (__instance is PointsOfInterest.SteeringConsole)
            {
                PointsOfInterest.SteeringConsole steeringConsole = (PointsOfInterest.SteeringConsole) __instance;
                if (steeringConsole != null)
                {
                    steeringConsole.ReleaseBy(player);
                    return false;
                }
            }

            return true;
        }
    }*/
}
