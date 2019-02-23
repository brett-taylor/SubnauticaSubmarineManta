using System.Collections.Generic;
using UnityEngine;

namespace Submarines.Content.Damage
{
    /**
     * Handles applying internal fire in the submarine.
     */
    public class InternalFireManager : MonoBehaviour
    {
        public LiveMixin SubmarineLiveMixin { get; set; }
        public Transform FirePoints { get; set; }
        public List<GameObject> FirePrefabs { get; set; } // All children in this object will be turned into a potential fire point

        public void Start()
        {
            if (SubmarineLiveMixin == null)
            {
                Utilities.Log.Error("InternalFireManager has not had the submarine's LiveMixin assigned. Destroying...");
                Destroy(this);
                return;
            }

            if (FirePrefabs == null || FirePrefabs.Count == 0)
            {
                Utilities.Log.Error("InternalFireManager has no fire gameobject prefabs assigned. Destroying...");
                Destroy(this);
                return;
            }

            if (FirePoints == null || FirePoints.childCount == 0)
            {
                Utilities.Log.Error("InternalFireManager has no fire points object assigned. Destroying...");
                Destroy(this);
                return;
            }
        }

        public void OnTakeDamage(DamageInfo damageInfo)
        {
            if (damageInfo.damage > 0)
            {

            }
        }
    }
}