using Submarines.Engine;
using System.Collections;
using UnityEngine;

namespace Submarines.DefaultCyclopsContent
{
    /**
     * Adds the "Warning creature attack" sound when the sub is attacked.
     * 
     * If you wish to set the sounds that the cyclops use.
     * Set the following:
     * UnderAttackCallout to the FMOD asset named "AI_Attack"
     */
    public class CyclopsUnderAttackCallout : MonoBehaviour
    {
        public FMODAsset UnderAttackCallout { get; set; } = null;

        public void Start()
        {
            if (EntryPoint.LOAD_DEFAULT_CYCLOPS_ASSETS && UnderAttackCallout == null)
            {
                UnderAttackCallout = CyclopsDefaultAssets.AI_CREATURE_ATTACK;
            }

            if (UnderAttackCallout == null)
            {
                Utilities.Log.Error("CyclopsUnderAttackCallout has no sound (UnderAttackCallout) set");
            }
        }

        public void OnTakeDamage(DamageInfo damageInfo)
        {
            if (damageInfo.damage > 0 && damageInfo.type == DamageType.Normal || damageInfo.type == DamageType.Electrical)
            {
                Utils.PlayFMODAsset(UnderAttackCallout, MainCamera.camera.transform, 20f);
            }
        }
    }
}
 