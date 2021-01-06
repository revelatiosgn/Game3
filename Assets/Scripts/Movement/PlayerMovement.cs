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

        float velocityH;
        float velocityV;

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

        void Start()
        {
            // animator.SetBool("aimMovement", true);
        }

        void Update()
        {
            // AimMovement();
            // RegularMovement();

            if (state == MovementState.Regular)
                RegularMovement();
            else
                AimMovement();

            // if (InputHandler.defenceBeginInput)
            // {
            //     animator.SetBool("aimMovement", true);
            //     state = MovementState.Aim;
            // }
            
            // if (InputHandler.defenceEndInput)
            // {
            //     animator.SetBool("aimMovement", false);
            //     state = MovementState.Regular;
            // }
        }

        void AimMovement()
        {
            Vector3 direction = Camera.main.transform.rotation.eulerAngles;
            direction.x = 0f;

            Vector3 moveDirection = Vector3.zero;
            moveDirection.x += InputHandler.movementInput.x;
            moveDirection.z += InputHandler.movementInput.y;

            float h = Mathf.SmoothDamp(animator.GetFloat("horizontal"), moveDirection.x, ref velocityH, 0.1f);
            float v = Mathf.SmoothDamp(animator.GetFloat("vertical"), moveDirection.z, ref velocityV, 0.1f);

            animator.SetFloat("horizontal", h);
            animator.SetFloat("vertical", v);

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
            
            float v = Mathf.SmoothDamp(animator.GetFloat("vertical"), direction.magnitude, ref velocityV, 0.1f);

            animator.SetFloat("horizontal", 0f);
            animator.SetFloat("vertical", v);

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = targetRotation;
                CameraFollow.SetState(CameraFollow.CameraState.Regular);
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
