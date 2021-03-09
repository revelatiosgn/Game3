using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;
using ARPG.Events;

namespace ARPG.Gear
{
    [System.Serializable]
    public class EquipmentShieldSlot : EquipmentSlot
    {
        [SerializeField] Transform body;
        [SerializeField] Transform arm;

        ShieldItem shieldItem;

        public EquipmentShieldSlot()
        {
            type = Type.Shield;
        }

        public override bool Equip(Equipment equipment, EquipmentItem item)
        {
            Unequip(equipment);

            shieldItem = item as ShieldItem;

            if (shieldItem == null)
                return false;

            GameObject.Instantiate<ShieldBehaviour>(shieldItem.prefab, arm);

            EquipmentWeaponSlot weaponSlot = equipment.GetSlot<EquipmentWeaponSlot>();
            if (weaponSlot != null && weaponSlot.weaponItem != null && weaponSlot.weaponItem.GetWeaponType() != WeaponItem.WeaponType.LightMelee)
                weaponSlot.Unequip(equipment);

            equipment.onEquip.RaiseEvent(shieldItem, equipment.gameObject);

            return true;
        }

        public override bool Unequip(Equipment equipment, EquipmentItem item)
        {
            if (item != shieldItem)
                return false;

            return Unequip(equipment);
        }

        public bool Unequip(Equipment equipment)
        {
            if (shieldItem == null)
                return false;

            foreach (Transform transform in body)
            {
                if (transform.GetComponent<ShieldBehaviour>() != null)
                    Destroy(transform.gameObject);
            }

            foreach (Transform transform in arm)
            {
                if (transform.GetComponent<ShieldBehaviour>() != null)
                    Destroy(transform.gameObject);
            }
                
            equipment.onUnequip.RaiseEvent(shieldItem, equipment.gameObject);
            shieldItem = null;

            return true;
        }
        
        public override bool IsEquipped(EquipmentItem item)
        {
            return shieldItem != null && item == shieldItem;
        }
        
        public override void Update(Equipment equipment)
        {
        }
    }
}
