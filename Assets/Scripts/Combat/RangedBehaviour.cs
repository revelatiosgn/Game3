using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Movement;
using ARPG.Gear;
using ARPG.Items;
using ARPG.Core;
using UnityEditor;

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
        PlayerCombat playerCombat;

        State state = State.None;
        Quaternion rotation;
        Quaternion rotationSpeed;
        bool attackTrigger = false;

        protected override void Awake()
        {
            base.Awake();

            movement = GetComponent<PlayerMovement>();
            playerCombat = GetComponent<PlayerCombat>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            RangedWeaponItem weaponItem = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Weapon).item as RangedWeaponItem;
            animator.SetLayerWeight(animator.GetLayerIndex(weaponItem.actionLayer), 1f);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            RangedWeaponItem weaponItem = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Weapon).item as RangedWeaponItem;
            animator.SetLayerWeight(animator.GetLayerIndex(weaponItem.actionLayer), 0f);
        }

        public override bool AttackBegin()
        {
            if (state != State.None)
                return false;

            state = State.Start;
            attackTrigger = false;

            animator.SetBool("rangedAim", true);
            animator.SetTrigger("rangedAttackBegin");

            movement.State = PlayerMovement.MovementState.Aim;

            RangedWeaponItem weaponItem = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Weapon).item as RangedWeaponItem;
            animator.SetLayerWeight(animator.GetLayerIndex(weaponItem.maskLayer), 0f);

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
                ArrowItem arrowItem = arrowSlot.item as ArrowItem;
                Transform bow = weaponSlot.GetWeaponTransform();
                if (arrowItem != null && bow != null)
                {
                    GameObject arrowObject = Instantiate(arrowItem.arrowPrefab);
                    arrowObject.transform.position = bow.position + arrowItem.launchOffset;
                    arrowObject.transform.rotation = Camera.main.transform.rotation;

                    Arrow arrow = arrowObject.GetComponent<Arrow>();
                    arrow.speed = arrowItem.speed;
                    arrow.gravity = arrowItem.gravity;

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
            animator.SetBool("rangedAim", false);

            RangedWeaponItem weaponItem = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Weapon).item as RangedWeaponItem;
            animator.SetLayerWeight(animator.GetLayerIndex(weaponItem.maskLayer), 1f);
        }

        void OnAnimatorIK(int layer)
        {
            Transform chestTransform = animator.GetBoneTransform(HumanBodyBones.Spine);
            Quaternion targetRotation = chestTransform.localRotation;

            if (state != State.None)
            {
                targetRotation = Quaternion.Inverse(chestTransform.parent.rotation) * Camera.main.transform.rotation;
                targetRotation *= Quaternion.AngleAxis(-90f, Vector3.forward);
                animator.SetBoneLocalRotation(HumanBodyBones.UpperChest, targetRotation);
            }

            rotation = Utils.QuaternionUtil.SmoothDamp(rotation, targetRotation, ref rotationSpeed, 0.1f);
            animator.SetBoneLocalRotation(HumanBodyBones.UpperChest, rotation);
        }
    }
}


