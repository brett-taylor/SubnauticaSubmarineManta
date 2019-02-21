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
        public List<GameObject> FirePrefabs { get; set; }
        public List<Fire> ActiveFires { get; private set; }

        private SubRoot submarine;
        private List<Transform> firePoints;
        private List<Transform> freeFirePoints;

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

            submarine = gameObject.GetComponent<SubRoot>();
            ActiveFires = new List<Fire>();
            firePoints = new List<Transform>();
            freeFirePoints = new List<Transform>();
            foreach (Transform potentialFirePoint in FirePoints)
            {
                firePoints.Add(potentialFirePoint);
                freeFirePoints.Add(potentialFirePoint);
            }
        }

        public void OnTakeDamage(DamageInfo damageInfo)
        {
            CreateFirePoint();
        }

        public void CreateFirePoint()
        {
            if (freeFirePoints.Count == 0)
            {
                return;
            }

            int randomPosition = Random.Range(0, freeFirePoints.Count - 1);
            int randomFirePrefab = Random.Range(0, FirePrefabs.Count - 1);
            GameObject fireGO = Utils.SpawnZeroedAt(FirePrefabs[randomFirePrefab], freeFirePoints[randomPosition], false);
            Fire fire = fireGO.GetComponentInChildren<Fire>();
            fire.fireSubRoot = submarine;
        }

        public void FirePointExtinguished(Fire fire)
        {

        }
    }
}