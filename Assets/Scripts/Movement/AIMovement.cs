using System.Collections;
using System.Collections.Generic;
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

        float speed;
        float smoothVelocity;

        void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        void Start()
        {
            navMeshAgent.updatePosition = false;
            navMeshAgent.updateRotation = false;
            navMeshAgent.acceleration = float.MaxValue;
        }

        void Update()
        {
            if (!targetTransform)
                return;

            if (navMeshAgent.destination != targetTransform.position)
                navMeshAgent.destination = targetTransform.position;

            float distance = Vector3.Distance(navMeshAgent.destination, transform.position);

            if (distance > 0.1f)
            {
                speed = Mathf.SmoothDamp(speed, 1f, ref smoothVelocity, smoothTime);
                animator.SetFloat("vertical", speed);
            }
            else
            {
                speed = Mathf.SmoothDamp(speed, 0f, ref smoothVelocity, smoothTime);
                animator.SetFloat("vertical", speed);
            }
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
