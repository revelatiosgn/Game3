using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;
using System;
using ARPG.Core;

namespace ARPG.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        public enum MovementState
        {
            Regular = 0,
            Aim
        }

        [SerializeField] bool isGrounded;
        [SerializeField] [Range(0f, 30f)] float jumpHeight = 2f;
        [SerializeField] [Range(0f, 3f)] float gravityMultiplier = 2f;
        [SerializeField] [Range(0f, 30f)] float slideSpeed = 10f;

        PlayerController playerController;
        Animator animator;
        CharacterController characterController;

        Vector3 inputVelocity;
        Vector3 gravityVelocity;
        Quaternion inputRotation;
        LayerMask layerMask;
        Vector3 ccNormal;

        float velocityH;
        float velocityV;
        Quaternion velocityRot;
        bool isSprinting;

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
            playerController = GetComponent<PlayerController>();
            animator = GetComponentInChildren<Animator>();
            characterController = GetComponent<CharacterController>();
        }

        void Start()
        {
            animator.SetBool("aimMovement", true);
            layerMask = LayerMask.GetMask("Environment");
        }

        void Update()
        {
            Vector3 slideVelocity = Vector3.zero;

            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out hit, characterController.stepOffset + 0.1f, layerMask))
            {
                inputVelocity = Vector3.ProjectOnPlane(inputVelocity, hit.normal);

                if (characterController.isGrounded)
                {
                    if (Vector3.Angle(hit.normal, Vector3.up) > characterController.slopeLimit && Vector3.Angle(ccNormal, Vector3.up) > characterController.slopeLimit)
                    {
                        slideVelocity.x = ((1f - hit.normal.y) * hit.normal.x);
                        slideVelocity.z = ((1f - hit.normal.y) * hit.normal.z);
                        slideVelocity = Vector3.ProjectOnPlane(slideVelocity, hit.normal).normalized * slideSpeed;
                    }
                }
            }
            else
            {
                if (characterController.isGrounded)
                {
                    if (Vector3.Angle(ccNormal, Vector3.up) > characterController.slopeLimit)
                    {
                        slideVelocity.x = ((1f - ccNormal.y) * ccNormal.x);
                        slideVelocity.z = ((1f - ccNormal.y) * ccNormal.z);
                        slideVelocity = Vector3.ProjectOnPlane(slideVelocity, ccNormal).normalized * slideSpeed;
                    }
                }
            }

            if (characterController.isGrounded)
            {
                gravityVelocity = Physics.gravity * 0.01f;
            }
            else
            {
                gravityVelocity += Physics.gravity * gravityMultiplier * Time.deltaTime;
            }

            characterController.Move((inputVelocity + gravityVelocity + slideVelocity) * Time.deltaTime);
            animator.SetFloat("fallSpeed", gravityVelocity.y);
            animator.SetBool("isGrounded", characterController.isGrounded);

            isGrounded = characterController.isGrounded;
        }

        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            ccNormal = hit.normal;
        }

        void OnAnimatorMove()
        {
            inputVelocity.x = animator.velocity.x;
            inputVelocity.z = animator.velocity.z;
        }

        public void Jump()
        {
            gravityVelocity.x = inputVelocity.x;
            gravityVelocity.z = inputVelocity.z;
            gravityVelocity.y = Mathf.Sqrt(2 * jumpHeight * -Physics.gravity.y * gravityMultiplier);
            characterController.Move(gravityVelocity * Time.deltaTime);
            animator.SetTrigger("jump");
        }

        public void Move(Vector2 value)
        {
            if (state == MovementState.Regular)
                RegularMovement(value);
            else
                AimMovement(value);
        }

        public void Sprint(bool isSprinting)
        {
            this.isSprinting = isSprinting;
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
            Vector3 targetDirection = Vector3.zero;
            targetDirection += Camera.main.transform.right * value.x;
            targetDirection += Camera.main.transform.forward * value.y;
            targetDirection.y = 0f;
            targetDirection.Normalize();

            if (targetDirection != Vector3.zero)
            {
                transform.rotation = Utils.QuaternionUtil.SmoothDamp(transform.rotation, Quaternion.LookRotation(targetDirection), ref velocityRot, 0.1f);
            }

            float vertical = animator.GetFloat("vertical");
            float targetVertical = 0f;
            float sprint = isSprinting ? 0.5f : 0f;
            if (Vector3.Dot(transform.forward, targetDirection) < 0.3f)
                targetVertical = Mathf.SmoothDamp(vertical, 0f, ref velocityV, 0.1f);
            else
                targetVertical = Mathf.SmoothDamp(vertical, targetDirection.magnitude * 0.5f + sprint, ref velocityV, 0.1f);

            animator.SetFloat("horizontal", 0f);
            animator.SetFloat("vertical", targetVertical);
        }
    }
}
