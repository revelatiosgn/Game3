using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;

namespace ARPG.Equipment
{
    [System.Serializable]
    public abstract class EquipmentSlot
    {
        public enum SlotType
        {
            Weapon,
            Arrows,
            Shield
        }

        public abstract SlotType GetSlotType();

        IEquipmentItem item;   
        public IEquipmentItem Item
        {
            get => item;
            set
            {
                if (item != null)
                    item.OnUneqiup(this);

                item = value;

                if (item != null)
                    item.OnEquip(this);
            }
        }
    }
}


