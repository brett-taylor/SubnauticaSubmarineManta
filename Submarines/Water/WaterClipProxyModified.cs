using UnityEngine;

namespace Submarines.Water
{
    /**
     * Adds the Subnautica WaterClipProxy but does not rely on a text field.
     */
    public class WaterClipProxyModified : MonoBehaviour
    {
        public static readonly string DISTANCE_FIELD_TEMPLATE_FILE_NAME = "WaterClipProxy.txt";
        private static Material ClipMaterialCache;

        public Vector3 DistanceFieldMin { get; set; } = new Vector3(-5.7f, -10.4f, -17.0f);
        public Vector3 DistanceFieldMax { get; set; } = new Vector3(5.7f, 5.5f, 29.1f);
        public WaterClipProxy.Shape Shape { get; set; } = WaterClipProxy.Shape.DistanceField;
        public bool Immovable { get; set; } = false;

        private WaterClipProxy waterClipProxy;

        public void Initialise()
        {
            gameObject.layer += LayerMask.NameToLayer("BaseClipProxy");

            TextAsset textAsset = Assets.SubmarineAssetLoader.ASSET_BUNDLE.LoadAsset<TextAsset>(DISTANCE_FIELD_TEMPLATE_FILE_NAME);
            if (textAsset == null)
            {
                Utilities.Log.Print("DistanceField text file not found in the submarine asset bundle: ");
                return;
            }
            
            waterClipProxy = gameObject.AddComponent<WaterClipProxy>();
            waterClipProxy.shape = Shape;
            waterClipProxy.immovable = Immovable;
            waterClipProxy.distanceField = textAsset;

            if (ClipMaterialCache == null)
            {
                Material[] mats = Resources.FindObjectsOfTypeAll<Material>();
                foreach (Material mat in mats)
                {
                    if (mat.name == "WaterClip")
                    {
                        ClipMaterialCache = mat;
                    }
                }
            }
            waterClipProxy.clipMaterial = ClipMaterialCache;
        }
    }
}
