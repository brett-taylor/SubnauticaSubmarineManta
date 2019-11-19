using Submarines.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using UnityEngine;

namespace Submarines.Assets
{
    public abstract class AssetFile
    {
        private static readonly string ASSET_FOLDER_NAME = "Assets";
        private static readonly List<string> ASSET_FILES_CREATED = new List<string>();

        protected abstract string AssetBundleName { get; }
        protected abstract string AssetBundleLocation { get; }
        protected abstract bool InAssetsFolder { get; }
        protected abstract bool ShouldLogContents { get; }
        protected abstract List<Type> TypesToLoad { get; }

        private AssetBundle bundle;
        private readonly Dictionary<string, UnityEngine.Object> assets;
        private readonly string finalLocation;

        public AssetFile()
        {
            finalLocation = GetAssetBundleLocation(Assembly.GetCallingAssembly().Location);
            if (DoesAssetBundleExist(finalLocation) == false)
            {
                Log.Error($"{GetType().FullName} Attempted to load a non-existent asset bundle at {finalLocation}");
                return;
            }

            if (ASSET_FILES_CREATED.Contains(finalLocation))
            {
                Log.Error($"Tried to create already loaded asset bundle {finalLocation}");
                return;
            }

            ASSET_FILES_CREATED.Add(finalLocation);
            assets = new Dictionary<string, UnityEngine.Object>();
            LoadAssetBundle(finalLocation);
            if (ShouldLogContents)
                LogContents();
        }

        private bool DoesAssetBundleExist(string assetBundleLocation)
        {
            return File.Exists(assetBundleLocation);
        }

        private void LoadAssetBundle(string assetBundleLocation)
        {
            HashSet<int> instanceIDsLoaded = new HashSet<int>();

            try
            {
                bundle = AssetBundle.LoadFromFile(assetBundleLocation);
                foreach(Type typeToLoad in TypesToLoad)
                {
                    if (typeToLoad.IsSubclassOf(typeof(UnityEngine.Object)))
                    {
                        foreach (UnityEngine.Object asset in bundle.LoadAllAssets(typeToLoad))
                        {
                            if (instanceIDsLoaded.Contains(asset.GetInstanceID()) == false)
                            {
                                assets.Add(asset.name, asset);
                                instanceIDsLoaded.Add(asset.GetInstanceID());
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error($"Failed to load {AssetBundleName} : {e.ToString()}");
            }

            instanceIDsLoaded = null;
        }

        private string GetAssetBundleLocation(string dllLocation)
        {
            if (InAssetsFolder)
            {
                string assemblyDirectory = Path.GetDirectoryName(dllLocation);
                return Path.Combine(assemblyDirectory, Path.Combine(ASSET_FOLDER_NAME, AssetBundleName));
            }
            else
            {
                return Path.Combine(AssetBundleLocation, AssetBundleName);
            }
        }

        public T GetAsset<T>(string name) where T : UnityEngine.Object
        {
            if (assets.ContainsKey(name))
            {
                UnityEngine.Object potentialAsset = assets[name];
                if (potentialAsset is T)
                {
                    return (T) potentialAsset;
                }
                else
                {
                    Log.Error($"Asset Bundle ({finalLocation}) found item: {name} but type was ({potentialAsset.GetType()}) and requested type was ({typeof(T)})");
                    return null;
                } 
            }
            else
            {
                Log.Error($"Asset Bundle ({finalLocation}) attempted to load non existent item: {name}");
                return null;
            }
        }

        public List<UnityEngine.Object> GetAllAssets()
        {
            return assets.Values.ToList();
        }

        private void LogContents()
        {
            Log.Print($"Printing asset bundle ({finalLocation}) contents to log file");
            foreach (string assetName in assets.Keys)
                Log.Print($"Asset Loaded: {assetName}");
        }
    }
}