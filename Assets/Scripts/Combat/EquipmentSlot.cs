using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;

namespace ARPG.Combat
{
    public class EquipmentSlot : MonoBehaviour
    {
        public enum SlotType
        {
            Weapon,
            Arrows,
            Shield
        }

        public SlotType slotType { get; protected set; }
        private Item item;
        
        public Item Item
        {
            get => item;
            set
            {
                item = value;

                DetachItem();
                AttachItem(item);
            }
        }

        protected virtual void AttachItem(Item item) {}
        protected virtual void DetachItem() {}
    }
}


