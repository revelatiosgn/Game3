using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;
using ARPG.Inventory;

namespace ARPG.Combat
{
    public class RangedBehaviour : WeaponBehaviour
    {
        bool isAttackEneded;
        bool isReadyToLaunch;
        GameObject arrowPrefab;

        public override bool AttackBegin()
        {
            Equipment equipment = GetComponent<Equipment>();
            EquipmentSlot arrowsSlot = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Arrows);

            if (arrowsSlot.Item == null)
                return false;

            ArrowProperty arrowProperty = arrowsSlot.Item.property as ArrowProperty;
            arrowPrefab = arrowProperty.arrowPrefab;

            ItemsContainer inventory = GetComponent<ItemsContainer>();
            if (!inventory.RemoveItem(arrowsSlot.Item))
                return false;

            isAttackEneded = false;
            isReadyToLaunch = false;

            animator.Play("RangedAttackGetMissile");

            return true;
        }
        
        public override void AttackEnd()
        {
            isAttackEneded = true;
            if (isAttackEneded && isReadyToLaunch)
                animator.Play("RangedAttackEnd");
        }

        public void ReadyToLaunch()
        {
            isReadyToLaunch = true;
            if (isAttackEneded && isReadyToLaunch)
                animator.Play("RangedAttackEnd");
        }

        public void LaunchMissile()
        {
            Equipment equipment = GetComponent<Equipment>();
            EquipmentSlot weaponSlot = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Weapon);
            foreach (WeaponHolder holder in weaponSlot.holders)
            {
                if (holder.hand == WeaponHolder.Hand.Left)
                {
                    GameObject arrow = Instantiate(arrowPrefab);
                    arrow.transform.position = holder.transform.position;
                    arrow.transform.rotation = transform.rotation;
                }
            }
        }
    }
}


