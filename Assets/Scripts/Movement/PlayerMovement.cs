using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        [SerializeField][Range(0f, 100f)] float jumpHeight = 1f;
        
        CharacterController characterController;
        PlayerController playerController;
        Animator animator;

        float velocityH;
        float velocityV;
        Quaternion velocityRot;

        public bool isGrounded;
        public bool isInAir = false;

        Vector3 snapVelocity;
        Vector3 slideVelocity;
        Vector3 jumpVelocity;

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
            playerController = GetComponent<PlayerController>();
            animator = GetComponent<Animator>();
        }

        void Start()
        {
            animator.SetBool("aimMovement", true);
        }

        void Update()
        {
            // Vector3 velocity = snapVelocity + jumpVelocity + Vector3.down * 0.001f;
            Vector3 velocity = snapVelocity + jumpVelocity - Physics.gravity;

            // isInAir = characterController.Move(velocity * Time.deltaTime) == CollisionFlags.Below;

            isGrounded = characterController.Move(velocity * Time.deltaTime) == CollisionFlags.Below;
    
            // Debug.Log(isInAir);
            // isInAir = characterController.Move(Vector3.down * characterController.stepOffset) != CollisionFlags.CollidedBelow;
            // Debug.Log(isInAir);

            // Snap();

            // if (isInAir && isGrounded)
            //     Land();
        }

        bool Snap()
        {
            if (Physics.SphereCast(new Ray(transform.position + Vector3.up * 0.1f, Vector3.down), characterController.radius, characterController.stepOffset + 0.1f))
            {
                snapVelocity = Vector3.down * characterController.stepOffset / Time.deltaTime;
                return true;
            }
            else
            {
                snapVelocity = Vector3.zero;
                return false;
            }
        }

        void LateUpdate()
        {
            // isGrounded = characterController.isGrounded;
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {

            slideVelocity = Vector3.zero;

            bool isSliding = false;
            Vector3 hitNormal = hit.normal;
            if (Vector3.Angle(Vector3.up, hitNormal) > characterController.slopeLimit)
            {
                RaycastHit hit2;
                if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out hit2, characterController.stepOffset + 0.1f))
                {
                    if (Vector3.Angle(Vector3.up, hit2.normal) > characterController.slopeLimit)
                        isSliding = true;
                }
                else
                {
                    isSliding = true;
                }
            }

            if (isSliding)
            {
                float slideSpeed = 6f;
                float gravity = Physics.gravity.y;
                // velocity.y += gravity;
                // velocity.x += (1f - hitNormal.y) * hitNormal.x * slideSpeed;
                // velocity.z += (1f - hitNormal.y) * hitNormal.z * slideSpeed;
                slideVelocity.x = hitNormal.x;
                slideVelocity.z = hitNormal.z;
            }
        }

        void Land()
        {
            Debug.Log("Land");

            // animator.SetBool("jump", false);
            // animator.SetFloat("horizontal", 0f);
            // animator.SetFloat("vertical", 0f);
            isInAir = false;
            isGrounded = true;
            jumpVelocity = Vector3.zero;
        }

        public void Jump()
        {
            if (characterController.isGrounded)
            {
                Debug.Log("JUMP");

                jumpVelocity.x = animator.velocity.x;
                jumpVelocity.z = animator.velocity.z;
                jumpVelocity.y = Mathf.Sqrt(2 * jumpHeight * -Physics.gravity.y);
                // animator.SetBool("jump", true);
                isInAir = true;
                isGrounded = false;
            }
        }

        public void Move(Vector2 value)
        {
            if (state == MovementState.Regular)
                RegularMovement(value);
            else
                AimMovement(value);
        }

        void AimMovement(Vector2 value)
        {
            Vector3 direction = Camera.main.transform.rotation.eulerAngles;
            direction.x = 0f;

            Vector3 moveDirection = Vector3.zero;
            moveDirection.x += value.x;
            moveDirection.z += value.y;

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

        void RegularMovement(Vector2 value)
        {
            Vector3 direction = Vector3.zero;
            direction += Camera.main.transform.right * value.x;
            direction += Camera.main.transform.forward * value.y;
            direction.y = 0f;
            direction.Normalize();
            
            float v = Mathf.SmoothDamp(animator.GetFloat("vertical"), direction.magnitude, ref velocityV, 0.1f);

            animator.SetFloat("horizontal", 0f);
            animator.SetFloat("vertical", v);

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Utils.QuaternionUtil.SmoothDamp(transform.rotation, targetRotation, ref velocityRot, 0.1f);
            }
        }
    }
}
