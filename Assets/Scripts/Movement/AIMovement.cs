﻿using System.Collections;
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

        public Transform target;

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
            if (target != null)
                navMeshAgent.destination = target.position;
            else
                Stop();

            if (!IsStopped())
            {
                Move();
            }
            else
            {
                Stop();
            }

            stopped = navMeshAgent.isStopped;
        }

        public void SetRunning(bool isRunning)
        {
            desiredSpeed = isRunning ? runSpeed : walkSpeed;
        }

        bool IsStopped()
        {
            if (!navMeshAgent.pathPending)
            {
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        void Move()
        {
            speed = Mathf.SmoothDamp(speed, desiredSpeed, ref smoothVelocity, smoothTime);
            animator.SetFloat("vertical", speed);
            navMeshAgent.isStopped = false;
        }

        void Stop()
        {
            speed = Mathf.SmoothDamp(speed, 0f, ref smoothVelocity, smoothTime);
            animator.SetFloat("vertical", speed);
            target = null;
            navMeshAgent.destination = transform.position;
            navMeshAgent.isStopped = true;
        }

        void OnAnimatorMove ()
        {
            if (navMeshAgent.isStopped)
            {
                transform.position = animator.rootPosition;
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

                if (target != null)
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawWireSphere(target.position, 0.4f);
                }
            }
        }
    }
}
