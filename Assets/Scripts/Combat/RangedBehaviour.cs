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

        BaseCombat combat;

        State state = State.None;
        Quaternion rotation;
        Quaternion rotationSpeed;
        bool attackTrigger = false;

        protected override void Awake()
        {
            base.Awake();

            combat = GetComponent<BaseCombat>();
        }

        public override bool AttackBegin()
        {
            if (state != State.None)
                return false;

            state = State.Start;
            attackTrigger = false;

            animator.SetBool("rangedAim", true);
            animator.SetTrigger("rangedAttackBegin");

            targetMaskLayerWeight = 0f;

            return true;
        }

        public override void AttackComplete()
        {
        }
        
        public override bool AttackEnd()
        {
            if (state == State.Start || state == State.Aim)
            {
                attackTrigger = true;
                TryAttack();
            }

            return true;
        }

        public override bool DefenceBegin()
        {
            return false;
        }

        public override bool DefenceEnd()
        {
            return false;
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

                    Arrow arrow = arrowObject.GetComponent<Arrow>();
                    arrow.speed = arrowItem.speed;

                    Vector3 targetPosition = combat.targetPosition;
                    Vector3 direction = targetPosition - arrow.transform.position;
                    arrow.transform.rotation = Quaternion.LookRotation(direction);
                }
            }
        }

        void OnEnd()
        {
            state = State.None;
            animator.SetBool("rangedAim", false);
            targetMaskLayerWeight = 1f;
        }

        void OnAnimatorIK(int layer)
        {
            Transform chestTransform = animator.GetBoneTransform(HumanBodyBones.Spine);
            Quaternion targetRotation = chestTransform.localRotation;

            if (state != State.None)
            {
                targetRotation = Quaternion.Inverse(chestTransform.parent.rotation) * combat.aimRotation;
                targetRotation *= Quaternion.AngleAxis(-90f, Vector3.forward);
                animator.SetBoneLocalRotation(HumanBodyBones.UpperChest, targetRotation);
            }

            rotation = Utils.QuaternionUtil.SmoothDamp(rotation, targetRotation, ref rotationSpeed, 0.1f);
            animator.SetBoneLocalRotation(HumanBodyBones.UpperChest, rotation);
        }
    }
}


