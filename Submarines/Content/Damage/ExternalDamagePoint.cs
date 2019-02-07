using UnityEngine;

namespace Submarines.Content.Damage
{
    /**
     * A external damage point.
     */
    [RequireComponent(typeof(WeldablePoint))]
    public class ExternalDamagePoint : HandTarget
    {
        public ExternalDamageManager ExternalDamageManager { get; set; }
        private LiveMixin liveMixin;
        private ParticleSystem ps;

        public override void Awake()
        {
            base.Awake();
            liveMixin = gameObject.AddComponent<LiveMixin>();
            liveMixin.data = DefaultCyclopsContent.CyclopsExternalDamagePointLiveMixinData.Get();
        }

        public void Start()
        {
            if (ExternalDamageManager == null)
            {
                Utilities.Log.Error("ExternalDamageManager not set for ExternalDamagePoint. Destroying");
                Destroy(gameObject);
                return;
            }

            if (ExternalDamageManager.LiveMixinDataForExternalDamagePoints != null)
            {
                liveMixin.data = ExternalDamageManager.LiveMixinDataForExternalDamagePoints;
            }

            liveMixin.health = 1;
            ps = gameObject.GetComponent<ParticleSystem>();
        }

        public void OnRepair()
        {
            Repaired();
        }

        public void NeedsRepairing()
        {
            Utilities.Log.Print("Callleddd NeedsRepairing");
            liveMixin.health = 1;
            ps?.Play();
            gameObject.SetActive(true);
        }

        public void Repaired()
        {
            Utilities.Log.Print("Callleddd Repaired");
            liveMixin.health = 1;
            ps?.Stop();
            ExternalDamageManager?.DamagePointRepaired(this);
            gameObject.SetActive(false);
        }
    }
}
