using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;
using ARPG.Inventory;

using ARPG.Movement;

namespace ARPG.Combat
{
    public class RangedBehaviour : WeaponBehaviour
    {
        private enum State
        {
            None,
            Start,
            Aim,
        }

        PlayerMovement movement;
        State state = State.None;
        Quaternion rotation;
        Quaternion rotationSpeed;

        void Start()
        {
            movement = GetComponent<PlayerMovement>();
        }

        void Update()
        {
        }

        public override bool AttackBegin()
        {
            state = State.Start;
            animator.Play("RangedAttackBegin");

            movement.State = PlayerMovement.MovementState.Aim;

            return true;
        }
        
        public override void AttackEnd()
        {
            if (state == State.Aim)
                animator.Play("RangedAttackEnd");
            state = State.Aim;
        }

        public void OnAim()
        {
            if (state == State.Aim)
                animator.Play("RangedAttackEnd");
            state = State.Aim;
        }

        public void OnLaunch()
        {
        }

        public void OnEnd()
        {
            state = State.None;
            movement.State = PlayerMovement.MovementState.Regular;
        }

        public void OnAnimatorIK(int layer)
        {
            Transform chestTransform = animator.GetBoneTransform(HumanBodyBones.UpperChest);
            Quaternion targetRotation = chestTransform.localRotation;

            if (state == State.Start || state == State.Aim)
            {
                
                targetRotation = Quaternion.Inverse(chestTransform.parent.rotation) * Camera.main.transform.rotation;
                targetRotation *= Quaternion.AngleAxis(90f, Vector3.up);
                animator.SetBoneLocalRotation(HumanBodyBones.UpperChest, targetRotation);
                
            }

            // rotation = Utils.QuaternionUtil.SmoothDamp(rotation, targetRotation, ref rotationSpeed, 0.1f);
            // animator.SetBoneLocalRotation(HumanBodyBones.UpperChest, rotation);
        }
    }
}


