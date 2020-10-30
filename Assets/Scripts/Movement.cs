using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG
{
    public class Movement : MonoBehaviour
    {
        public Vector3 velocity;

        [SerializeField][Range(0f, 20f)] float gravity = 5f;
        
        CharacterController characterController;

        float gravitySpeed;

        // public void SetVelocity(Vector3 value)
        // {
        //     velocity = value;
        // }

        void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        void Update()
        {
            // UpdateMovement();
            // UpdateRotation();
        }

        void UpdateMovement()
        {
            gravitySpeed += gravity * Time.deltaTime;
            velocity.y -= gravitySpeed;
            characterController.Move(velocity * Time.deltaTime);

            if (characterController.isGrounded)
                gravitySpeed = 0f;

            velocity = Vector3.zero;
        }

        void UpdateRotation()
        {
            Vector3 velocity = characterController.velocity;
            velocity.y = 0f;

            if (velocity != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(velocity);
                transform.rotation = targetRotation;
            }
        }

        // void Jump()
        // {
        //     if (characterController.isGrounded && InputHandler.jumpInput)
        //         velocityY = Mathf.Sqrt(2 * gravity * jumpHeight);
        // }
    }
}
