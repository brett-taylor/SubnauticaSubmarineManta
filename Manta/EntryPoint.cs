using System;
using HarmonyLib;
using Manta.Core;
using Manta.Utilities;
using System.Reflection;
using QModManager.API.ModLoading;
using Submarines.DefaultCyclopsContent;

namespace Manta
{
    /**
    * Entry point into our Manta submarine
    */
    [QModCore]
    public class EntryPoint
    {
        public static readonly string MOD_FOLDER_LOCATION = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        
        [QModPatch]
        public static void Entry()
        {
            LoadSubmarinesFramework();
            LoadMantaFramework();
        }

        private static void LoadSubmarinesFramework()
        {
            Submarines.EntryPoint.MOD_FOLDER_LOCATION = MOD_FOLDER_LOCATION;
            Submarines.EntryPoint.InitialiseFramework();
        }

        private static void LoadMantaFramework()
        {
            var harmonyInstance = new Harmony("taylor.brett.TheMantaMod.mod");
            harmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

            Assets.Instance = new Assets();

            var manta = new MantaMod();
            manta.Patch();
        }
    }
}