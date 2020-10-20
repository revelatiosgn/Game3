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
        public float fallingSpeed = 5f;
        public float minSpeedToLand = 5f;
        public float jumpingSpeed = 5f;

        public float groundRaycastOrigin = 0.1f;
        public float groundRaycastOffset = 0.2f;

        private Rigidbody rb;
        private Animator animator;

        public bool isOnGround = false;
        public bool isFalling = false;

        private float freezeDuration = 0f;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (freezeDuration > 0f)
                freezeDuration -= Time.deltaTime;
        }

        void FixedUpdate()
        {
            HandleVerticalMovement();
            HandleHorizontalMovement();
            HandleRotation();
        }

        void HandleVerticalMovement()
        {
            if (InputHandler.jumpInput)
            {
                rb.velocity =   new Vector3(rb.velocity.x, jumpingSpeed, rb.velocity.z);
            }

            RaycastHit hitInfo;
            if (rb.velocity.y <= 0f && RaycastGround(out hitInfo))
            {
                if (isFalling)
                {
                    if (Mathf.Abs(rb.velocity.y) > minSpeedToLand)
                    {
                        animator.CrossFade("Landing", 0.2f);
                        FreezeMovement(0.3f);
                    }
                    else
                    {
                        animator.CrossFade("Moving", 0.2f);
                    }
                }

                isOnGround = true;
                isFalling = false;

                Vector3 pos = rb.position;
                pos.y = hitInfo.point.y;
                // rb.MovePosition(pos);
            }
            else
            {
                if (isOnGround)
                {
                    animator.CrossFade("Falling", 0.5f);
                }

                isOnGround = false;
                isFalling = true;
                rb.AddForce(Vector3.down * fallingSpeed);
            }
        }

        void FreezeMovement(float duration)
        {
            freezeDuration = duration;
        }

        bool RaycastGround(out RaycastHit hitInfo)
        {
            return Physics.Raycast(transform.position + Vector3.up * groundRaycastOrigin, Vector3.down, out hitInfo, groundRaycastOrigin + groundRaycastOffset);
        }

        void HandleHorizontalMovement()
        {
            if (!isOnGround)
                return;

            Vector3 direction = Vector3.zero;
            direction += Camera.main.transform.right * InputHandler.movementInput.x;
            direction += Camera.main.transform.forward * InputHandler.movementInput.y;
            direction.y = 0f;
            direction.Normalize();

            if (freezeDuration > 0f)
                direction = Vector3.zero;

            Vector3 movementVelocity = direction * movementSpeed;
            var velocity = new Vector3(movementVelocity.x, 0f, movementVelocity.z);
            rb.velocity = velocity;

            animator.SetFloat("vertical", direction.magnitude, 0.1f, Time.fixedDeltaTime);
        }

        void HandleRotation()
        {
            Vector3 direction = rb.velocity.normalized;
            direction.y = 0f;

            if (direction == Vector3.zero)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + Vector3.up * groundRaycastOrigin, transform.position + Vector3.down * groundRaycastOffset);
        }
    
        public float footDist = 0f;
        public int mask = 0;

        void OnAnimatorIK(int layerIndex) 
        {
            // animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1.0f);
            // animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1.0f);
            // Vector3 pos = animator.GetIKPosition(AvatarIKGoal.LeftFoot);

            // RaycastHit hit;
            // Ray ray = new Ray(pos + Vector3.up, Vector3.down);
            // if (Physics.Raycast(ray, out hit, footDist))
            // {
            //     pos.y = hit.point.y - footDist + 1.0f;
            //     // animator.SetIKPosition(AvatarIKGoal.LeftFoot, pos);
            // }
        }

        

        void RayCastLegs()
        {
            // Vector3 footPos = animator.GetIKPosition(AvatarIKGoal.LeftFoot);
            // RaycastHit hit;
            // Ray ray = new Ray(pos + Vector3.up, Vector3.down);
        }
    }
}

