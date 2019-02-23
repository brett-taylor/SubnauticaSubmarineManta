using UnityEngine;

namespace Submarines.Content.Damage
{
    /**
     * Class that automatically regens the submarine if certain conditions are met.
     * These conditions are: no fires, no external damage points and no leaks.
     * TO:DO conditional leaks
     */
    public class AutoRegenConditional : AutoRegen
    {
        public InternalFireManager InternalFireManager {get; set;}
        public ExternalDamageManager ExternalDamageManager { get; set; }

        private bool noFires = true;
        private bool noExternalDamagePoints = true;

        public override void Start()
        {
            if (InternalFireManager == null)
            {
                Utilities.Log.Error("AutoRegenConditional has no reference to a InternalFireManager. Destroying...");
                Destroy(this);
                return;
            }

            if (ExternalDamageManager == null)
            {
                Utilities.Log.Error("AutoRegenConditional has no reference to a ExternalDamageManager. Destroying...");
                Destroy(this);
                return;
            }

            base.Start();

            noFires = (InternalFireManager.CurrentFireCount == 0);
            noExternalDamagePoints = (ExternalDamageManager.GetUsedDamagePointsCount() == 0);
        }

        public override bool ShouldRegen()
        {
            return noFires && noExternalDamagePoints;
        }

        public void InternalFirePointCreated()
        {
            noFires = (InternalFireManager.CurrentFireCount == 0);
            Utilities.Log.Print("Test 1: " + noFires);
        }

        public void InternalFirePointExtinguished()
        {
            noFires = (InternalFireManager.CurrentFireCount == 0);
            Utilities.Log.Print("Test 2: " + noFires);
        }

        public void ExternalDamagePointCreated()
        {
            noExternalDamagePoints = (ExternalDamageManager.GetUsedDamagePointsCount() == 0);
            Utilities.Log.Print("Test 3: " + noExternalDamagePoints);
        }

        public void ExternalDamagePointRepaired()
        {
            noExternalDamagePoints = (ExternalDamageManager.GetUsedDamagePointsCount() == 0);
            Utilities.Log.Print("Test 4: " + noExternalDamagePoints);
        }
    }
}
