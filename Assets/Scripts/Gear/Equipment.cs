using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;
using ARPG.Events;

namespace ARPG.Gear
{
    public class Equipment : MonoBehaviour
    {
        [SerializeReference][SerializeField] List<EquipmentSlot> equipmentSlots = new List<EquipmentSlot>();
        public List<EquipmentSlot> EquipmentSlots { get { return equipmentSlots; } private set { equipmentSlots = value; } }

        public EquipmentItemEvent onEquip;
        public EquipmentItemEvent onUnequip;

        public void Equip(EquipmentItem item)
        {
            EquipmentSlot slot = GetSlot(item);
            slot?.Equip(this, item);
        }

        public void Unequip(EquipmentItem item)
        {
            EquipmentSlot slot = GetSlot(item);
            slot?.Unequip(this, item);
        }

        public bool IsEquipped(EquipmentItem item)
        {
            EquipmentSlot slot = GetSlot(item);
            if (slot != null)
                return slot.IsEquipped(item);

            return false;
        }

        public EquipmentSlot GetSlot(EquipmentItem item)
        {
            return equipmentSlots.Find(slot => { return slot.type == item.GetSlotType(); });
        }

        public T GetSlot<T>() where T : EquipmentSlot
        {
            foreach (EquipmentSlot slot in equipmentSlots)
            {
                if (slot as T != null)
                    return slot as T;
            }

            return null;
        }
    }
}
