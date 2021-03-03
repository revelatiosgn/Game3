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
        [SerializeField] float runSpeed = 0.7f;

        NavMeshAgent navMeshAgent;
        Animator animator;
        AIController aiController;

        float speed;
        float smoothVelocity;
        float desiredSpeed;
        Quaternion velocityRot;

        void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponentInChildren<Animator>();
            aiController = GetComponent<AIController>();
        }

        void Start()
        {
            navMeshAgent.updatePosition = false;
            navMeshAgent.updateRotation = false;
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

            float sp = animator.GetFloat("vertical");
            if (navMeshAgent.isStopped)
                sp = Mathf.SmoothDamp(sp, 0f, ref smoothVelocity, 0.1f);
            else
                sp = Mathf.SmoothDamp(sp, desiredSpeed, ref smoothVelocity, 0.1f);
            animator.SetFloat("vertical", sp);
        }

        public void Move(Vector3 destination)
        {
            if (!aiController.isInteracting)
            {
                navMeshAgent.destination = destination;
                navMeshAgent.isStopped = false;
            }
        }

        public void Stop()
        {
            navMeshAgent.destination = transform.position;
            navMeshAgent.Warp(transform.position);
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
                return;

            Vector3 direction = navMeshAgent.nextPosition - transform.position;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Utils.QuaternionUtil.SmoothDamp(transform.rotation, targetRotation, ref velocityRot, 0.1f);
            }

            transform.position = navMeshAgent.nextPosition;
            navMeshAgent.speed = animator.deltaPosition.magnitude / Time.deltaTime;
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
