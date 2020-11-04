using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;

namespace ARPG.Combat
{
    [System.Serializable]
    public class EquipmentSlot
    {
        public enum SlotType
        {
            Weapon,
            Arrows,
            Shield
        }

        public SlotType slotType;
        public List<EquipmentHolder> holders;
        public EquipmentProperty defaultEquipment;

        Item item;   
        public Item Item
        {
            get => item;
            set
            {
                item = value;

                foreach (EquipmentHolder holder in holders)
                {
                    holder.AttachItem(item);
                }
            }
        }
    }
}


