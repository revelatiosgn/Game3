using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;

namespace ARPG.Gear
{
    [System.Serializable]
    public class EquipmentSlot
    {
        public enum SlotType
        {
            None,
            Weapon,
            Arrow,
            Shield,
            Armor
        }

        public EquipmentItem item;

        public virtual SlotType GetSlotType()
        {
            return SlotType.None;
        }

        public virtual void Equip(EquipmentItem item, GameObject target)
        {
            this.item = item;
        }

        public virtual void Unequip(GameObject target)
        {
            this.item = null;
        }
    }
}


