
using Submarines.Content;
using UnityEngine;

namespace Submarines.Creatures
{
    /**
     * Basic class that will cause a creature to attack a submarine if:
     * the submarine is is in range
     * the player is inside the submarine.
     */
    public class AttackSubmarineBasic : CreatureAction
    {
        public static readonly float SWIM_VELOCITY = 25f;
        public static readonly float SWIM_INTERVAL = 1f;
        private float lastSwimTime;

        private LastTarget lastTarget;

        private void Start()
        {
            lastTarget = GetComponent<LastTarget>();
        }

        public override void OnEnable()
        {
            base.OnEnable();
            lastSwimTime = Time.time;
        }

        private void OnDisable()
        {
        }

        public override float Evaluate(Creature creature)
        {
            Submarine currentSubmarine = Player.main.currentSub?.GetComponent<Submarine>();
            LiveMixin liveMixin = currentSubmarine?.GetComponent<LiveMixin>();
            bool possibleAttack = currentSubmarine != null && liveMixin != null && liveMixin.IsAlive();

            return possibleAttack ? GetEvaluatePriority() : 0f;
        }

        public override void StartPerform(Creature creature)
        {
            SafeAnimator.SetBool(creature.GetAnimator(), "attacking", true);
            lastTarget.SetLockedTarget(Player.main.currentSub.gameObject);
        }

        public override void StopPerform(Creature creature)
        {
            SafeAnimator.SetBool(creature.GetAnimator(), "attacking", false);
            lastTarget.UnlockTarget();
            lastTarget.target = null;
            lastSwimTime = Time.time;
        }

        public override void Perform(Creature creature, float deltaTime)
        {
            if (Player.main.currentSub == null)
            {
                return;
            }

            swimBehaviour.SwimTo(Player.main.currentSub.gameObject.transform.position, SWIM_VELOCITY);
            creature.Aggression.Value = 1f;
        }

        public void OnMeleeAttack(GameObject target)
        {
            Utilities.Log.Print("Basic MeleeAttack target: " + target.name);
        }
    }
}
