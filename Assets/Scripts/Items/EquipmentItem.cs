using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Gear;

namespace ARPG.Items
{
    public abstract class EquipmentItem : Item
    {
        public override void OnUse(GameObject target)
        {
            Equipment equipment = target.GetComponent<Equipment>();
            if (equipment.IsEquipped(this))
                target.GetComponent<Equipment>().Unequip(this);
            else
                target.GetComponent<Equipment>().Equip(this);
        }

        public abstract EquipmentSlot.Type GetSlotType();
    }
}
