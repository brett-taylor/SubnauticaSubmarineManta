using HarmonyLib;
using PlayerAnimations.Test;
using System.Reflection;

namespace PlayerAnimations
{
    public class EntryPoint
    {
        public static void Entry()
        {
            Harmony harmony = new Harmony("taylor.brett.PlayerAnimations.mod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            AssetLoader.LoadAnimations();
        }
    }
}
