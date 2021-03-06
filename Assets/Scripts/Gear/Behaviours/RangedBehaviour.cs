﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Movement;
using ARPG.Gear;
using ARPG.Items;
using ARPG.Core;
using UnityEditor;

namespace ARPG.Gear
{
    public class RangedBehaviour : WeaponBehaviour
    {
        Quaternion rotation;
        Quaternion rotationSpeed;

        private bool isAttacking = false;

        protected override void OnAttackBegin()
        {
            Debug.Log("RANGED ATTACK BEGIN");
        }

        protected override void OnAttackEnd()
        {
            Debug.Log("RANGED ATTACK END");
        }

        protected override void OnAttackComplete()
        {
            Debug.Log("RANGED ATTACK COMPLETE");
        }

        // public override void AttackBegin()
        // {
        //     if (isAttacking)
        //         return;

        //     isAttacking = true;
        //     animator.SetBool("aim", true);
        //     animator.SetTrigger("rangedAttackBegin");
        //     animator.ResetTrigger("rangedAttackEnd");

        //     return;
        // }
        
        // public override void AttackEnd()
        // {
        //     if (!isAttacking)
        //         return;

        //     animator.SetTrigger("rangedAttackEnd");

        //     return;
        // }

        // public override void OnAttackComplete()
        // {
        //     isAttacking = false;
        //     animator.SetBool("aim", false);
        // }

        // public override void OnAnimationEvent(string animationEvent)
        // {
        //     if (animationEvent == "Launch")
        //         OnLaunch();
        //     else if (animationEvent == "GrabArrow")
        //         OnGrabArrow();
        // }

        // void OnLaunch()
        // {
        //     EquipmentArrowSlot arrowSlot = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Arrow) as EquipmentArrowSlot;
        //     EquipmentWeaponSlot weaponSlot = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Weapon) as EquipmentWeaponSlot;
        //     if (arrowSlot != null && weaponSlot != null)
        //     {
        //         ArrowItem arrowItem = arrowSlot.item as ArrowItem;
        //         Transform bow = weaponSlot.GetWeaponTransform();
        //         if (arrowItem != null && bow != null)
        //         {
        //             GameObject arrowObject = GameObject.Instantiate(arrowItem.arrowPrefab);
        //             arrowObject.transform.position = bow.position + arrowItem.launchOffset;

        //             Projectile arrow = arrowObject.GetComponent<Projectile>();
        //             arrow.speed = arrowItem.speed;
        //             arrow.damage = arrowItem.baseDamage + (weaponSlot.item as WeaponItem).baseDamage;
        //             arrow.owner = controller;

        //             Vector3 targetPosition = combat.targetPosition;
        //             Vector3 direction = targetPosition - arrow.transform.position;
        //             arrow.transform.rotation = Quaternion.LookRotation(direction);

        //             arrow.Launch();
        //         }

        //         arrowSlot.SetArrowActive(false);
        //     }
        // }

        // void OnGrabArrow()
        // {
        //     EquipmentArrowSlot arrowSlot = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Arrow) as EquipmentArrowSlot;
        //     if (arrowSlot != null)
        //     {
        //         arrowSlot.SetArrowActive(true);
        //     }
        // }

        // public override void OnAnimatorIK(int layer)
        // {
        //     if (layer != actionLayerIndex)
        //         return;

        //     Transform chestTransform = animator.GetBoneTransform(HumanBodyBones.Spine);
        //     Quaternion targetRotation = chestTransform.localRotation;

        //     if (isAttacking)
        //     {
        //         targetRotation = Quaternion.Inverse(chestTransform.parent.rotation) * combat.aimRotation;
        //         targetRotation *= Quaternion.AngleAxis(40f, Vector3.up);
        //     }

        //     rotation = Utils.QuaternionUtil.SmoothDamp(rotation, targetRotation, ref rotationSpeed, 0.1f);
        //     animator.SetBoneLocalRotation(HumanBodyBones.Spine, rotation);
        // }
    }
}


