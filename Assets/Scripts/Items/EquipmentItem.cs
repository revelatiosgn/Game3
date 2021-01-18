using System.Collections;
using System.Collections.Generic;
using ARPG.Gear;
using UnityEngine;

namespace ARPG.Items
{
    public abstract class EquipmentItem : Item
    {
        public override void OnUse(GameObject target)
        {
            Equipment equipment = target.GetComponent<Equipment>();
            if (equipment)
            {
                if (equipment.IsEquipped(this))
                    equipment.UnEquip(this);
                else
                    equipment.Equip(this);
            }
        }

        public abstract EquipmentSlot.SlotType GetSlotType();
    }
}