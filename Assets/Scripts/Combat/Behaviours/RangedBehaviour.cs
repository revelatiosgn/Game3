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
        Quaternion rotation;
        Quaternion rotationSpeed;

        private bool isAttacking = false;

        public RangedBehaviour(BaseCombat combat) : base(combat)
        {
        }
        
        public override bool DefenceBegin()
        {
            return false;
        }

        public override bool DefenceEnd()
        {
            return false;
        }

        public override bool AttackBegin()
        {
            if (isAttacking)
                return false;

            isAttacking = true;
            animator.SetBool("aim", true);
            animator.SetTrigger("rangedAttackBegin");
            animator.ResetTrigger("rangedAttackEnd");

            animator.SetLayerWeight(maskLayerIndex, 0f);

            return true;
        }
        
        public override bool AttackEnd()
        {
            if (!isAttacking)
                return false;

            animator.SetTrigger("rangedAttackEnd");

            return true;
        }

        public override void OnAttackComplete()
        {
            animator.SetLayerWeight(maskLayerIndex, 1f);

            isAttacking = false;
            animator.SetBool("aim", false);
        }

        public override void OnAnimationEvent(string animationEvent)
        {
            if (animationEvent == "Launch")
                OnLaunch();
        }

        void OnLaunch()
        {
            EquipmentArrowSlot arrowSlot = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Arrow) as EquipmentArrowSlot;
            EquipmentWeaponSlot weaponSlot = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Weapon) as EquipmentWeaponSlot;
            if (arrowSlot != null && weaponSlot != null)
            {
                ArrowItem arrowItem = arrowSlot.item as ArrowItem;
                Transform bow = weaponSlot.GetWeaponTransform();
                if (arrowItem != null && bow != null)
                {
                    GameObject arrowObject = GameObject.Instantiate(arrowItem.arrowPrefab);
                    arrowObject.transform.position = bow.position + arrowItem.launchOffset;

                    Projectile arrow = arrowObject.GetComponent<Projectile>();
                    arrow.speed = arrowItem.speed;
                    arrow.damage = arrowItem.baseDamage + (weaponSlot.item as WeaponItem).baseDamage;
                    arrow.owner = controller;

                    Vector3 targetPosition = combat.targetPosition;
                    Vector3 direction = targetPosition - arrow.transform.position;
                    arrow.transform.rotation = Quaternion.LookRotation(direction);
                }
            }
        }

        public override void OnAnimatorIK(int layer)
        {
            Transform chestTransform = animator.GetBoneTransform(HumanBodyBones.Spine);
            Quaternion targetRotation = chestTransform.localRotation;

            if (isAttacking)
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


