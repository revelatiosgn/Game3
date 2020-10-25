using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSRPG
{
    public class FootIK : MonoBehaviour
    {
        [Range(0f, 1f)] public float raycastOrigin = 0.5f;
        [Range(0f, 1f)] public float raycastOffset = 0.5f;
        [Range(0f, 1f)] public float groundIKOffset = 0.05f;

        Animator anim;

        // Vector3 leftFootPosition, rightFootPosition;

        void Awake()
        {
            anim = GetComponent<Animator>();
        }

        void Update()
        {

        }

        // void OnAnimatorIK(int layerIndex)
        // {
        //     // Vector3 bodyPos = anim.bodyPosition;
        //     // bodyPos.y += 0.5f;
        //     // anim.bodyPosition = bodyPos;

        //     anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, anim.GetFloat("leftFoot"));
        //     anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, anim.GetFloat("rightFoot"));

        //     anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, anim.GetFloat("leftFoot"));
        //     anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, anim.GetFloat("rightFoot"));

        //     leftFootPosition = anim.GetBoneTransform(HumanBodyBones.LeftFoot).position;
        //     rightFootPosition = anim.GetBoneTransform(HumanBodyBones.RightFoot).position;

        //     AlignFoot(AvatarIKGoal.LeftFoot, leftFootPosition);
        //     AlignFoot(AvatarIKGoal.RightFoot, rightFootPosition);
        // }

        // void AlignFoot(AvatarIKGoal footIKGoal, Vector3 footPosition)
        // {
        //     Ray ray = new Ray(footPosition + Vector3.up * raycastOrigin, Vector3.down);
        //     RaycastHit hit;
        //     if (Physics.Raycast(ray, out hit, raycastOrigin + raycastOffset))
        //     {
        //         Vector3 footIKPosition = anim.GetIKPosition(footIKGoal);
        //         footIKPosition.y = hit.point.y + groundIKOffset;
        //         anim.SetIKPosition(footIKGoal, footIKPosition);

        //         anim.SetIKRotation(footIKGoal, Quaternion.FromToRotation(Vector3.up, hit.normal) * transform.rotation);
        //     }
        // }


        /////

        [Header("Feet Grounder")]
        public bool enableFeetIk = true;
        [Range(0f, 2f)][SerializeField] private float heightFromGroundRaycast = 1.14f;
        [Range(0f, 1f)][SerializeField] private float raycastDownDistance = 1.5f;
        [SerializeField] private LayerMask environmentLayer;
        [SerializeField] private float pelvisOffset = 0f;
        [Range(0f, 1f)][SerializeField] private float pelvisUpAndDownSpeed = 0.28f;
        [Range(0f, 1f)][SerializeField] private float feetToIkPositionSpeed = 0.5f;

        public string leftFootAnimVariableName = "LeftFootCurve";
        public string rightFootAnimVariableName = "RightFootCurve";

        public bool useProIkFeature = false;
        public bool showSolverDebug = true;

        private Vector3 rightFootPosition, leftFootPosition, leftFootIkPosition, rightFootIkPosition;
        private Quaternion leftFootIkRotation, rightFootIkRotation;
        private float lastPelvisPositionY, lastRightFootPositionY, lastLeftFootPositionY;

        void FixedUpdate()
        {
            GroundedFixedUpdate();
        }

        void GroundedFixedUpdate()
        {
            if (enableFeetIk == false)
                return;
            if (anim == null)
                return;

            AdjustFeetTarget(ref rightFootPosition, HumanBodyBones.RightFoot);
            AdjustFeetTarget(ref leftFootPosition, HumanBodyBones.LeftFoot);

            FeetPositionSolver(rightFootPosition, ref rightFootIkPosition, ref rightFootIkRotation);
            FeetPositionSolver(leftFootPosition, ref leftFootIkPosition, ref leftFootIkRotation);
        }

        void OnAnimatorIK(int layerIndex)
        {
            if (enableFeetIk == false)
                return;
            if (anim == null)
                return;

            MovePelvisHeight();

            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
            if (useProIkFeature)
            {
                anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, anim.GetFloat(rightFootAnimVariableName));
            }
            MoveFeetToIkPoint(AvatarIKGoal.RightFoot, rightFootIkPosition, rightFootIkRotation, ref lastRightFootPositionY);

            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
            if (useProIkFeature)
            {
                anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, anim.GetFloat(leftFootAnimVariableName));
            }
            MoveFeetToIkPoint(AvatarIKGoal.LeftFoot, leftFootIkPosition, leftFootIkRotation, ref lastLeftFootPositionY);
        }

        void MoveFeetToIkPoint(AvatarIKGoal foot, Vector3 positionIkHolder, Quaternion rotationIkHolder, ref float lastFootPositionY)
        {
            Vector3 targetIkPosition = anim.GetIKPosition(foot);

            if (positionIkHolder != Vector3.zero)
            {
                targetIkPosition = transform.InverseTransformPoint(targetIkPosition);
                positionIkHolder = transform.InverseTransformPoint(positionIkHolder);

                float yVariable = Mathf.Lerp(lastFootPositionY, positionIkHolder.y, feetToIkPositionSpeed);
                targetIkPosition.y += yVariable;

                lastFootPositionY = yVariable;

                targetIkPosition = transform.TransformPoint(targetIkPosition);

                anim.SetIKRotation(foot, rotationIkHolder);
            }

            anim.SetIKPosition(foot, targetIkPosition);
        }

        void MovePelvisHeight()
        {
            if (rightFootIkPosition == Vector3.zero || leftFootIkPosition == Vector3.zero || lastPelvisPositionY == 0)
            {
                lastPelvisPositionY = anim.bodyPosition.y;
                return;
            }

            float lOffsetPosition = leftFootIkPosition.y - transform.position.y;
            float rOffsetPosition = rightFootIkPosition.y - transform.position.y;

            float totalOffset = (lOffsetPosition < rOffsetPosition) ? lOffsetPosition : rOffsetPosition;

            Vector3 newPelvisPosition = anim.bodyPosition + Vector3.up * totalOffset;

            newPelvisPosition.y = Mathf.Lerp(lastPelvisPositionY, newPelvisPosition.y, pelvisUpAndDownSpeed);

            anim.bodyPosition = newPelvisPosition;

            lastPelvisPositionY = anim.bodyPosition.y;
        }

        void FeetPositionSolver(Vector3 fromSkyPosition, ref Vector3 feetIkPositions, ref Quaternion feetIkRotations)
        {
            RaycastHit feetOutHit;

            if (showSolverDebug)
                Debug.DrawLine(fromSkyPosition, fromSkyPosition + Vector3.down * (raycastDownDistance + heightFromGroundRaycast), Color.yellow);

            if (Physics.Raycast(fromSkyPosition, Vector3.down, out feetOutHit, raycastDownDistance + heightFromGroundRaycast, environmentLayer))
            {
                feetIkPositions = fromSkyPosition;
                feetIkPositions.y = feetOutHit.point.y + pelvisOffset;
                feetIkRotations = Quaternion.FromToRotation(Vector3.up, feetOutHit.normal) * transform.rotation;

                return;
            }

            feetIkPositions = Vector3.zero;
        }

        void AdjustFeetTarget(ref Vector3 feetPositions, HumanBodyBones foot)
        {
            feetPositions = anim.GetBoneTransform(foot).position;
            feetPositions.y = transform.position.y + heightFromGroundRaycast;
        }
    }
}

