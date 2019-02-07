using System.Collections.Generic;
using UnityEngine;

namespace Submarines.Content.Damage
{
    /**
     * Handles applying external damage to the submarine.
     * Set "DamagePoints" to an object on the submarine with children gameobjects.
     * Each child gameobject will become a damage point.
     */
    public class ExternalDamageManager : MonoBehaviour
    {
        public Transform DamagePoints { get; set; }
        public List<GameObject> DamagePointGameObjects { get; set; }
        public List<GameObject> DamagePointParticleEffects { get; set; }
        public LiveMixinData LiveMixinDataForExternalDamagePoints { get; set; }

        private List<ExternalDamagePoint> damagePoints;
        private List<ExternalDamagePoint> freeDamagePoints;

        public void Start()
        {
            if (DamagePoints == null || DamagePoints.childCount == 0)
            {
                Utilities.Log.Error("ExternalDamageManager has no damage points object assigned. Destroying...");
                Destroy(this);
                return;
            }

            if (DamagePointGameObjects == null || DamagePointGameObjects.Count == 0)
            {
                Utilities.Log.Error("ExternalDamageManager has no damage points prefab assigned. Destroying...");
                Destroy(this);
                return;
            }

            damagePoints = new List<ExternalDamagePoint>();
            freeDamagePoints = new List<ExternalDamagePoint>();
            foreach (Transform potentialDamagePoint in DamagePoints)
            {
                ExternalDamagePoint damagePoint = CreateDamagePoint(potentialDamagePoint);
                damagePoints.Add(damagePoint);
                freeDamagePoints.Add(damagePoint);
                damagePoint.gameObject.SetActive(false);
            }
        }

        private ExternalDamagePoint CreateDamagePoint(Transform position)
        {
            int randomPrefab = Random.Range(0, DamagePointGameObjects.Count - 1);
            int randomParticle = Random.Range(0, DamagePointParticleEffects.Count - 1);
            GameObject mainGO = Utils.SpawnZeroedAt(DamagePointGameObjects[randomPrefab], position, false);
            GameObject particles = Utils.SpawnZeroedAt(DamagePointParticleEffects[randomParticle], mainGO.transform, false);

            mainGO.AddComponent<BoxCollider>();
            ExternalDamagePoint damagePoint =  mainGO.AddComponent<ExternalDamagePoint>();
            damagePoint.ExternalDamageManager = this;
            return damagePoint;
        }

        public void DamagePointRepaired(ExternalDamagePoint damagePoint)
        {
            freeDamagePoints.Add(damagePoint);
            ErrorMessage.AddMessage("DamagePoint Repaired");
        }

        public void OnTakeDamage()
        {
            if (freeDamagePoints.Count == 0)
            {
                ErrorMessage.AddMessage("No free damage points");
                return;
            }

            int randomDamagePoint = Random.Range(0, freeDamagePoints.Count - 1);
            ExternalDamagePoint damagePoint = freeDamagePoints[randomDamagePoint];
            freeDamagePoints.Remove(damagePoint);
            damagePoint.NeedsRepairing();
        }
    }
}
