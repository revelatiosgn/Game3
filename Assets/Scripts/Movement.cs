using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSRPG
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : MonoBehaviour
    {
        public float movementSpeed = 5f;
        public float rotationSpeed = 5f;

        private Rigidbody rb;
        private Animator animator;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            HandleMovement();
            HandleRotation();
        }

        void HandleMovement()
        {
            Vector3 direction = new Vector3(InputHandler.movementInput.x, 0f, InputHandler.movementInput.y);
            direction.Normalize();

            rb.velocity = direction * movementSpeed;
            //rb.velocity = Vector3.SmoothDamp(rb.velocity, direction * speed, ref vl, 0.2f);

            animator.SetFloat("vertical", direction.magnitude, 0.1f, Time.deltaTime);
        }

        void HandleRotation()
        {
            Vector3 direction = rb.velocity.normalized;
            direction.y = 0f;

            if (direction == Vector3.zero)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}

