using System.Collections;
using System.Collections.Generic;
using ARPG.Gear;
using UnityEngine;

using ARPG.Core;
using ARPG.Movement;
using ARPG.Items;

namespace ARPG.Combat
{
    public class MeleeBehaviour : WeaponBehaviour
    {
        private bool isAttacking = false;

        public override bool AttackBegin()
        {
            if (isAttacking)
                return false;

            animator.SetTrigger("attack");
            isAttacking = true;

            targetMaskLayerWeight = 0f;

            return true;
        }

        public override bool AttackEnd()
        {
            return true;
        }

        public override void AttackComplete()
        {
            isAttacking = false;
            targetMaskLayerWeight = 1f;
        }

        public override bool DefenceBegin()
        {
            animator.SetBool("defence", true);

            return true;
        }

        public override bool DefenceEnd()
        {
            animator.SetBool("defence", false);

            return true;
        }

        void OnAttack()
        {
            isAttacking = false;

            // EquipmentSlot slot = GetComponent<Equipment>().GetEquipmentSlot(EquipmentSlot.SlotType.Weapon);
            // MeleeWeaponItem weaponItem = slot.item as MeleeWeaponItem;

            // GameObject collisionObject = new GameObject();
            // collisionObject.transform.position = transform.position;
            // SphereCollider collider = collisionObject.AddComponent<SphereCollider>();
            // collider.isTrigger = true;
            // collider.radius = weaponItem.range;
            // collider.center = Vector3.up * collider.radius * 0.5f;

            // MeleeCollider meleeCollider = collisionObject.AddComponent<MeleeCollider>();
            // meleeCollider.weaponEvent.AddListener(OnWeaponEvent);
        }

        void OnComplete()
        {
            isAttacking = false;

            MeleeWeaponItem weaponItem = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Weapon).item as MeleeWeaponItem;
            animator.SetLayerWeight(animator.GetLayerIndex(weaponItem.maskLayer), 0f);
        }

        void OnWeaponEvent(Transform target)
        {
            if (target.tag == tag)
                return;

            EquipmentSlot slot = GetComponent<Equipment>().GetEquipmentSlot(EquipmentSlot.SlotType.Weapon);
            MeleeWeaponItem weaponItem = slot.item as MeleeWeaponItem;
            float angle = Vector3.Angle(target.transform.position - transform.position, transform.forward);
            if (angle * 2f < weaponItem.angle)
                Debug.Log(target + " DAMAGE " + weaponItem.baseDamage);
        }
    }
}


