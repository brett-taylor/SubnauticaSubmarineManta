using UnityEngine;

namespace Submarines.Creatures
{
    /**
     * Added to creatures to allow them to attack Submarines.
     * This is based on how creatures normally attack submarines.
     * TO:DO a noise system.
     * TO:DO this needs more work overall, it's pretty buggy right now.
     */
    public class AttackSubmarineAdvanced : CreatureAction
    {
        public static readonly float TARGET_VALID_RANGE = 150f;
        public static readonly float UPDATE_AGRESSION_TIMER = 1f;
        //public static readonly float UPDATE_AGRESSION_TIMER = 0.5f;
        public static readonly float CREATURE_TRAIT_FALLOFF = 0.01f;
        public static readonly float ATTACK_REFRACTORY_PERIOD = 3f;
        public static readonly float MAX_LEASH_DISTANCE = 160f;
        public static readonly float AGGRESSION_GAINED_PER_SECOND = 0.4f;
        public static readonly float AGGRESSION_ATTACK_THRESHOLD = 0.75f;
        public static readonly float SWIM_INTERVAL = 0.8f;
        public static readonly float SWIM_VELOCITY = 25f;

        private CreatureTrait aggression;
        private float lastAttackTime = 0f;
        private GameObject currentTarget;
        private bool isAttacking = false;
        private LastTarget lastTarget;
        private Vector3 targetAttackPosition;
        private float nextSwimTime;

        private void Start()
        {
            aggression = new CreatureTrait(0f, CREATURE_TRAIT_FALLOFF);
            lastTarget = GetComponent<LastTarget>();
        }

        public override void OnEnable()
        {
            base.OnEnable();
            Utilities.Log.Print("AttackSubmarineCreatureAction enabled");
            InvokeRepeating("UpdateAggression", Random.value * UPDATE_AGRESSION_TIMER, UPDATE_AGRESSION_TIMER);
            lastAttackTime = Time.time;
            nextSwimTime = Time.time; 
            currentTarget = null;
            isAttacking = false;
        }

        private void OnDisable()
        {
            aggression.Value = 0f;
            CancelInvoke();
        }

        private void UpdateAggression()
        {
            aggression.UpdateTrait(UPDATE_AGRESSION_TIMER);
            if (Time.time < lastAttackTime + ATTACK_REFRACTORY_PERIOD)
            {
                return;
            }

            if (Player.main == null || Player.main.currentSub == null || Player.main.currentSub.gameObject.GetComponent<Content.Submarine>() == null)
            {
                return;
            }

            if (Vector3.Distance(Player.main.currentSub.transform.position, transform.position) < TARGET_VALID_RANGE && Vector3.Distance(Player.main.currentSub.transform.position, creature.leashPosition) < MAX_LEASH_DISTANCE)
            {
                aggression.Add(AGGRESSION_GAINED_PER_SECOND * UPDATE_AGRESSION_TIMER);
                Utilities.Log.Print("New Aggression: " + aggression.Value);
            }

            if (isAttacking)
            {
                currentTarget = Player.main.currentSub.gameObject;
                UpdateAttackPoint();
                lastTarget.SetLockedTarget(currentTarget);
            }
        }

        private void UpdateAttackPoint()
        {
            targetAttackPosition = Vector3.zero;
            targetAttackPosition.z = Mathf.Clamp(currentTarget.transform.InverseTransformPoint(transform.position).z, -30f, 30f);
        }

        public override float Evaluate(Creature creature)
        {
            if (currentTarget != null && aggression.Value > AGGRESSION_ATTACK_THRESHOLD && Time.time > lastAttackTime + ATTACK_REFRACTORY_PERIOD)
            {
                return GetEvaluatePriority();
            }

            return 0f;
        }

        public override void StartPerform(Creature creature)
        {
            SafeAnimator.SetBool(creature.GetAnimator(), "attacking", true);
            UpdateAttackPoint();
            lastTarget.SetLockedTarget(currentTarget);
            isAttacking = true;
        }

        public override void StopPerform(Creature creature)
        {
            SafeAnimator.SetBool(creature.GetAnimator(), "attacking", false);
            lastTarget.UnlockTarget();
            lastTarget.target = null;
            isAttacking = false;
            StopAttack();
        }

        public override void Perform(Creature creature, float deltaTime)
        {
            if (Time.time > nextSwimTime && currentTarget != null)
            {
                nextSwimTime = Time.time + SWIM_INTERVAL;
                Vector3 targetPosition = currentTarget.transform.TransformPoint(targetAttackPosition);
                swimBehaviour.SwimTo(targetPosition, SWIM_VELOCITY);
            }

            creature.Aggression.Value = aggression.Value;
        }

        public void OnMeleeAttack(GameObject target)
        {
            Utilities.Log.Print("MeleeAttack target: " + target.name);
            if (target == currentTarget)
            {
                Utilities.Log.Print("ATTENTION! MANTA ATTACKED!");
                StopAttack();
            }
        }

        protected void StopAttack()
        {
            aggression.Value = 0f;
            creature.Aggression.Value = 0f;
            lastAttackTime = Time.time;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Utilities.Log.Print("OnCollisionEnter with: " + collision.gameObject.name);
        }
    }
}
