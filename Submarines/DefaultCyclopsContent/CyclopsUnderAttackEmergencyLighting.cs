using Submarines.Content.Lighting;
using System.Collections;
using UnityEngine;

namespace Submarines.DefaultCyclopsContent
{
    /**
     * Makes the submarine enable flashing red lights when it is under attack.
     * Must set EmergencyLighting.LightsAffected
     */
    public class CyclopsUnderAttackEmergencyLighting : EmergencyLighting
    {
        public float ToggleForSeconds { get; set; } = 5f;

        public override void Start()
        {
            base.Start();
            FlickerColor = Color.red;
            FlickerTime = 0.5f;
            FlickerIntensity = 3f;
            LerpColorTime = 0.5f;
        }

        public void OnTakeDamage(DamageInfo damageInfo)
        {
            StopCoroutine(DisableEmergencyLightingAfterWait());
            if (damageInfo.damage > 0 && damageInfo.type == DamageType.Normal || damageInfo.type == DamageType.Electrical)
            {
                if (IsRunning == false)
                {
                    EnableEmergencyLighting();
                }
                StartCoroutine(DisableEmergencyLightingAfterWait());
            }
        }

        private IEnumerator DisableEmergencyLightingAfterWait()
        {
            yield return new WaitForSeconds(ToggleForSeconds);
            DisableEmergencyLighting();
        }

        public void OnDeathPrepare()
        {
            Utilities.Log.Print("OnDeathPrepare LockEmergencyLightingOn");
            LockEmergencyLightingOn();
        }
    }
}
