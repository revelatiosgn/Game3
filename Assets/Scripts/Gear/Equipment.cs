using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;
using ARPG.Events;

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
        
        [SerializeField] ItemEvent onEquip = default;
        [SerializeField] ItemEvent onUnequip = default;

        void Awake()
        {
        }

        void Start()
        {
            foreach (EquipmentSlot equipmentSlot in equipmentSlots)
                equipmentSlot.AddBehaviour(gameObject);
        }

        public void Equip(EquipmentItem item)
        {
            EquipmentSlot slot = GetEquipmentSlot(item.GetSlotType());

            if (slot == null)
                return;

            if (slot.item != null)
            {
                onUnequip.RaiseEvent(slot.item);
                slot.Unequip(gameObject);
            }

            slot.Equip(item, gameObject);
            onEquip.RaiseEvent(item);

            ResolveConflict(item);
        }

        public void UnEquip(EquipmentItem item)
        {
            EquipmentSlot slot = GetEquipmentSlot(item.GetSlotType());

            onUnequip.RaiseEvent(slot.item);
            slot.Unequip(gameObject);

            slot.EquipDefault(gameObject);
            onEquip.RaiseEvent(slot.item);
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
            if ((item as ShieldItem) != null)
            {
                EquipmentWeaponSlot weaponSlot = GetEquipmentSlot(EquipmentSlot.SlotType.Weapon) as EquipmentWeaponSlot;
                if (weaponSlot != null)
                {
                    WeaponItem weaponItem = weaponSlot.item as WeaponItem;
                    if (weaponItem != null && weaponItem.type == WeaponItem.Type.TwoHanded)
                    {
                        UnEquip(weaponItem);
                    }
                }
            }

            if ((item as WeaponItem) != null)
            {
                if ((item as WeaponItem).type == WeaponItem.Type.TwoHanded)
                {
                    EquipmentShieldSlot shieldSlot = GetEquipmentSlot(EquipmentSlot.SlotType.Shield) as EquipmentShieldSlot;
                    if (shieldSlot != null)
                    {
                        ShieldItem shieldItem = shieldSlot.item as ShieldItem;
                        if (shieldItem != null)
                        {
                            UnEquip(shieldItem);
                        }
                    }
                }
            }
        }
    }
}
