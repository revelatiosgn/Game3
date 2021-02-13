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
            ChestArmor,
            LegsArmor
        }

        [HideInInspector] public EquipmentItem item;

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

        public virtual void EquipDefault(GameObject target)
        {
        }

        public virtual void AddBehaviour(GameObject target)
        {
        }

        protected void Destroy(Object obj)
        {
            if (Application.isPlaying)
                GameObject.Destroy(obj);
            else
                GameObject.DestroyImmediate(obj);
        }
    }
}


