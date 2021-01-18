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

        public Transform targetTransform;

        NavMeshAgent navMeshAgent;
        Animator animator;
        AIController aiController;

        float speed;
        float smoothVelocity;

        void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            aiController = GetComponent<AIController>();
        }

        void Start()
        {
            navMeshAgent.updatePosition = false;
            navMeshAgent.updateRotation = false;
            navMeshAgent.acceleration = float.MaxValue;
        }

        void Update()
        {
            targetTransform = aiController.targetTransform;

            if (!targetTransform || aiController.state != AIController.State.Chase)
            {
                Stop();
                return;
            }

            if (navMeshAgent.destination != targetTransform.position)
                navMeshAgent.destination = targetTransform.position;

            float distance = Vector3.Distance(navMeshAgent.destination, transform.position);

            if (distance > 0.1f)
            {
                Move();
            }
            else
            {
                Stop();
            }
        }

        void Move()
        {
            speed = Mathf.SmoothDamp(speed, 1f, ref smoothVelocity, smoothTime);
            animator.SetFloat("vertical", speed);
        }

        void Stop()
        {
            speed = Mathf.SmoothDamp(speed, 0f, ref smoothVelocity, smoothTime);
            animator.SetFloat("vertical", speed);
        }

        void OnAnimatorMove ()
        {
            Vector3 direction = navMeshAgent.steeringTarget - transform.position;
            direction.y = 0f;

            if (direction != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(direction);
            transform.position = navMeshAgent.nextPosition;
            navMeshAgent.speed = animator.deltaPosition.magnitude / Time.deltaTime;
        }

        void OnDrawGizmos()
        {
            if (navMeshAgent)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawWireSphere(navMeshAgent.steeringTarget, 0.3f);
            }
        }
    }
}
