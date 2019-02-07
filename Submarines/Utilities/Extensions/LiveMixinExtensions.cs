namespace Submarines.Utilities.Extensions
{
    /**
    * Extension methods for the Livemixin component.
    */
    public static class LiveMixinExtensions
    {
        public static void PrintAllLiveMixinDetails(this LiveMixin liveMixin)
        {
            Log.Print("LiveMixin broadcastKillOnDeath: " + liveMixin.broadcastKillOnDeath);
            Log.Print("LiveMixin canResurrect: " + liveMixin.canResurrect);
            Log.Print("LiveMixin damageEffect: " + liveMixin.damageEffect);
            Log.Print("LiveMixin deathEffect: " + liveMixin.deathEffect);
            Log.Print("LiveMixin destroyOnDeath: " + liveMixin.destroyOnDeath);
            Log.Print("LiveMixin electricalDamageEffect: " + liveMixin.electricalDamageEffect);
            Log.Print("LiveMixin explodeOnDestroy: " + liveMixin.explodeOnDestroy);
            Log.Print("LiveMixin invincibleInCreative: " + liveMixin.invincibleInCreative);
            Log.Print("LiveMixin knifeable: " + liveMixin.knifeable);
            Log.Print("LiveMixin loopEffectBelowPercent: " + liveMixin.loopEffectBelowPercent);
            Log.Print("LiveMixin loopingDamageEffect: " + liveMixin.loopingDamageEffect);
            Log.Print("LiveMixin maxHealth: " + liveMixin.maxHealth);
            Log.Print("LiveMixin minDamageForSound: " + liveMixin.minDamageForSound);
            Log.Print("LiveMixin passDamageDataOnDeath: " + liveMixin.passDamageDataOnDeath);
            Log.Print("LiveMixin weldable: " + liveMixin.weldable);
            Log.Print("LiveMixin damageClip: " + liveMixin.damageClip);
            Log.Print("LiveMixin data: " + liveMixin.data);
            Log.Print("LiveMixin deathClip: " + liveMixin.deathClip);
            Log.Print("LiveMixin health: " + liveMixin.health);
            Log.Print("LiveMixin invincible: " + liveMixin.invincible);
            Log.Print("LiveMixin onHealDamage: " + liveMixin.onHealDamage);
            Log.Print("LiveMixin onHealTempDamage: " + liveMixin.onHealTempDamage);
            Log.Print("LiveMixin shielded: " + liveMixin.shielded);
            Log.Print("LiveMixin data broadcastKillOnDeath: " + liveMixin.data.broadcastKillOnDeath);
            Log.Print("LiveMixin data canResurrect: " + liveMixin.data.canResurrect);
            Log.Print("LiveMixin data damageEffect: " + liveMixin.data.damageEffect);
            Log.Print("LiveMixin data deathEffect: " + liveMixin.data.deathEffect);
            Log.Print("LiveMixin data destroyOnDeath: " + liveMixin.data.destroyOnDeath);
            Log.Print("LiveMixin data electricalDamageEffect: " + liveMixin.data.electricalDamageEffect);
            Log.Print("LiveMixin data explodeOnDestroy: " + liveMixin.data.explodeOnDestroy);
            Log.Print("LiveMixin data invincibleInCreative: " + liveMixin.data.invincibleInCreative);
            Log.Print("LiveMixin data knifeable: " + liveMixin.data.knifeable);
            Log.Print("LiveMixin data loopEffectBelowPercent: " + liveMixin.data.loopEffectBelowPercent);
            Log.Print("LiveMixin data loopingDamageEffect: " + liveMixin.data.loopingDamageEffect);
            Log.Print("LiveMixin data maxHealth: " + liveMixin.data.maxHealth);
            Log.Print("LiveMixin data minDamageForSound: " + liveMixin.data.minDamageForSound);
            Log.Print("LiveMixin data passDamageDataOnDeath: " + liveMixin.data.passDamageDataOnDeath);
            Log.Print("LiveMixin data weldable: " + liveMixin.data.weldable);
        }
    }
}
