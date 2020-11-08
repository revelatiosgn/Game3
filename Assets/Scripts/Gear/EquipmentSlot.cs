using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Gear
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

        private EquipmentItem item;
        public virtual EquipmentItem Item
        {
            get => item;
            set
            {
                if (item != null)
                {
                    Unequip();
                    item.OnUnequip(this);
                }

                item = value;

                if (item == null)
                    item = GetDefaultItem();

                if (item != null)
                {
                    Equip();
                    item.OnEquip(this);
                }
            }
        }

        protected abstract void Equip();
        protected abstract void Unequip();
        protected abstract EquipmentItem GetDefaultItem();
    }
}


