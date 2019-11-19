using UnityEngine;
using System.Collections.Generic;
using Submarines.Assets;
using System;

namespace PlayerAnimations.Test
{
    internal class Assets : AssetFile
    {
        internal static Assets Instance { get; set; }
        protected override string AssetBundleName => "customplayeranimations";
        protected override string AssetBundleLocation => throw new NotImplementedException();
        protected override bool InAssetsFolder => true;
        protected override bool ShouldLogContents => false;
        protected override List<Type> TypesToLoad => new List<Type>() { typeof(RuntimeAnimatorController) };

        public static List<RuntimeAnimatorController> AnimationControllers { get; private set; }

        internal Assets() : base()
        {
            AnimationControllers = new List<RuntimeAnimatorController>();
            foreach(var asset in GetAllAssets())
            {
                if (asset is RuntimeAnimatorController)
                {
                    AnimationControllers.Add((RuntimeAnimatorController) asset);  
                }
            }
        }
    }
}
