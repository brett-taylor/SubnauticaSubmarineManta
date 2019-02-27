using UnityEngine;
using System.Collections.Generic;
using PlayerAnimations.Utilities;

namespace PlayerAnimations.Test
{
    public class AssetLoader
    {
        private static readonly string ASSET_BUNDLE_LOCATION = "./QMods/TheMantaMod/Assets/customplayeranimations";
        public static List<RuntimeAnimatorController> AnimationControllers;

        public static void LoadAnimations()
        {
            AssetBundle ab = AssetBundle.LoadFromFile(ASSET_BUNDLE_LOCATION);
            if (ab == null)
            {
                Log.Error("Custom Player Animations asset bundle not found.");
            }

            AnimationControllers = new List<RuntimeAnimatorController>(ab.LoadAllAssets<RuntimeAnimatorController>());
        }
    }
}
