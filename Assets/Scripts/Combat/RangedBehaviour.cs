using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Movement;
using ARPG.Gear;
using ARPG.Items;

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

        public void OnEnd()
        {
            state = State.None;
            movement.State = PlayerMovement.MovementState.Regular;
        }

        public void OnAnimatorIK(int layer)
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


