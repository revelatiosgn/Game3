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
        public float climbingSpeed = 5f;

        public float groundRaycastOrigin = 0.1f;

        public Transform[] footTransforms;
        public float footRaycastOffset = 0f;

        private Rigidbody rb;
        private Animator anim;

        public bool isOnGround = true;
        public bool isFalling = false;

        private float freezeDuration = 0f;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
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

            // updateRB();
        }

        void HandleVerticalMovement()
        {
            if (!isOnGround)
            {
                rb.AddForce(Vector3.down * 100f);
            }

            Vector3 groundPosition = Vector3.zero;
            if (RaycastGround(transform.position, groundRaycastOrigin, 0.01f, ref groundPosition))
            {
                List<float> results = new List<float>();
                results.Add(groundPosition.y);
                foreach (Transform footTransform in footTransforms)
                {
                    Vector3 footPosition = footTransform.position;
                    footPosition.y = rb.position.y;
                    if (RaycastGround(footPosition, groundRaycastOrigin, footRaycastOffset, ref groundPosition))
                        results.Add(groundPosition.y);
                }

                Vector3 rbPosition = rb.position;
                float targetY = Mathf.Min(results.ToArray());
                rbPosition.y = Mathf.Lerp(rb.position.y, targetY, fallingSpeed * Time.deltaTime);
                rb.position = rbPosition;

                isOnGround = true;
            }
            else
            {
                isOnGround = false;
            }
        }

        void FreezeMovement(float duration)
        {
            freezeDuration = duration;
        }

        bool RaycastGround(Vector3 position, float originY, float offsetY, ref Vector3 result)
        {
            RaycastHit hit;
            if (Physics.Raycast(position + Vector3.up * originY, Vector3.down, out hit, originY + offsetY))
            {
                result = hit.point;
                return true;
            }

            return false;
        }

        void HandleHorizontalMovement()
        {
            // if (!isOnGround)
            //     return;

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

            anim.SetFloat("vertical", direction.magnitude, 0.1f, Time.fixedDeltaTime);
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
            Gizmos.DrawLine(transform.position + Vector3.up * groundRaycastOrigin, transform.position);

            Gizmos.color = Color.blue;
            foreach (Transform footTransform in footTransforms)
            {
                Vector3 footPosition = footTransform.position;
                footPosition.y = rb.position.y;
                Gizmos.DrawLine(footPosition + Vector3.up * groundRaycastOrigin, footPosition + Vector3.down * footRaycastOffset);
            }

            Gizmos.color = Color.yellow;
            // Gizmos.DrawLine(leftLastFootPosition + Vector3.up * footIKRayOrigin, leftLastFootPosition + Vector3.down * footIKRayOffset);
            // Gizmos.DrawLine(rightLastFootPosition + Vector3.up * footIKRayOrigin, rightLastFootPosition + Vector3.down * footIKRayOffset);

            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(debug, 0.1f);
        }

        [Header("FootIK")]
        [Range(0f, 1f)][SerializeField] float footIKRayOrigin = 0.5f;
        [Range(0f, 1f)][SerializeField] float footIKRayOffset = 0.5f;
        [Range(0f, 100f)][SerializeField] float footIKPositioningSpeed = 0.5f;
        [Range(0f, 5f)][SerializeField] float footIKOffset = 0.5f;
        [SerializeField] LayerMask layerMask;

        Vector3 leftLastFootPosition, rightLastFootPosition;
        Vector3 leftFootPosition, rightFootPosition;

        Vector3 debug;
        [Range(0f, 1f)] public float debugSpeed;

        public float rby, hy;

        void OnAnimatorIK(int layerIndex)
        {
            anim.speed = debugSpeed;

            HandleIKFoot(AvatarIKGoal.LeftFoot, AvatarIKHint.LeftKnee, ref leftLastFootPosition);
            HandleIKFoot(AvatarIKGoal.RightFoot, AvatarIKHint.RightKnee, ref rightLastFootPosition);

            // HandleGrounded();

            // Vector3 bodyPositision = anim.bodyPosition;
            // bodyPositision.y = 0.5f;
            // debug = bodyPositision;
            // Debug.Log(bodyPositision.y);

            // anim.bodyPosition = bodyPositision;
        }

        void HandleIKFoot(AvatarIKGoal avatarIKGoal, AvatarIKHint avatarIKHint, ref Vector3 lastFootIKPosition)
        {
            anim.SetIKPositionWeight(avatarIKGoal, 1.0f);
            anim.SetIKRotationWeight(avatarIKGoal, 1.0f);
            anim.SetIKHintPositionWeight(avatarIKHint, 1.0f);

            Vector3 footIKPosition = anim.GetIKPosition(avatarIKGoal);
            Vector3 kneeIKPosition = anim.GetIKHintPosition(avatarIKHint);

            float kneeOffset = kneeIKPosition.y - footIKPosition.y;
            kneeIKPosition.y += kneeOffset;
            anim.SetIKHintPosition(avatarIKHint, kneeIKPosition);
            debug = kneeIKPosition;

            Ray ray = new Ray(footIKPosition + Vector3.up * footIKRayOrigin, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, footIKRayOrigin + footIKRayOffset, layerMask))
            {
                footIKPosition.y = hit.point.y + footIKOffset;
                anim.SetIKRotation(avatarIKGoal, Quaternion.FromToRotation(Vector3.up, hit.normal) * transform.rotation);
            }

            footIKPosition.y = Mathf.Lerp(lastFootIKPosition.y, footIKPosition.y, footIKPositioningSpeed * Time.deltaTime);
            anim.SetIKPosition(avatarIKGoal, footIKPosition);
            lastFootIKPosition = footIKPosition;
        }
    }
}

