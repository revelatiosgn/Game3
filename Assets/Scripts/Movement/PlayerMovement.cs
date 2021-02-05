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
        
        Rigidbody rb;
        CapsuleCollider coll;
        PlayerController playerController;
        Animator animator;
        CharacterController cc;

        float velocityH;
        float velocityV;
        Quaternion velocityRot;

        public bool isGrounded;
        public bool isInAir = false;
        LayerMask layerMask;

        Vector3 snapVelocity;
        Vector3 slideVelocity;
        Vector3 jumpVelocity;

        Vector3 animatorVel;
        Vector3 animatorVr;

        public Vector3 velocity;
        public Vector3 hitNormalsSum;
        public int hitsCount;
        public float snapRadius;
        public float groudRadius;
        public float stepOffset = 0.5f;
        public float slopeLimit = 35f;

        public RaycastHit[] hits;

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
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            coll = GetComponent<CapsuleCollider>();
        }

        void Start()
        {
            animator.SetBool("aimMovement", true);
            layerMask = LayerMask.GetMask("Environment");
        }

        void Update()
        {
        }

        void FixedUpdate()
        {
            CheckGround();
            // Snap();
            // ApplyGravity();

            rb.velocity = velocity;
        }

        public float checkDistance;

        List<Vector3> p1 = new List<Vector3>();
        List<Vector3> p2 = new List<Vector3>();

        public Vector3 gv;
        [Range(0f, 5f)] public float gvMax;

        void CheckGround()
        {
            if (isGrounded)
                checkDistance = stepOffset;
            else
                checkDistance = rb.velocity.y < 0f ? -rb.velocity.y * Time.fixedDeltaTime : 0f;

            isGrounded = false;

            RaycastHit hit;
            if (Physics.Raycast(rb.position + Vector3.up, Vector3.down, out hit, 1.0f + checkDistance, layerMask))
            {
                if (Vector3.Angle(hit.normal, Vector3.up) <= slopeLimit)
                    isGrounded = true;
            }

            float mag = float.MaxValue;
            gv = Vector3.zero;

            p1.Clear();
            p2.Clear();
            if (!isGrounded)
            {
                float maxAngle = 0f;
                Collider[] colliders = Physics.OverlapSphere(rb.position + Vector3.up * snapRadius, snapRadius, layerMask);
                for (int i = 0; i < colliders.Length; i++)
                {
                    for (int j = i + 1; j < colliders.Length; j++)
                    {
                        Vector3 v1 = colliders[i].ClosestPoint(rb.position + Vector3.up * snapRadius) - (rb.position + Vector3.up * snapRadius);
                        Vector3 v2 = colliders[j].ClosestPoint(rb.position + Vector3.up * snapRadius) - (rb.position + Vector3.up * snapRadius);
                        float angle = Vector3.Angle(v1, v2);
                        if (maxAngle < angle)
                            maxAngle = angle;
                    }
                }

                an = maxAngle;

                foreach (Collider collider in colliders)
                {
                    Vector3 point = collider.ClosestPoint(rb.position + Vector3.up * snapRadius);
                    p1.Add(point);
                    Vector3 p = rb.position + Vector3.up * snapRadius;
                    p.y = point.y;
                    p2.Add(p);

                    // Vector3 v1 = rb.position + Vector3.up * snapRadius - point;
                    // v1.y = 0f;
                    // v1.Normalize();

                    // gv += v1;
                }

                if (colliders.Length > 0)
                    mag = gv.magnitude;
            }

            if (p1.Count == 2 && p2.Count == 2)
            {
                an = Vector3.Angle(p1[0] - p2[0], p1[1] - p2[1]);
            }
            else
            {
                an = 0f;
            }

            RaycastHit[] hits = Physics.RaycastAll(rb.position + Vector3.up * snapRadius, Vector3.down, snapRadius, layerMask);
            Debug.Log(hits.Length);

            // if (!isGrounded && mag < gvMax)
            //     isGrounded = true;

            // if (isGrounded)
            // {
            //     groundHits.Clear();
            // }
            // else
            // {
            //     RaycastHit[] hits;
            //     hits = Physics.SphereCastAll(rb.position + Vector3.up, snapRadius, Vector3.down, 1.0f - snapRadius, layerMask);
            //     if (hits.Length == 0)
            //         return;

            //     groundHits.Clear();
            //     foreach (RaycastHit groundHit in hits)
            //     {
            //         groundHits.Add(groundHit);
            //     }
            // }
        }

        public bool isSliding = false;
        public float an;

        void Snap()
        {
            if (!isGrounded)
                return;

            RaycastHit[] hits;
            hits = Physics.SphereCastAll(rb.position + Vector3.up, snapRadius, Vector3.down, 2.0f, layerMask);
            if (hits.Length == 0)
                return;

            float targetY = float.MinValue;
            foreach(RaycastHit hit in hits)
            {
                string name = hit.collider.name;

                if (Mathf.Abs(rb.position.y - hit.point.y) > stepOffset)
                    continue;

                RaycastHit slopeHit;
                if (Physics.Raycast(hit.point + Vector3.up * 0.01f, Vector3.down, out slopeHit, 0.02f, layerMask))
                {
                    if (Vector3.Angle(slopeHit.normal, Vector3.up) > slopeLimit)
                        continue;
                }
                    
                float d = snapRadius - snapRadius * Mathf.Cos(Vector3.Angle(hit.normal, Vector3.up) * Mathf.Deg2Rad);
                float y = hit.point.y - d;

                if (targetY < y)
                    targetY = y;
            }

            if (targetY > float.MinValue)
            {
                Vector3 position = rb.position;
                position.y = targetY;
                rb.position = position;
            }
        }

        void ApplyGravity()
        {
            if (isGrounded)
                velocity.y = 0f;
            else
                velocity.y += Physics.gravity.y * Time.fixedDeltaTime;
        }

        void Slide()
        {
            bool isSliding = false;
            float slopeLimit = 45f;

            RaycastHit hit;
            if (Physics.Raycast(rb.position + Vector3.up * snapRadius, Vector3.down, out hit, snapRadius))
            {
                // if (Vector3.Angle(Vector3.up, hit.normal) > slopeLimit && Vector3.Angle(Vector3.up, hitNormal) > slopeLimit)
                //     isSliding = true;
            }
            else
            {
                if (Vector3.Angle(Vector3.up, hitNormalsSum) > slopeLimit)
                    isSliding = true;
            }

            if (isSliding)
            {
                // isGrounded = false;
            }
        }

        public float angle;
        public float dot;

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + Vector3.up * snapRadius, snapRadius);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * checkDistance);

            Gizmos.color = Color.magenta;

            if (p1.Count == p2.Count)
            {
                for (int i = 0; i < p1.Count; i++)
                {
                    Gizmos.DrawLine(p1[i], p2[i]);
                }
            }

            
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position + Vector3.up * snapRadius, transform.position + Vector3.up * snapRadius + gv);
        }

        void OnAnimatorMove()
        {
            velocity.x = animator.velocity.x;
            velocity.z = animator.velocity.z;
        }

        void OnControllerColliderHit(ControllerColliderHit hit)
        {

            

            // if (isSliding)
            // {
            //     float slideSpeed = 6f;
            //     float gravity = Physics.gravity.y;
            //     // velocity.y += gravity;
            //     // velocity.x += (1f - hitNormal.y) * hitNormal.x * slideSpeed;
            //     // velocity.z += (1f - hitNormal.y) * hitNormal.z * slideSpeed;
            //     slideVelocity.x = hitNormal.x;
            //     slideVelocity.z = hitNormal.z;
            // }
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
            }
        }
    }
}
