using UnityEngine;

namespace Submarines.DefaultCyclopsContent
{
    /**
     * The live mixin data the default cyclops comes with.
     */
    public class CyclopsLiveMixinData
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
            instance.explodeOnDestroy = true;
            instance.invincibleInCreative = true;
            instance.knifeable = false;
            instance.loopEffectBelowPercent = 0;
            instance.loopingDamageEffect = null;
            instance.maxHealth = 1500;
            instance.minDamageForSound = 0;
            instance.passDamageDataOnDeath = false;
            instance.weldable = false;

            return instance;
        }
    }
}
