using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using ARPG.AI;
using ARPG.Movement;
using ARPG.Combat;

namespace ARPG.Controller
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] AIState currentState;
        [SerializeField] AIState remainState;
        
        public Transform eyes;
        public Transform[] waypoints;
        public int waypointIndex = 0;
        public float currentStateTime = 0f;

        public AIMovement aiMovement;
        public AICombat aiCombat;

        Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();
            aiMovement = GetComponent<AIMovement>();
            aiCombat = GetComponent<AICombat>();
        }

        void Start()
        {
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
                Debug.Log("STATE " + nextState.name);
                currentState = nextState;
                currentStateTime = 0f;
                currentState.OnStateEnter(this);
            }
        }
    }
}
