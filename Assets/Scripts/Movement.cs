using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSRPG
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : MonoBehaviour
    {
        [Range(0, 20f)] public float movementSpeed = 5f;
        public float rotationSpeed = 5f;
        public float fallingSpeed = 5f;
        public float minSpeedToLand = 5f;
        public float jumpingSpeed = 5f;
        public float climbingSpeed = 5f;

        public float groundRaycastOrigin = 0.1f;
        public float groundRaycastRadius = 0.1f;
        
        public Transform[] footTransforms;
        public float footRaycastOffset = 0f;

        private Vector3 groundNormal;

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

        // void Update()
        // {
        //     if (freezeDuration > 0f)
        //         freezeDuration -= Time.deltaTime;
        // }

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
                rb.AddForce(Vector3.down * fallingSpeed);
            }

            RaycastHit hit;
            if (RaycastGround(transform.position, groundRaycastOrigin, 0.01f, out hit))
            {
                groundNormal = hit.normal;

                List<float> results = new List<float>();
                results.Add(hit.point.y);
                // foreach (Transform footTransform in footTransforms)
                // {
                //     Vector3 footPosition = footTransform.position;
                //     footPosition.y = rb.position.y;
                //     if (RaycastGround(footPosition, groundRaycastOrigin, footRaycastOffset, out hit))
                //         results.Add(hit.point.y);
                // }


                float lvalue = anim.GetFloat("leftFoot");
                float rvalue = anim.GetFloat("rightFoot");

                if (lvalue > rvalue)
                {

                }


                Vector3 rbPosition = rb.position;

                float targetY;
                targetY = Mathf.Min(results.ToArray());
                    
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

        bool RaycastGround(Vector3 position, float originY, float offsetY, out RaycastHit hit)
        {
            return Physics.Raycast(position + Vector3.up * originY, Vector3.down, out hit, originY + offsetY);
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

            rb.velocity = Vector3.ProjectOnPlane(direction, groundNormal) * movementSpeed;

            anim.SetFloat("horizontal", direction.magnitude, 0.1f, Time.fixedDeltaTime);
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
                // footPosition.y = rb.position.y;
                // Gizmos.DrawLine(footPosition + Vector3.up * groundRaycastOrigin, footPosition + Vector3.down * footRaycastOffset);
            }

            Gizmos.color = Color.yellow;
            // Gizmos.DrawLine(leftLastIKPosition + Vector3.up * footIKRayOrigin, leftLastIKPosition + Vector3.down * footIKRayOffset);
            // Gizmos.DrawLine(rightLastIKPosition + Vector3.up * footIKRayOrigin, rightLastIKPosition + Vector3.down * footIKRayOffset);

            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(debug1, 0.05f);

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(debug2, 0.05f);
        }

        [Header("FootIK")]
        [Range(0f, 1f)][SerializeField] float footIKRayOrigin = 0.5f;
        [Range(0f, 1f)][SerializeField] float footIKRayOffset = 0.5f;
        [Range(0f, 100f)][SerializeField] float footIKPositioningSpeed = 0.5f;
        [Range(0f, 5f)][SerializeField] float footIKOffset = 0.5f;
        [SerializeField] LayerMask layerMask;

        Vector3 leftLastIKPosition, rightLastIKPosition;
        Vector3 leftLastBonePosition, rightLastBonePosition;
        Vector3 leftLastPrediction, rightLastPrediction;

        Vector3 debug1;
        Vector3 debug2;

        [Range(0f, 1f)] public float debugSpeed;
        [Range(0f, 1f)] public float predictionTime;
        [Range(0f, 1f)] public float predictionD;
        [Range(0f, 1f)] public float predictionOffset;
        [Range(0f, 5f)] public float radius;

        public float rby, hy;

        void OnAnimatorIK(int layerIndex)
        {
            anim.speed = debugSpeed;

            HandleIKFoot(AvatarIKGoal.RightFoot, AvatarIKHint.RightKnee, HumanBodyBones.RightFoot, ref rightLastIKPosition, ref rightLastBonePosition, ref rightLastPrediction);
            HandleIKFoot(AvatarIKGoal.LeftFoot, AvatarIKHint.LeftKnee, HumanBodyBones.LeftFoot, ref leftLastIKPosition, ref leftLastBonePosition, ref leftLastPrediction);
        }

        Vector3 last;

        void Update()
        {
            // leftLastPrediction = footTransforms[0].position;
            // leftLastPrediction += transform.forward * predictionD;
        }

        public AnimationCurve leftFoot;

        void HandleIKFoot(AvatarIKGoal foot, AvatarIKHint knee, HumanBodyBones bone, ref Vector3 lastIKPosition, ref Vector3 lastBonePosition, ref Vector3 lastPrediction)
        {
            anim.SetIKPositionWeight(foot, 1.0f);
            anim.SetIKRotationWeight(foot, 1.0f);
            anim.SetIKHintPositionWeight(knee, 1.0f);

            Vector3 footIKPosition = anim.GetIKPosition(foot);
            Vector3 kneeIKPosition = anim.GetIKHintPosition(knee);
            float kneeOffset = kneeIKPosition.y - footIKPosition.y;

            Vector3 currentBonePosition = anim.GetBoneTransform(bone).position;
            Vector3 delta = (currentBonePosition - lastBonePosition) / Time.deltaTime * predictionTime;
            // if (delta.magnitude > 0.1f)
            //     delta.Normalize();
            // delta.y = 0f;
            delta = transform.forward * predictionD;

            lastBonePosition = currentBonePosition;

            // lastPrediction = footTransforms[0].position;

            float y = footIKPosition.y;
            // Ray pray = new Ray(lastPrediction + Vector3.up * footIKRayOrigin, Vector3.down);
            // RaycastHit phit;
            // if (Physics.Raycast(pray, out phit, footIKRayOrigin + footIKRayOffset + predictionOffset, layerMask))
            // {
            //     y = phit.point.y + footIKOffset;
            // }

            Ray ray = new Ray(footIKPosition + Vector3.up * footIKRayOrigin, Vector3.down);
            RaycastHit hit;
            // if (Physics.SphereCast(ray, radius, out hit, footIKRayOrigin + footIKRayOffset, layerMask))
            if (Physics.Raycast(ray, out hit, footIKRayOrigin + footIKRayOffset, layerMask))
            {
                y = hit.point.y + footIKOffset;
                // anim.SetIKRotation(avatarIKGoal, Quaternion.FromToRotation(Vector3.up, hit.normal) * transform.rotation);
            }

            // ray = new Ray(lastPrediction + Vector3.up * footIKRayOrigin, Vector3.down);
            // if (Physics.Raycast(ray, out hit, footIKRayOrigin + footIKRayOffset, layerMask))
            // {
            //     if (hit.point.y + footIKOffset > footIKPosition.y)
            //     {
            //         y = hit.point.y + footIKOffset;
            //     }
            // }

            footIKPosition.y = y;

            footIKPosition.y = Mathf.Lerp(lastIKPosition.y, footIKPosition.y, footIKPositioningSpeed * Time.deltaTime);
            anim.SetIKPosition(foot, footIKPosition);
            lastIKPosition = footIKPosition;



            kneeIKPosition.y = footIKPosition.y + kneeOffset;
            anim.SetIKHintPosition(knee, kneeIKPosition);

            
            // lastPrediction = footTransforms[0].position;
        }
    }
}

