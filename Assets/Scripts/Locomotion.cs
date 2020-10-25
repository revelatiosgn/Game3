using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSRPG
{
    public class Locomotion : MonoBehaviour
    {
        [Range(0f, 20f)] public float walkingSpeed = 1.5f;
        [Range(0f, 20f)] public float runningSpeed = 5.0f;
        [Range(0f, 20f)] public float gravity = 5f;
        [Range(0f, 20f)] public float jumpHeight = 5f;


        CharacterController characterController;
        float velocityY = 0f;

        void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        void Update()
        {
            Move();
            Rotate();
            Jump();
        }

        void Move()
        {
            Vector3 direction = Vector3.zero;
            direction += Camera.main.transform.right * InputHandler.movementInput.x;
            direction += Camera.main.transform.forward * InputHandler.movementInput.y;
            direction.Normalize();

            float speed = InputHandler.runInput ? runningSpeed : walkingSpeed;
            Vector3 currentSpeed = direction * speed;
            velocityY -= gravity * Time.deltaTime;
            currentSpeed.y = velocityY;
            characterController.Move(currentSpeed * Time.deltaTime);

            if (characterController.isGrounded)
                velocityY = 0f;
        }

        void Rotate()
        {
            Vector3 direction = characterController.velocity;
            direction.y = 0f;

            if (direction == Vector3.zero)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
        }

        void Jump()
        {
            if (characterController.isGrounded && InputHandler.jumpInput)
                velocityY = Mathf.Sqrt(2 * gravity * jumpHeight);
        }
    }
}
