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

        Rigidbody rb;
        CapsuleCollider capsuleCollider;
        PlayerController playerController;
        Animator animator;

        public bool isGrounded;
        public bool isGravity = true;
        [SerializeField][Range(0f, 1f)] float skinWidth = 0.1f;
        [SerializeField][Range(0f, 90f)] float slopeLimit = 30f;
        [SerializeField][Range(0f, 1f)] float stepLimit = 1.0f;
        [SerializeField][Range(0f, 1f)] float stepRadius = 0.1f;
        [SerializeField][Range(0f, 100f)] float jumpHeight = 1f;

        Vector3 inputVelocity;
        Vector3 gravityVelocity;
        LayerMask layerMask;

        float velocityH;
        float velocityV;
        Quaternion velocityRot;

        public List<Vector3> snapPoints;

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
            rb = GetComponent<Rigidbody>();
            capsuleCollider = GetComponent<CapsuleCollider>();
            playerController = GetComponent<PlayerController>();
            animator = GetComponent<Animator>();
        }

        void Start()
        {
            animator.SetBool("aimMovement", true);
            layerMask = LayerMask.GetMask("Environment");
        }

        public int colls;
        List<Collision> collisions = new List<Collision>();

        private void OnCollisionEnter(Collision other)
        {
            collisions.Add(other);
            colls = collisions.Count;
        }

        private void OnCollisionExit(Collision other)
        {
            collisions.Remove(other);
            colls = collisions.Count;
        }

        void FixedUpdate()
        {
            CheckGround();
            Snap();
            ApplyGravity();

            rb.velocity = inputVelocity + gravityVelocity;
        }

        void CheckGround()
        {
            float radius = capsuleCollider.radius + skinWidth;
            float offset = isGrounded ? stepLimit : 0.01f;
            RaycastHit hit;
            if (Physics.SphereCast(rb.position + Vector3.up, radius, Vector3.down, out hit, 1.0f - radius + offset, layerMask))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }

        void CollectSnapPoints()
        {
            snapPoints.Clear();

            float radius = capsuleCollider.radius + skinWidth;
            RaycastHit[] hits = Physics.SphereCastAll(rb.position + Vector3.up * (1.0f + radius), radius, Vector3.down, 2.0f + 2 * radius, layerMask);
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];

                if (Mathf.Abs(hit.point.y - rb.position.y) > stepLimit)
                    continue;

                float angle = Vector3.Angle(hit.normal, Vector3.up);
                if (angle > slopeLimit)
                {
                    RaycastHit stepHit;
                    if (Physics.SphereCast(hit.point + Vector3.up * (stepRadius * 2f), stepRadius, Vector3.down, out stepHit, stepRadius * 2f, layerMask))
                    {
                        angle = 90.0f - Vector3.Angle(stepHit.point - hit.point, Vector3.up);
                        Debug.Log(angle);

                        if (angle > slopeLimit)
                        {
                            Debug.Log("LIMIT");
                        }
                        else
                        {
                            Debug.Log("OK");
                        }

                        if ((stepHit.point - hit.point).sqrMagnitude > Mathf.Epsilon && angle > slopeLimit)
                            continue;
                    }
                    else
                    {
                        continue;
                    }
                }

                float d = radius - radius * Mathf.Cos(Vector3.Angle(hit.normal, Vector3.up) * Mathf.Deg2Rad);
                float y = hit.point.y - d;

                snapPoints.Add(new Vector3(hit.point.x, y, hit.point.z));
            }
        }

        void Snap()
        {
            CollectSnapPoints();

            if (!isGrounded)
                return;

            Vector3 snapPoint = new Vector3(0f, float.MinValue, 0f);
            foreach (Vector3 sp in snapPoints)
            {
                if (sp.y > snapPoint.y)
                    snapPoint = sp;
            }

            if (snapPoint.y > float.MinValue)
            {
                Vector3 position = rb.position;
                position.y = snapPoint.y;
                rb.MovePosition(position);
            }
        }

        private void OnCollisionStay(Collision other) {
            Debug.Log(other.collider.name);
        }

        void ApplyGravity()
        {
            if (!isGravity)
                return;

            if (isGrounded)
                gravityVelocity = Vector3.zero;
            else
                gravityVelocity += Physics.gravity * Time.fixedDeltaTime;
        }

        void OnAnimatorMove()
        {
            inputVelocity.x = animator.velocity.x;
            inputVelocity.z = animator.velocity.z;
        }

        void OnDrawGizmos()
        {
        }

        void Land()
        {
            Debug.Log("Land");

            // animator.SetBool("jump", false);
            // animator.SetFloat("horizontal", 0f);
            // animator.SetFloat("vertical", 0f);
            isGrounded = true;
        }

        public void Jump()
        {
            // if (characterController.isGrounded)
            // {
            //     Debug.Log("JUMP");

            //     jumpVelocity.x = animator.velocity.x;
            //     jumpVelocity.z = animator.velocity.z;
            //     jumpVelocity.y = Mathf.Sqrt(2 * jumpHeight * -Physics.gravity.y);
            //     // animator.SetBool("jump", true);
            //     isInAir = true;
            //     isGrounded = false;
            // }
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
                transform.rotation = targetRotation;
            }
        }
    }
}
