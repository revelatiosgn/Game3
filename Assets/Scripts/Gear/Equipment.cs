using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;
using ARPG.Inventory;

namespace ARPG.Gear
{
    public class Equipment : MonoBehaviour
    {
        [SerializeReference] List<EquipmentSlot> equipmentSlots;
        
        public ItemEvent onEquip = new ItemEvent();
        public ItemEvent onUnequip = new ItemEvent();

        void Awake()
        {
        }

        void Start()
        {
            foreach (EquipmentSlot equipmentSlot in equipmentSlots)
                equipmentSlot.Item = null;
        }

        public void Equip(EquipmentItem item)
        {
            EquipmentSlot slot = GetEquipmentSlot(item.GetSlotType());

            if (slot.Item != null)
                onUnequip.Invoke(slot.Item);

            slot.Item = item;
            onEquip.Invoke(item);

            ResolveConflict(item);
        }

        public void UnEquip(EquipmentItem item)
        {
            EquipmentSlot slot = GetEquipmentSlot(item.GetSlotType());

            onUnequip.Invoke(slot.Item);
            slot.Item = null;
        }
        
        public bool IsEquipped(Item item)
        {
            return GetEquipmentSlot(item) != null;
        }

        public EquipmentSlot GetEquipmentSlot(Item item)
        {
            return equipmentSlots.Find(equipmentSlot => equipmentSlot.Item == item);
        }

        public EquipmentSlot GetEquipmentSlot(EquipmentSlot.SlotType slotType)
        {
            return equipmentSlots.Find(equipmentSlot => equipmentSlot.GetSlotType() == slotType);
        }

        void ResolveConflict(EquipmentItem item)
        {
            if ((item as ShieldItem) != null)
            {
                EquipmentWeaponSlot weaponSlot = GetEquipmentSlot(EquipmentSlot.SlotType.Weapon) as EquipmentWeaponSlot;
                if (weaponSlot != null)
                {
                    WeaponItem weaponItem = weaponSlot.Item as WeaponItem;
                    if (weaponItem != null && weaponItem.GetStatement().type == WeaponStatement.Type.TwoHanded)
                    {
                        UnEquip(weaponItem);
                    }
                }
            }

            if ((item as WeaponItem) != null && (item as WeaponItem).GetStatement().type == WeaponStatement.Type.TwoHanded)
            {
                EquipmentShieldSlot shieldSlot = GetEquipmentSlot(EquipmentSlot.SlotType.Shield) as EquipmentShieldSlot;
                if (shieldSlot != null)
                {
                    ShieldItem shieldItem = shieldSlot.Item as ShieldItem;
                    if (shieldItem != null)
                    {
                        UnEquip(shieldItem);
                    }
                }
            }
        }
    }
}
