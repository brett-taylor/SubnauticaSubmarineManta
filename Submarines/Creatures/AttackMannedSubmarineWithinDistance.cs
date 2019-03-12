using Submarines.Content;
using UnityEngine;

namespace Submarines.Creatures
{
    /**
     * Basic class that will cause a creature to attack a submarine if:
     * the submarine is is in range
     * the player is inside the submarine.
     */
    public class AttackMannedSubmarineWithinDistance : CreatureAction
    {
        public static readonly float SWIM_VELOCITY = 25f;
        public static readonly float BITE_REFRACTORTY_PERIOD = 3f;
        public static readonly float MAX_DISTANCE = 100f;
        public static readonly float UPDATE_AGGRESSION_TIMER = 0.5f;
        private float lastBiteTime;
        private LastTarget lastTarget;
        private float distanceToMannedSub;

        private void Start()
        {
            lastTarget = GetComponent<LastTarget>();
            Utilities.Log.Print("Added onto creature: " + gameObject.name);
        }

        public override void OnEnable()
        {
            base.OnEnable();
            lastBiteTime = Time.time;   
            distanceToMannedSub = MAX_DISTANCE + 1;
            InvokeRepeating("UpdateAggression", Random.value * UPDATE_AGGRESSION_TIMER, UPDATE_AGGRESSION_TIMER);
        }

        private void OnDisable()
        {
            CancelInvoke();
        }

        private void UpdateAggression()
        {
            if (Player.main.currentSub == null)
            {
                distanceToMannedSub = MAX_DISTANCE + 1;
                return;
            }

            distanceToMannedSub = Vector3.Distance(Player.main.currentSub.gameObject.transform.position, gameObject.transform.position);
        }

        public override float Evaluate(Creature creature)
        {
            Submarine currentSubmarine = Player.main.currentSub?.GetComponent<Submarine>();
            LiveMixin liveMixin = currentSubmarine?.GetComponent<LiveMixin>();
            bool pastBiteRefractoryTime = Time.time > lastBiteTime + BITE_REFRACTORTY_PERIOD;
            bool withinRange = distanceToMannedSub <= MAX_DISTANCE;

            bool possibleAttack = currentSubmarine != null && liveMixin != null && liveMixin.IsAlive() && pastBiteRefractoryTime && withinRange;
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
            lastBiteTime = Time.time;
        }
    }
}
