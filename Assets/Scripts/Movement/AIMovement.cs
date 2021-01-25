using System.Collections;
using System.Collections.Generic;
using ARPG.Controller;
using UnityEngine;
using UnityEngine.AI;

namespace ARPG.Movement
{
    public class AIMovement : MonoBehaviour
    {
        [SerializeField] float smoothTime;
        [SerializeField] float walkSpeed = 0.5f;
        [SerializeField] float runSpeed = 0.9f;

        NavMeshAgent navMeshAgent;
        Animator animator;

        float speed;
        float smoothVelocity;
        float desiredSpeed;

        public float vel;
        public float sp;
        public bool stopped;

        void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        void Start()
        {
            navMeshAgent.updatePosition = false;
            navMeshAgent.acceleration = float.MaxValue;
            SetRunning(false);
        }

        void Update()
        {
            if (!navMeshAgent.pathPending)
            {
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        navMeshAgent.isStopped = true;
                    }
                }
            }

            if (navMeshAgent.isStopped)
            {
                animator.SetFloat("vertical", 0f);
            }
            else
            {
                animator.SetFloat("vertical", desiredSpeed);
            }

            stopped = navMeshAgent.isStopped;
        }

        public void Move(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Stop()
        {
            navMeshAgent.isStopped = true;
        }

        public bool IsStopped()
        {
            return navMeshAgent.isStopped;
        }

        public void SetRunning(bool isRunning)
        {
            desiredSpeed = isRunning ? runSpeed : walkSpeed;
        }

        void OnAnimatorMove ()
        {
            if (navMeshAgent.isStopped)
            {
                transform.position = animator.rootPosition;
                navMeshAgent.Warp(transform.position);
                navMeshAgent.destination = transform.position;
            }
            else
            {
                transform.position = navMeshAgent.nextPosition;
                navMeshAgent.speed = animator.deltaPosition.magnitude / Time.deltaTime;
            }
            
            vel = navMeshAgent.velocity.magnitude;
            sp = navMeshAgent.speed;
        }

        void OnDrawGizmos()
        {
            if (navMeshAgent)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawWireSphere(navMeshAgent.nextPosition, 0.3f);
            }
        }
    }
}
