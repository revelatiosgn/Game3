using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;

namespace ARPG.Gear
{
    [System.Serializable]
    public class EquipmentWeaponSlot : EquipmentSlot
    {
        public enum Hand
        {
            Left,
            Right
        }

        [SerializeField] Transform leftHand;
        [SerializeField] Transform rightHand;

        public GameObject currentWeapon;
        public WeaponStatement defaultWeapon;

        public override SlotType GetSlotType()
        {
            return SlotType.Weapon;
        }

        protected override void Equip()
        {
            WeaponItem weaponItem = Item as WeaponItem;
            Transform parent = weaponItem.GetStatement().hand == Hand.Left ? leftHand : rightHand;
            currentWeapon = GameObject.Instantiate(weaponItem.GetStatement().prefab, parent);
        }

        protected override void Unequip()
        {
            if (currentWeapon)
                GameObject.Destroy(currentWeapon);
            currentWeapon = null;
        }

        protected override EquipmentItem GetDefaultItem()
        {
            if (defaultWeapon != null)
                return defaultWeapon.CreateItem();

            return null;
        }
    }
}


