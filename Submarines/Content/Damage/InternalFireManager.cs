using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Submarines.Content.Damage
{
    /**
     * Handles applying internal fire in the submarine.
     */
    public class InternalFireManager : MonoBehaviour
    {
        public SubRoot Submarine { get; set; }
        public LiveMixin SubmarineLiveMixin { get; set; }
        public Transform FirePoints { get; set; } // All children in this object will be turned into a potential fire point
        public List<GameObject> FirePrefabs { get; set; }
        public float DamageDonePerFirePerSecond { get; set; } = 5;
        public int ChancePerDamageTakenToSpawnFire { get; set; } = 5; // 1 in X chance of spawning a fire.
        public int CurrentFireCount { get; private set; } = 0;

        private List<Vector3> freeFirePoints;

        public void Start()
        {
            if (SubmarineLiveMixin == null)
            {
                Utilities.Log.Error("InternalFireManager has not had the submarine's LiveMixin assigned. Destroying...");
                Destroy(this);
                return;
            }

            if (Submarine == null)
            {
                Utilities.Log.Error("InternalFireManager has no Submarine assigned. Destroying...");
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

            freeFirePoints = new List<Vector3>();
            foreach (Transform potentialFirePoint in FirePoints)
            {
                freeFirePoints.Add(potentialFirePoint.position);
            }
        }

        public void OnTakeDamage(DamageInfo damageInfo)
        {
            // We ignore fire damage when we do this as we dont wana fires to spawn more fires.
            if (damageInfo.damage > 0 && damageInfo.type != DamageType.Fire)
            {
                int randomNumber = Random.Range(1, ChancePerDamageTakenToSpawnFire);
                if (randomNumber == 1)
                {
                    CreateFireAtEmptySpot();
                }
            }
        }

        public void CreateFireAtEmptySpot()
        {
            if (freeFirePoints.Count == 0)
            {
                Utilities.Log.Print("No freeFirePoint found");
                return;
            }

            int randomPrefab = Random.Range(0, FirePrefabs.Count - 1);
            int randomPosition = Random.Range(0, freeFirePoints.Count - 1);

            GameObject fireGO = Instantiate(FirePrefabs[randomPrefab]);
            fireGO.transform.SetParent(transform);
            fireGO.transform.position = freeFirePoints[randomPosition];
            Fire fire = fireGO.GetComponentInChildren<Fire>();
            fire.fireSubRoot = Submarine;
            InternalFirePoint firePoint = fire.gameObject.AddComponent<InternalFirePoint>();
            firePoint.Fire = fire;
            firePoint.FireManager = this;
            freeFirePoints.RemoveAt(randomPosition);

            int oldFireCount = CurrentFireCount;
            CurrentFireCount += 1;
            SendMessage("InternalFirePointCreated", firePoint, SendMessageOptions.DontRequireReceiver);
            if (oldFireCount == 0)
            {
                InvokeRepeating("DamageSubmarinePerSecond", 1f, 1f);
            }
        }

        public void FireExtinguished(Vector3 position)
        {
            freeFirePoints.Add(position);
            CurrentFireCount -= 1;
            SendMessage("InternalFirePointExtinguished", null, SendMessageOptions.DontRequireReceiver);

            if (CurrentFireCount == 0)
            {
                CancelInvoke("DamageSubmarinePerSecond");
            }
        }

        private void DamageSubmarinePerSecond()
        {
            SubmarineLiveMixin.TakeDamage(CurrentFireCount * DamageDonePerFirePerSecond, type: DamageType.Fire);
        }
    }
}