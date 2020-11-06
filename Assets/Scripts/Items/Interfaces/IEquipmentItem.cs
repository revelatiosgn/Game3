using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Equipment;

namespace ARPG.Items
{
    public interface IEquipmentItem
    {
        EquipmentSlot.SlotType GetSlotType();
        void OnEquip(EquipmentSlot slot);
        void OnUneqiup(EquipmentSlot slot);
    }
}
