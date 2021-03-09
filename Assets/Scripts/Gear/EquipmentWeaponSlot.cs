using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;
using ARPG.Events;

namespace ARPG.Gear
{
    [System.Serializable]
    public class EquipmentWeaponSlot : EquipmentSlot
    {
        [System.Serializable]
        public class WeaponHolder
        {
            public WeaponItem.WeaponType type;
            public Transform body;
            public Transform arm;
        }

        [SerializeField] List<WeaponHolder> weaponHolders;
        public WeaponItem weaponItem;

        public EquipmentWeaponSlot()
        {
            type = Type.Weapon;
        }

        public override bool Equip(Equipment equipment, EquipmentItem item)
        {
            Unequip(equipment);

            weaponItem = item as WeaponItem;

            if (weaponItem == null)
                return false;

            WeaponHolder weaponHolder = weaponHolders.Find(weaponHolder => weaponHolder.type == weaponItem.GetWeaponType());

            if (weaponHolder == null)
            {
                weaponItem = null;
                return false;
            }

            WeaponBehaviour weaponBehaviour = GameObject.Instantiate<WeaponBehaviour>(weaponItem.prefab, weaponHolder.arm);
            weaponBehaviour.Init(weaponItem, equipment.gameObject);

            EquipmentShieldSlot shieldSlot = equipment.GetSlot<EquipmentShieldSlot>();
            if (shieldSlot != null && weaponItem.GetWeaponType() != WeaponItem.WeaponType.LightMelee)
                shieldSlot.Unequip(equipment);

            equipment.onEquip.RaiseEvent(weaponItem, equipment.gameObject);

            return true;
        }

        public override bool Unequip(Equipment equipment, EquipmentItem item)
        {
            if (item != weaponItem)
                return false;

            return Unequip(equipment);
        }

        public bool Unequip(Equipment equipment)
        {
            if (weaponItem == null)
                return false;

            foreach (WeaponHolder weaponHolder in weaponHolders)
            {
                foreach (Transform transform in weaponHolder.body)
                {
                    if (transform.GetComponent<WeaponBehaviour>() != null)
                        Destroy(transform.gameObject);
                }

                foreach (Transform transform in weaponHolder.arm)
                {
                    if (transform.GetComponent<WeaponBehaviour>() != null)
                        Destroy(transform.gameObject);
                }
            }

            equipment.onUnequip.RaiseEvent(weaponItem, equipment.gameObject);
            
            weaponItem = null;

            return true;
        }
        
        public override bool IsEquipped(EquipmentItem item)
        {
            return weaponItem != null && item == weaponItem;
        }
        
        public override void Update(Equipment equipment)
        {
        }
    }
}
