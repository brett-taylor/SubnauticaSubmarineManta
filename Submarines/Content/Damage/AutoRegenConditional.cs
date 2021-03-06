﻿using UnityEngine;

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
            base.Start();

            if (InternalFireManager != null)
                noFires = (InternalFireManager.CurrentFireCount == 0);
            else
                noFires = true;

            if (ExternalDamageManager != null)
                noExternalDamagePoints = (ExternalDamageManager.GetUsedDamagePointsCount() == 0);
            else
                noExternalDamagePoints = true;
        }

        public override bool ShouldRegen()
        {
            return noFires && noExternalDamagePoints;
        }

        public void InternalFirePointCreated()
        {
            noFires = (InternalFireManager.CurrentFireCount == 0);
        }

        public void InternalFirePointExtinguished()
        {
            noFires = (InternalFireManager.CurrentFireCount == 0);
        }

        public void ExternalDamagePointCreated()
        {
            noExternalDamagePoints = (ExternalDamageManager.GetUsedDamagePointsCount() == 0);
        }

        public void ExternalDamagePointRepaired()
        {
            noExternalDamagePoints = (ExternalDamageManager.GetUsedDamagePointsCount() == 0);
        }
    }
}
