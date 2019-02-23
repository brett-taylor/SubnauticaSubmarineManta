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
        public LiveMixin SubmarineLiveMixin { get; set; }
        public LiveMixinData LiveMixinDataForExternalDamagePoints { get; set; }
        public float EndOfHealthBuffer { get; set; } = 50; // The EndOfHealthBuffer is added to the MaxHealth. This makes that all damage points are shown before the sub's health hits 0.
        public float StartOfHealthBuffer { get; set; } = 50; // The StartOfHealthBuffer is added to the subs current health. This means the sub can take some damage before a hole appears.

        private List<ExternalDamagePoint> damagePoints;
        private List<ExternalDamagePoint> freeDamagePoints;

        public void Start()
        {
            if (SubmarineLiveMixin == null)
            {
                Utilities.Log.Error("ExternalDamageManager has not had the submarine's LiveMixin assigned. Destroying...");
                Destroy(this);
                return;
            }

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
            SubmarineLiveMixin.AddHealth(GetUsedDamagePointsCount() == 0 ? SubmarineLiveMixin.maxHealth : GetHealthPerDamagePoint());
            SendMessage("ExternalDamagePointRepaired", damagePoint, SendMessageOptions.DontRequireReceiver);
        }

        public void OnTakeDamage(DamageInfo damageInfo)
        {
            Utilities.Log.Print("DamageType: " + damageInfo.type);
            if (damageInfo.damage > 0 && damageInfo.type == DamageType.Normal)
            {
                int numberToCreate = GetNumberOfDamagePointsThatShouldBeShowing() - GetUsedDamagePointsCount();
                EnableMultipleDamagePoints(numberToCreate);
            }
        }

        public void EnableMultipleDamagePoints(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                if (freeDamagePoints.Count == 0)
                {
                    return;
                }

                int randomDamagePoint = Random.Range(0, freeDamagePoints.Count - 1);
                ExternalDamagePoint damagePoint = freeDamagePoints[randomDamagePoint];
                freeDamagePoints.Remove(damagePoint);
                damagePoint.NeedsRepairing();
                SendMessage("ExternalDamagePointCreated", damagePoint, SendMessageOptions.DontRequireReceiver);
            }
        }

        public virtual float GetHealthPerDamagePoint()
        {
            return Mathf.Ceil((SubmarineLiveMixin.maxHealth - EndOfHealthBuffer) / damagePoints.Count);
        }

        public virtual int GetNumberOfDamagePointsThatShouldBeShowing()
        {
            float lostHealth = (SubmarineLiveMixin.maxHealth + EndOfHealthBuffer) - (SubmarineLiveMixin.health + StartOfHealthBuffer);
            return Mathf.FloorToInt(lostHealth / GetHealthPerDamagePoint());
        }

        public int GetUnusedDamagePointsCount()
        {
            return freeDamagePoints.Count;
        }

        public int GetUsedDamagePointsCount()
        {
            return damagePoints.Count - freeDamagePoints.Count;
        }
    }
}
