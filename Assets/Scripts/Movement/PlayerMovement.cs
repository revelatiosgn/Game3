using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Core;
using ARPG.Controller;

namespace ARPG.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField][Range(0f, 20f)] float gravity = 5f;
        
        CharacterController characterController;
        Animator animator;
        PlayerController playerController;

        Vector3 velocity;
        float gravitySpeed;

        void Awake()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            playerController = GetComponent<PlayerController>();
        }

        void Update()
        {
            Move();
        }

        void Move()
        {
            if (playerController.isInteracting)
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

            gravitySpeed += gravity * Time.deltaTime;
            velocity.y -= gravitySpeed;
            characterController.Move(velocity * Time.deltaTime);

            if (characterController.isGrounded)
                gravitySpeed = 0f;

            velocity = Vector3.zero;
        }


        // void Jump()
        // {
        //     if (characterController.isGrounded && InputHandler.jumpInput)
        //         velocityY = Mathf.Sqrt(2 * gravity * jumpHeight);
        // }
    }
}
