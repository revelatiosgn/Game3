using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Gear;

namespace ARPG.Items
{
    public abstract class EquipmentItem : Item
    {
        public abstract EquipmentSlot.SlotType GetSlotType();
        public abstract void OnEquip(EquipmentSlot equipmentSlot);
        public abstract void OnUnequip(EquipmentSlot equipmentSlot);
    }
}