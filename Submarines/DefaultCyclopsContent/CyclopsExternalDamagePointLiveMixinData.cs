using UnityEngine;

namespace Submarines.DefaultCyclopsContent
{
    /**
     * The live mixin data the default cyclop's external damage point.
     */
    public class CyclopsExternalDamagePointLiveMixinData
    {
        private static LiveMixinData instance;

        public static LiveMixinData Get()
        {
            if (instance != null)
            {
                return instance;
            }

            instance = ScriptableObject.CreateInstance<LiveMixinData>();
            instance.broadcastKillOnDeath = true;
            instance.canResurrect = false;
            instance.damageEffect = null;
            instance.deathEffect = null;
            instance.destroyOnDeath = false;
            instance.electricalDamageEffect = null;
            instance.explodeOnDestroy = false;
            instance.invincibleInCreative = false;
            instance.knifeable = true;
            instance.loopEffectBelowPercent = 0;
            instance.loopingDamageEffect = null;
            instance.maxHealth = 35;
            instance.minDamageForSound = 0;
            instance.passDamageDataOnDeath = false;
            instance.weldable = true;

            return instance;
        }
    }
}
