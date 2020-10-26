using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSRPG
{
    public class Movement : MonoBehaviour
    {
        public Vector3 currentSpeed;

        [SerializeField][Range(0f, 20f)] float walkingSpeed = 1.5f;
        [SerializeField][Range(0f, 20f)] float runningSpeed = 5.0f;
        [SerializeField][Range(0f, 20f)] float gravity = 5f;
        [SerializeField][Range(0f, 20f)] float jumpHeight = 5f;
        [SerializeField][Range(0f, 1f)] float smoothTime = 0.5f;

        CharacterController characterController;
        float velocityY = 0f;
        Vector3 smoothVelocity;

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
            direction.y = 0f;
            direction.Normalize();

            float speed = InputHandler.walkInput ? walkingSpeed : runningSpeed;
            Vector3 targetSpeed = direction * speed;
            currentSpeed = Vector3.SmoothDamp(currentSpeed, targetSpeed, ref smoothVelocity, smoothTime);

            velocityY -= gravity * Time.deltaTime;
            currentSpeed.y = velocityY;
            characterController.Move(currentSpeed * Time.deltaTime);

            if (characterController.isGrounded)
                velocityY = 0f;



            Vector3 horizontalSpeed = currentSpeed;
            horizontalSpeed.y = 0f;

            if (horizontalSpeed != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(horizontalSpeed);
                transform.rotation = targetRotation;
            }
        }

        void Rotate()
        {
            // Vector3 direction = characterController.velocity;
            // direction.y = 0f;

            // if (direction == Vector3.zero)
            //     return;

            // Quaternion targetRotation = Quaternion.LookRotation(direction);
            // transform.rotation = targetRotation;
        }

        void Jump()
        {
            if (characterController.isGrounded && InputHandler.jumpInput)
                velocityY = Mathf.Sqrt(2 * gravity * jumpHeight);
        }
    }
}
