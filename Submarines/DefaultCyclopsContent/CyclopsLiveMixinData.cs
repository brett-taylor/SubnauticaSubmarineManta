namespace Submarines.DefaultCyclopsContent
{
    /**
     * The live mixin data the default cyclops comes with.
     */
    public class CyclopsLiveMixinData
    {
        public static LiveMixinData Get()
        {
            return new LiveMixinData()
            {
                broadcastKillOnDeath = false,
                canResurrect = false,
                damageEffect = null,
                deathEffect = null,
                destroyOnDeath = false,
                electricalDamageEffect = null,
                explodeOnDestroy = true,
                invincibleInCreative = true,
                knifeable = false,
                loopEffectBelowPercent = 0,
                loopingDamageEffect=  null,
                maxHealth = 1500,
                minDamageForSound = 0,
                passDamageDataOnDeath = false,
                weldable = false
            };
        }
    }
}
