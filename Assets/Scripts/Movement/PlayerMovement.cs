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
            Regular = 0,
            Aim
        }

        public enum ColliderState
        {
            Grounded = 0,
            InAir,
            Floating,
            Sliding,
            Jump
        }

        Rigidbody rb;
        CapsuleCollider capsuleCollider;
        PlayerController playerController;
        Animator animator;

        [SerializeField][Range(0f, 1f)] float skinWidth = 0.1f;
        [SerializeField][Range(0f, 90f)] float slopeLimit = 30f;
        [SerializeField][Range(0f, 1f)] float stepLimit = 1.0f;
        [SerializeField][Range(0f, 100f)] float jumpHeight = 1f;
        [SerializeField][Range(0f, 10f)] float gravityMultiplier = 1f;
        [SerializeField][Range(0f, 100f)] float landSpeedLimit = 8f;

        Vector3 inputVelocity;
        Vector3 gravityVelocity;
        Vector3 jumpVelocity;
        LayerMask layerMask;

        float velocityH;
        float velocityV;
        Quaternion velocityRot;
        float stepHeight;
        Vector3 groundPoint;
        Vector3 groundNormal;
        Vector3 snapVelocity;
        float snapRadius;
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

        public ColliderState colliderState = ColliderState.InAir;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            capsuleCollider = GetComponent<CapsuleCollider>();
            playerController = GetComponent<PlayerController>();
            animator = GetComponent<Animator>();
        }

        void Start()
        {
            animator.SetBool("aimMovement", true);
            layerMask = LayerMask.GetMask("Environment");

            stepHeight = capsuleCollider.center.y - capsuleCollider.height * 0.5f;
        }

        public int colls;
        List<ContactPoint> contactPoints = new List<ContactPoint>();

        private void OnCollisionEnter(Collision other)
        {
            contactPoints.AddRange(other.contacts);
            colls = contactPoints.Count;
        }
        private void OnCollisionStay(Collision other)
        {
            contactPoints.AddRange(other.contacts);
            colls = contactPoints.Count;
        }

        void FixedUpdate()
        {
            if (colliderState == ColliderState.Jump)
            {
                jumpVelocity.y = Mathf.Sqrt(2 * jumpHeight * -Physics.gravity.y * gravityMultiplier);
                rb.velocity = inputVelocity + gravityVelocity + jumpVelocity;
                rb.MovePosition(rb.position + rb.velocity * Time.fixedDeltaTime);
                colliderState = ColliderState.InAir;
                contactPoints.Clear();

                animator.SetTrigger("jump");
                animator.SetTrigger("fall");
            }

            float fallSpeed = animator.GetFloat("fallSpeed");
            if (colliderState == ColliderState.InAir)
                fallSpeed = Mathf.Clamp01(fallSpeed + Time.fixedDeltaTime * 1f);
            else
                fallSpeed = Mathf.Clamp01(fallSpeed - Time.fixedDeltaTime * 10f);
            animator.SetFloat("fallSpeed", fallSpeed);

            rb.velocity = inputVelocity + gravityVelocity + jumpVelocity;

            bool isInAir = colliderState == ColliderState.InAir;

            CheckGround();
            Snap();
            ApplyGravity();

            if (colliderState == ColliderState.Grounded)
            {
                inputVelocity = Vector3.ProjectOnPlane(inputVelocity, groundNormal);
            }

            if (isInAir && colliderState != ColliderState.InAir)
            {
                Land();
            }

            if (colliderState == ColliderState.InAir)
            {
                inputVelocity.y = 0f;
            }

            colls = contactPoints.Count;
            contactPoints.Clear();
        }

        void CheckGround()
        {
            float offset = colliderState == ColliderState.InAir ? -rb.velocity.y * Time.fixedDeltaTime : stepHeight;

            RaycastHit hit;
            if (Physics.Raycast(rb.position + Vector3.up * (stepHeight + 0.1f), Vector3.down, out hit, stepHeight + offset + 0.1f, layerMask))
            {
                colliderState = ColliderState.Grounded;
                groundNormal = hit.normal;
                groundPoint = hit.point;
            }
            else
            {
                colliderState = ColliderState.InAir;
            }

            if (colliderState == ColliderState.Grounded)
            {
                if (Vector3.Angle(groundNormal, Vector3.up) > slopeLimit)
                    colliderState = ColliderState.Sliding;
            }

            if (colliderState == ColliderState.InAir)
            {
                for (int i = 0; i < contactPoints.Count; i++)
                {
                    if (Vector3.Dot(contactPoints[i].normal, Vector3.up) < -0.001f)
                    {
                        jumpVelocity.y = 0f;
                        continue;
                    }

                    Vector3 v1 = Vector3.ProjectOnPlane(contactPoints[i].normal, Vector3.up).normalized;
                    for (int j = i + 1; j < contactPoints.Count; j++)
                    {
                        if (Vector3.Dot(contactPoints[j].normal, Vector3.up) < -Mathf.Epsilon)
                        {
                            jumpVelocity.y = 0f;
                            continue;
                        }

                        Vector3 v2 = Vector3.ProjectOnPlane(contactPoints[j].normal, Vector3.up).normalized;
                        if (Vector3.Dot(v1, v2) < -0.2f)
                        {
                            colliderState = ColliderState.Floating;
                            break;
                        }
                    }

                    if (colliderState == ColliderState.Floating)
                        break;
                }
            }
        }

        void ApplyGravity()
        {
            if (colliderState == ColliderState.InAir || colliderState == ColliderState.Sliding)
                gravityVelocity += Physics.gravity * Time.fixedDeltaTime * gravityMultiplier;
            else
                gravityVelocity = Vector3.zero;
        }

        void Snap()
        {
            snapRadius = 0f;

            if (colliderState != ColliderState.Grounded)
                return;

            snapRadius = Mathf.Clamp((rb.velocity.x * rb.velocity.x + rb.velocity.z * rb.velocity.z) * 0.01f, 0f, capsuleCollider.radius * 0.5f) + 0.01f;
            RaycastHit hit;
            if (Physics.SphereCast(rb.position + capsuleCollider.center + Vector3.up * 1f, snapRadius, Vector3.down, out hit, capsuleCollider.center.y + stepHeight + 1f, layerMask))
            {
                float d = snapRadius - snapRadius * Mathf.Cos(Vector3.Angle(hit.normal, Vector3.up) * Mathf.Deg2Rad);
                float y = hit.point.y - d;

                Vector3 pos = rb.position;
                pos.y = y;
                rb.position = Vector3.SmoothDamp(rb.position, pos, ref snapVelocity, 0.05f);
            }
        }

        void OnAnimatorMove()
        {
            if (colliderState == ColliderState.InAir)
                return;

            inputVelocity.x = animator.velocity.x;
            inputVelocity.z = animator.velocity.z;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + Vector3.up * stepHeight, transform.position + Vector3.down * stepHeight);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position + Vector3.up * snapRadius, snapRadius);

            
            Gizmos.color = Color.magenta;
            foreach (ContactPoint contactPoint in contactPoints)
                Gizmos.DrawRay(contactPoint.point, contactPoint.normal);
        }

        public void Jump()
        {
            if (colliderState == ColliderState.Grounded || colliderState == ColliderState.Floating)
            {
                colliderState = ColliderState.Jump;
            }
        }

        void Land()
        {
            jumpVelocity = Vector3.zero;

            if (rb.velocity.y < -landSpeedLimit)
                animator.SetTrigger("land");
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

            if (direction != Vector3.zero && colliderState != ColliderState.InAir)
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
            

            if (direction != Vector3.zero && colliderState != ColliderState.InAir)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Utils.QuaternionUtil.SmoothDamp(transform.rotation, targetRotation, ref velocityRot, 0.05f);
            }

            float vertical = animator.GetFloat("vertical");
            float targetVertical = 0f;
            float sprint = isSprinting ? 0.5f : 0f;
            if (Vector3.Dot(transform.forward, direction) < 0.3f)
                targetVertical = Mathf.SmoothDamp(vertical, 0f, ref velocityV, 0.1f);
            else
                targetVertical = Mathf.SmoothDamp(vertical, direction.magnitude * 0.5f + sprint, ref velocityV, 0.1f);

            animator.SetFloat("horizontal", 0f);
            animator.SetFloat("vertical", targetVertical);
        }
    }
}
