using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using ARPG.AI;
using ARPG.Movement;
using ARPG.Combat;

namespace ARPG.Controller
{
    public class AIController : BaseController
    {
        [SerializeField] AIState currentState;

        public AITransition[] anyStateTransitions;

        public Transform eyes;
        public Transform[] waypoints;
        
        [HideInInspector] public int waypointIndex = 0;
        [HideInInspector] public float currentStateTime = 0f;
        [HideInInspector] public List<BaseController> charactersCanSee;
        [HideInInspector] public BaseController combatTarget;

        [HideInInspector] public AICombat aiCombat;
        [HideInInspector] public AIMovement aiMovement;
        [HideInInspector] public Animator animator;

        protected override void Awake()
        {
            base.Awake();

            animator = GetComponentInChildren<Animator>();
            aiCombat = GetComponent<AICombat>();
            aiMovement = GetComponent<AIMovement>();
        }

        void Start()
        {
            currentState.OnStateEnter(this);
        }

        void Update()
        {
            currentState.UpdateState(this);
            currentStateTime += Time.deltaTime;
        }

        public bool TransitionToState(AIState nextState)
        {
            if (nextState == null || nextState == currentState)
                return false;

            // Debug.Log(nextState.name);
            currentState.OnStateExit(this);
            currentState = nextState;
            currentStateTime = 0f;
            currentState.OnStateEnter(this);

            return true;
        }

        public bool IsState(AIState state)
        {
            return currentState == state;
        }

        public override void OnTakeDamage(BaseController source, float damage)
        {
            base.OnTakeDamage(source, damage);
            combatTarget = source;
            Debug.Log(combatTarget.name);
        }
    }
}
