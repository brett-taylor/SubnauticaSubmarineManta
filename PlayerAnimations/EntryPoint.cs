using Harmony;
using PlayerAnimations.Test;
using System.Reflection;

namespace PlayerAnimations
{
    public class EntryPoint
    {
        public static void Entry()
        {
            HarmonyInstance harmony = HarmonyInstance.Create("taylor.brett.PlayerAnimations.mod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            AssetLoader.LoadAnimations();
        }
    }
}
