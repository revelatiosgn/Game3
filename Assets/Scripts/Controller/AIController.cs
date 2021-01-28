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
        [SerializeField] AIState remainState;

        public AITransition[] anyStateTransitions;

        public Transform eyes;
        public Transform[] waypoints;
        public int waypointIndex = 0;
        public float currentStateTime = 0f;
        public List<BaseController> charactersCanSee;
        public BaseController chaseTarget;

        public AICombat aiCombat;
        public AIMovement aiMovement;

        Animator animator;

        protected override void Awake()
        {
            base.Awake();

            animator = GetComponent<Animator>();
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

        public void TransitionToState(AIState nextState)
        {
            if (nextState != remainState)
            {
                Debug.Log(nextState.name);
                currentState.OnStateExit(this);
                currentState = nextState;
                currentStateTime = 0f;
                currentState.OnStateEnter(this);
            }
        }

        public bool IsState(AIState state)
        {
            return currentState == state;
        }
    }
}
