using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Gear;
using ARPG.Combat;

namespace ARPG.Items
{
    public class WeaponItem : EquipmentItem
    {
        // Item

        public override Sprite GetIcon()
        {
            return GetStatement().icon;
        }

        public override void OnUse()
        {
            Transform target = GameObject.FindGameObjectWithTag(Constants.Tags.Player).transform;
            Equipment equipment = target.GetComponent<Equipment>();
            if (equipment.IsEquipped(this))
                equipment.UnEquip(this);
            else
                equipment.Equip(this);
        }

        public override bool IsEquals(Item other)
        {
            WeaponItem weaponItem = other as WeaponItem;
            if (weaponItem != null && weaponItem.GetStatement() == GetStatement())
                return true;
            
            return false;
        }

        // EquipmentItem

        public override EquipmentSlot.SlotType GetSlotType()
        {
            return EquipmentSlot.SlotType.Weapon;
        }

        public override void OnEquip(EquipmentSlot slot)
        {
            Transform target = GameObject.FindGameObjectWithTag(Constants.Tags.Player).transform;
            Animator animator = target.GetComponent<Animator>();
            animator.runtimeAnimatorController = GetStatement().animatorContoller;
            animator.SetLayerWeight(animator.GetLayerIndex(GetStatement().layerName), 1f);

            WeaponBehaviour weaponBehaviour = target.GetComponent<WeaponBehaviour>();
            if (weaponBehaviour != null)
                GameObject.Destroy(weaponBehaviour);
            AddBehaviour(target);
        }

        public override void OnUnequip(EquipmentSlot slot)
        {
            Transform target = GameObject.FindGameObjectWithTag(Constants.Tags.Player).transform;
            Animator animator = target.GetComponent<Animator>();
            animator.SetLayerWeight(animator.GetLayerIndex(GetStatement().layerName), 0f);

            WeaponBehaviour weaponBehaviour = target.GetComponent<WeaponBehaviour>();
            if (weaponBehaviour != null)
                GameObject.Destroy(weaponBehaviour);
        }

        // Self

        public virtual WeaponStatement GetStatement() { return null; }
        protected virtual void AddBehaviour(Transform target) { }
    }
}


