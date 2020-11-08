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
        }

        public void Equip(EquipmentItem item)
        {
            EquipmentSlot slot = GetEquipmentSlot(item.GetSlotType());

            if (slot.Item != null)
                onUnequip.Invoke(slot.Item);

            slot.Item = item;
            onEquip.Invoke(item);
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
    }
}
