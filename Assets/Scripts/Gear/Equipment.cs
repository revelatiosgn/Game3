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

        public List<EquipmentSlot> EquipmentSlots
        {
            get => equipmentSlots;
            private set { equipmentSlots = value; }
        }
        
        public ItemEvent onEquip = new ItemEvent();
        public ItemEvent onUnequip = new ItemEvent();

        void Awake()
        {
        }

        void Start()
        {
        }

        public void Equip(EquipmentItem item)
        {
            EquipmentSlot slot = GetEquipmentSlot(item.GetSlotType());

            if (slot == null)
                return;

            if (slot.item != null)
            {
                onUnequip.Invoke(slot.item);
                slot.Unequip(gameObject);
            }

            slot.Equip(item, gameObject);
            onEquip.Invoke(item);

            ResolveConflict(item);
        }

        public void UnEquip(EquipmentItem item)
        {
            EquipmentSlot slot = GetEquipmentSlot(item.GetSlotType());

            onUnequip.Invoke(slot.item);
            slot.Unequip(gameObject);
        }
        
        public bool IsEquipped(Item item)
        {
            return GetEquipmentSlot(item) != null;
        }

        public EquipmentSlot GetEquipmentSlot(Item item)
        {
            return equipmentSlots.Find(equipmentSlot => equipmentSlot.item == item);
        }

        public EquipmentSlot GetEquipmentSlot(EquipmentSlot.SlotType slotType)
        {
            return equipmentSlots.Find(equipmentSlot => equipmentSlot.GetSlotType() == slotType);
        }

        void ResolveConflict(EquipmentItem item)
        {
            // if ((item as ShieldItem) != null)
            // {
            //     EquipmentWeaponSlot weaponSlot = GetEquipmentSlot(EquipmentSlot.SlotType.Weapon) as EquipmentWeaponSlot;
            //     if (weaponSlot != null)
            //     {
            //         WeaponItem weaponItem = weaponSlot.Item as WeaponItem;
            //         WeaponStatement statement = weaponItem.statement as WeaponStatement;

            //         if (weaponItem != null && statement.type == WeaponStatement.Type.TwoHanded)
            //         {
            //             UnEquip(weaponItem);
            //         }
            //     }
            // }

            // if ((item as WeaponItem) != null)
            // {
            //     WeaponStatement statement = item.statement as WeaponStatement;

            //     if (statement.type == WeaponStatement.Type.TwoHanded)
            //     {
            //         EquipmentShieldSlot shieldSlot = GetEquipmentSlot(EquipmentSlot.SlotType.Shield) as EquipmentShieldSlot;
            //         if (shieldSlot != null)
            //         {
            //             ShieldItem shieldItem = shieldSlot.Item as ShieldItem;
            //             if (shieldItem != null)
            //             {
            //                 UnEquip(shieldItem);
            //             }
            //         }
            //     }
            // }
        }
    }
}
