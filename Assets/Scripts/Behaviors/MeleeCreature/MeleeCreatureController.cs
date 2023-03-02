using EventArgs;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviors
{
    public class MeleeCreatureController : MonoBehaviour
    {
        [HideInInspector] public MeleeCreatureHelper helper;
        [HideInInspector] public NavMeshAgent thisAgent;
        [HideInInspector] public Animator thisAnimator;
        [HideInInspector] public LifeScript thisLife;

        [HideInInspector] public StateMachine stateMachine;
        [HideInInspector] public Idle idleState;
        [HideInInspector] public Follow followState;
        [HideInInspector] public Attack AttackState;
        [HideInInspector] public Hurt hurtState;
        [HideInInspector] public Dead DeadState;



        [Header("General")]
        public float searchRadius = 5f;

        [Header("Idle")]
        public float targetSearchInterval = 1f;

        [Header("Follow")]
        public float ceaseFollowInterval = 4f;

        [Header("Attack")]
        public float distanceToAttack = 1f;
        public float attackRadius = 1.5f;
        public float attackSphereRadius = 1.5f;
        public float damageDelay = 0f;
        public float attackDuration = 1f;
        public int attackDamage = 1;

        [Header("Hurt")]
        public float hurtDuration = 1f;

        [Header("Dead")]
        public float destroyIfFar = 30f;

        [Header("Debug")]
        public string currentStateName;
        //public Vector3 drawSphereAt;


        private void Awake()
        {
            thisAgent = GetComponent<NavMeshAgent>();
            thisAnimator = GetComponent<Animator>();
            thisLife = GetComponent<LifeScript>();

            helper = new MeleeCreatureHelper(this);
        }

        private void Start()
        {
            stateMachine = new StateMachine();

            idleState = new Idle(this);
            followState = new Follow(this);
            AttackState = new Attack(this);
            hurtState = new Hurt(this);
            DeadState = new Dead(this);

            stateMachine.ChangeState(idleState);

            thisLife.OnDamage += OnDamage;
        }

        private void OnDamage(object sender, DamageEventArgs args)
        {
            Debug.Log("Criatura recebeu " + args.damage + " de dano de " + args.attacker.name);
            stateMachine.ChangeState(hurtState);
        }

        private void Update()
        {
            stateMachine.Update();
            currentStateName = stateMachine.currentStateName;

            var velocityRate = thisAgent.velocity.magnitude / thisAgent.speed;
            thisAnimator.SetFloat("fVelocity", velocityRate);
        }

        private void LateUpdate()
        {
            stateMachine.LateUpdate();
        }

        private void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }

        /*private void OnDrawGizmos()
        {
            if(drawSphereAt != null)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawSphere(drawSphereAt, attackSphereRadius);
            }
        }*/
    }
}

