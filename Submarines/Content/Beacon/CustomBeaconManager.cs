using Submarines.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace Submarines.Content.Beacon
{
    /**
     * Handles adding a custom ping to the submarine.
     */
    public static class CustomBeaconManager
    {
        private static readonly int CUSTOM_BEACON_MIN_DIST = 60; // Yes these are correct.
        private static readonly int CUSTOM_BEACON_MAX_DIST = 50;
        private static readonly Dictionary<PingType, Atlas.Sprite> cachedSprites = new Dictionary<PingType, Atlas.Sprite>();
        private static readonly Dictionary<string, PingType> nameToType = new Dictionary<string, PingType>();
        private static uGUI_Pings pingUI = null;

        public static void Initialize()
        {
            if (pingUI != null)
                return;

            pingUI = Object.FindObjectOfType<uGUI_Pings>();
        }

        public static PingInstance AddNewBeacon(GameObject gameObject, PingType pingType, string beaconName)
        {
            return AddNewBeacon(gameObject, pingType, beaconName, CUSTOM_BEACON_MIN_DIST, CUSTOM_BEACON_MAX_DIST);
        }

        public static PingInstance AddNewBeacon(GameObject gameObject, PingType pingType, string beaconName, int minDist, int maxDist)
        {
            Initialize();
            if (cachedSprites.ContainsKey(pingType) == false)
            {
                Log.Error($"CustomBeaconManager::AddNewBeacon attempted to add a beacon with a PingType ({pingType}) that was not registered with CustomBeaconManager::RegisterNewPingType", false);
                return null;
            }

            var pingInstance = gameObject.EnsureComponent<PingInstance>();
            pingInstance.pingType = pingType;
            pingInstance.minDist = minDist;
            pingInstance.maxDist = maxDist;
            pingInstance.origin = gameObject.transform;
            pingInstance.SetLabel(beaconName);

            if (pingUI != null)
                pingUI.pings[pingInstance.GetInstanceID()].SetIcon(cachedSprites[pingType]);

            return pingInstance;
        }

        /**
         * Expects the tech type for your custom spawnable. This gives us a unique number that we can use.
         */
        public static PingType RegisterNewPingType(TechType type, string pingName, Sprite sprite)
        {
            var pingType = GetPingType(type);
            if (cachedSprites.ContainsKey(pingType) == false)
            {
                if (nameToType.ContainsKey(pingName))
                {
                    Log.Error($"CustomBeaconManager::RegisterNewPingType Attempted to register a new ping with name {pingName} but that is already taken.");
                    return pingType;
                }

                cachedSprites.Add(pingType, new Atlas.Sprite(sprite));
                nameToType.Add(pingName, pingType);
                if (PingManager.sCachedPingTypeStrings.valueToString.ContainsKey(pingType) == false)
                    PingManager.sCachedPingTypeStrings.valueToString.Add(pingType, pingName);

                if (PingManager.sCachedPingTypeTranslationStrings.valueToString.ContainsKey(pingType) == false)
                    PingManager.sCachedPingTypeTranslationStrings.valueToString.Add(pingType, pingName);
            }

            Log.Print($"CustomBeaconManager::RegisterNewPingType ({type}, {pingName}) -> {pingType}", false);
            return pingType;
        }

        /**
         * Expects the tech type for your custom spawnable. This gives us a unique number that we can use.
         */
        public static PingType GetPingType(TechType type)
        {
            var number = (int) type;
            var result = (PingType) number;

            Log.Print($"CustomBeaconManager::GetPingType turned {type} -> ({number}, {result})", false);
            return result;
        }

        public static bool DoesCustomPingExist(string name)
        {
            return nameToType.ContainsKey(name);
        }

        public static Atlas.Sprite GetSprite(string name)
        {
            return cachedSprites.GetOrDefault(nameToType[name], SpriteManager.defaultSprite);
        }
    }
}
