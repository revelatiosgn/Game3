using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Equipment;

namespace ARPG.Items
{
    public class MeleeWeaponItem : EquipmentItem, IInventoryItem, IEquipmentItem
    {
        public MeleeWeaponStatement statement;

        public Sprite GetIcon()
        {
            return statement.icon;
        }

        public void OnUse(Transform transform)
        {
            transform = GameObject.FindGameObjectWithTag(Constants.Tags.Player).transform;

            Equipments equipments = transform.GetComponent<Equipments>();
            if (equipments.GetEquipmentSlot(this) == null)
            {
                equipments.Equip(this);
                transform.GetComponent<Animator>().runtimeAnimatorController = statement.animatorContoller;
            }
            else
            {
                equipments.UnEquip(EquipmentSlot.SlotType.Weapon);
            }
        }

        public EquipmentSlot.SlotType GetSlotType()
        {
            return EquipmentSlot.SlotType.Weapon;
        }

        public void OnEquip(EquipmentSlot slot)
        {
            EquipmentWeaponSlot weaponSlot = (EquipmentWeaponSlot) slot;
            GameObject.Instantiate(statement.prefab, weaponSlot.rightHand);
        }

        public void OnUneqiup(EquipmentSlot slot)
        {
            EquipmentWeaponSlot weaponSlot = (EquipmentWeaponSlot) slot;
            foreach (Transform transform in weaponSlot.rightHand)
                GameObject.Destroy(transform.gameObject);
        }
    }
}