using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;
using ARPG.Inventory;

namespace ARPG.Equipment
{
    public class Equipments : MonoBehaviour
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

        public void Equip(IEquipmentItem item)
        {
            EquipmentSlot slot = GetEquipmentSlot(item.GetSlotType());
            slot.Item = item;
        }
        
        public void UnEquip(EquipmentSlot.SlotType slotType)
        {
            EquipmentSlot slot = GetEquipmentSlot(slotType);
            slot.Item = null;
        }

        public bool IsEquipped(IEquipmentItem item)
        {
            return false;
        }

        public EquipmentSlot GetEquipmentSlot(IEquipmentItem item)
        {
            return equipmentSlots.Find(equipmentSlot => equipmentSlot.Item == item);
        }

        public EquipmentSlot GetEquipmentSlot(EquipmentSlot.SlotType slotType)
        {
            return equipmentSlots.Find(equipmentSlot => equipmentSlot.GetSlotType() == slotType);
        }
    }
}
