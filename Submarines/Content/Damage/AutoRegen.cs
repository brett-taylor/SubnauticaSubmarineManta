using UnityEngine;

namespace Submarines.Content.Damage
{
    /**
     * Class that automatically regens the submarine all the time.
     * There are no conditions that will stop the submarine from regening apart from death.
     * TO:DO stop regen when dead.
     */
    public class AutoRegen : MonoBehaviour
    {
        public float RegenPerSecond { get; set; } = 2f;
        public LiveMixin LiveMixin { get; set; }

        public virtual bool ShouldRegen()
        {
            return true;
        }

        public virtual void Start()
        {
            if (LiveMixin == null)
            {
                Utilities.Log.Error("AutoRegenConditional has no reference to a LiveMixin. Destroying...");
                Destroy(this);
                return;
            }

            InvokeRepeating("Regen", 1f, 1f);
        }

        public void OnDestroy()
        {
            CancelInvoke("Regen");
        }

        public void Regen()
        {
            if (ShouldRegen() == true)
            {
                LiveMixin.AddHealth(RegenPerSecond);
            }
        }
    }
}
