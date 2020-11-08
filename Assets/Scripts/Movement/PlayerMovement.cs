using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Core;
using ARPG.Controller;
using System;

namespace ARPG.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        public enum MovementState
        {
            Regular,
            Aim
        }

        [SerializeField][Range(0f, 20f)] float gravity = 5f;
        
        CharacterController characterController;
        Animator animator;
        PlayerController playerController;

        Vector3 velocity;
        float gravitySpeed;

        public MovementState state = MovementState.Regular;
        public MovementState State
        {
            get => state;
            set
            {
                state = value;
            }
        }

        void Awake()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            playerController = GetComponent<PlayerController>();
        }

        void Update()
        {
            if (state == MovementState.Regular)
                RegularMovement();
            else
                AimMovement();
        }

        void AimMovement()
        {
            Vector3 direction = Camera.main.transform.rotation.eulerAngles;
            direction.x = 0f;

            Vector3 moveDirection = Vector3.zero;
            moveDirection.x += InputHandler.movementInput.x;
            moveDirection.z += InputHandler.movementInput.y;

            animator.SetFloat("horizontal", moveDirection.x);
            animator.SetFloat("vertical", moveDirection.z);

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.Euler(direction);
                transform.rotation = targetRotation;
            }
        }

        void RegularMovement()
        {
            // if (playerController.isInteracting)
            // {
            //     animator.SetFloat("vertical", 0f);
            //     return;
            // }


            Vector3 direction = Vector3.zero;
            direction += Camera.main.transform.right * InputHandler.movementInput.x;
            direction += Camera.main.transform.forward * InputHandler.movementInput.y;
            direction.y = 0f;
            direction.Normalize();

            animator.SetFloat("horizontal", 0f);
            animator.SetFloat("vertical", direction.magnitude * 2f);

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
