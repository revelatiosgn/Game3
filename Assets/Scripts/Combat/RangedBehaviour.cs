using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Movement;
using ARPG.Gear;
using ARPG.Items;
using ARPG.Core;

namespace ARPG.Combat
{
    public class RangedBehaviour : WeaponBehaviour
    {
        private enum State
        {
            None,
            Start,
            Aim,
            Launch
        }

        PlayerMovement movement;
        State state = State.None;
        Quaternion rotation;
        Quaternion rotationSpeed;
        bool attackTrigger = false;

        protected override void Awake()
        {
            base.Awake();
            movement = GetComponent<PlayerMovement>();
        }

        void Update()
        {
            
        }

        public override bool AttackBegin()
        {
            if (state != State.None)
                return false;

            state = State.Start;
            attackTrigger = false;

            animator.SetBool("aim", true);
            animator.SetTrigger("rangedAttackBegin");

            movement.State = PlayerMovement.MovementState.Aim;
            CameraFollow.SetState(CameraFollow.CameraState.Aiming);

            return true;
        }
        
        public override void AttackEnd()
        {
            if (state == State.Start || state == State.Aim)
            {
                attackTrigger = true;
                TryAttack();
            }
        }

        public override bool DefenceBegin()
        {
            return false;
        }

        public override void DefenceEnd()
        {
        }

        void TryAttack()
        {
            if (state == State.Aim && attackTrigger)
                animator.SetTrigger("rangedAttackEnd");
        }

        void OnAim()
        {
            state = State.Aim;
            TryAttack();
        }

        void OnLaunch()
        {
            state = State.Launch;

            EquipmentArrowSlot arrowSlot = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Arrow) as EquipmentArrowSlot;
            EquipmentWeaponSlot weaponSlot = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Weapon) as EquipmentWeaponSlot;
            if (arrowSlot != null && weaponSlot != null)
            {
                ArrowItem arrowItem = arrowSlot.Item as ArrowItem;
                GameObject bow = weaponSlot.currentWeapon;
                if (arrowItem != null && bow != null)
                {
                    ArrowStatement statement = arrowItem.GetStatement();
                    GameObject arrowObject = Instantiate(statement.arrowPrefab);
                    arrowObject.transform.position = bow.transform.position + statement.launchOffset;
                    arrowObject.transform.rotation = Camera.main.transform.rotation;

                    Arrow arrow = arrowObject.GetComponent<Arrow>();
                    arrow.speed = statement.speed;
                    arrow.gravity = statement.gravity;

                    RaycastHit hit;
                    if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.rotation * Vector3.forward, out hit))
                    {
                        Vector3 direction = hit.point - arrow.transform.position;
                        arrow.transform.rotation = Quaternion.LookRotation(direction);
                    }
                }
            }
        }

        void OnEnd()
        {
            state = State.None;
            movement.State = PlayerMovement.MovementState.Regular;
            CameraFollow.SetState(CameraFollow.CameraState.Regular);
            animator.SetBool("aim", false);
            playerController.isInteracting = false;
        }

        void OnAnimatorIK(int layer)
        {
            Transform chestTransform = animator.GetBoneTransform(HumanBodyBones.Spine);
            Quaternion targetRotation = chestTransform.localRotation;

            if (state != State.None)
            {
                targetRotation = Quaternion.Inverse(chestTransform.parent.rotation) * Camera.main.transform.rotation;
                targetRotation *= Quaternion.AngleAxis(90f, Vector3.up);
                animator.SetBoneLocalRotation(HumanBodyBones.UpperChest, targetRotation);
            }

            rotation = Utils.QuaternionUtil.SmoothDamp(rotation, targetRotation, ref rotationSpeed, 0.1f);
            animator.SetBoneLocalRotation(HumanBodyBones.UpperChest, rotation);
        }
    }
}


