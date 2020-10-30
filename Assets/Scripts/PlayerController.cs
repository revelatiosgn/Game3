using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField][Range(0f, 20f)] float walkingSpeed = 1.5f;
        [SerializeField][Range(0f, 20f)] float runningSpeed = 5.0f;
        [SerializeField][Range(0f, 20f)] float jumpHeight = 5f;
        [SerializeField][Range(0f, 1f)] float smoothTime = 0.5f;

        [SerializeField] Weapon weapon;

        public bool isInteracting = false;

        Movement movement;
        Animator animator;

        void Awake()
        {
            movement = GetComponent<Movement>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            Movement();
            Actions();
        }

        void Movement()
        {
            if (isInteracting)
                return;

            Vector3 direction = Vector3.zero;
            direction += Camera.main.transform.right * InputHandler.movementInput.x;
            direction += Camera.main.transform.forward * InputHandler.movementInput.y;
            direction.y = 0f;
            direction.Normalize();

            animator.SetFloat("vertical", direction.magnitude);

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = targetRotation;
            }
        }

        void Actions()
        {
            if (InputHandler.attackInput)
            {
                isInteracting = true;
                animator.CrossFade("Attack", 0.2f);
            }
        }

        void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            Debug.Log(animatorStateInfo);
        }
    }
}