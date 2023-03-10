using EventArgs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviors.Boss
{
    public class BossController : MonoBehaviour
    {
        [HideInInspector] public BossHelper helper;

        [HideInInspector] public NavMeshAgent thisAgent;
        [HideInInspector] public Animator thisAnimator;
        [HideInInspector] public LifeScript thisLife;

        [HideInInspector] public StateMachine stateMachine;
        [HideInInspector] public Idle idleState;
        [HideInInspector] public Follow followState;
        [HideInInspector] public AttackNormal AttackNormalState;
        [HideInInspector] public AttackRitual AttackRitualState;
        [HideInInspector] public AttackSuper AttackSuperState;
        [HideInInspector] public Hurt hurtState;
        [HideInInspector] public Dead DeadState;



        [Header("General")]
        public float lowHealthThreshold = 0.4f;
        public Transform staffTop;
        public Transform staffBottom;

        [Header("Idle")]
        public float idleDuration = 0.3f;

        [Header("Follow")]
        public float ceaseFollowInterval = 4f;

        [Header("Attack")]
        public int attackDamage = 1;

        [Header("Attack Normal")]
        public float attackNormalMagicDelay = 0f;
        public float attackNormalDuration = 0f;

        [Header("Attack Super")]
        public float attackSuperMagicDelay = 0f;
        public float attackSuperDuration = 0f;
        public float attackSuperMagicDuration = 1f;
        public int attackSuperMagicCount = 5;

        [Header("Attack Ritual")]
        public float distanceToRitual = 2.5f;
        public float attackRitualDelay = 0f;
        public float attackRitualDuration = 0f;

        [Header("Hurt")]
        public float hurtDuration = 0.5f;

        [Header("Magic")]
        public GameObject fireballPrefab;
        public GameObject energyballPrefab;
        public GameObject ritualPrefab;
        

        [Header("Debug")]
        public string currentStateName;

        [HideInInspector] public List<IEnumerator> stateCoroutines = new List<IEnumerator>();


        private void Awake()
        {
            thisAgent = GetComponent<NavMeshAgent>();
            thisAnimator = GetComponent<Animator>();
            thisLife = GetComponent<LifeScript>();

            helper = new BossHelper(this);
        }

        private void Start()
        {
            stateMachine = new StateMachine();

            idleState = new Idle(this);
            followState = new Follow(this);
            AttackNormalState = new AttackNormal(this);
            AttackRitualState = new AttackRitual(this);
            AttackSuperState = new AttackSuper(this);
            hurtState = new Hurt(this);
            DeadState = new Dead(this);

            stateMachine.ChangeState(idleState);

            thisLife.OnDamage += OnDamage;
        }

        private void OnDamage(object sender, DamageEventArgs args)
        {
            Debug.Log("Boss recebeu " + args.damage + " de dano de " + args.attacker.name);
            //stateMachine.ChangeState(hurtState);
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
    }
}

