using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSRPG
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : MonoBehaviour
    {
        public float speed = 10f;

        private Rigidbody rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            HandleMovement();
        }

        void HandleMovement()
        {
            Vector3 direction = new Vector3(InputHandler.movementInput.x, 0f, InputHandler.movementInput.y);

            rb.velocity = direction * speed;
        }
    }
}

