using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;
using ARPG.Events;
using ARPG.Combat;

namespace ARPG.Gear
{
    [System.Serializable]
    public abstract class EquipmentSlot
    {
        public enum Type
        {
            Weapon,
            Shield,
            Armor,
            Quiver
        }

        public Type type;

        public abstract bool Equip(Equipment equipment, EquipmentItem item);
        public abstract bool Unequip(Equipment equipment, EquipmentItem item);
        public abstract bool IsEquipped(EquipmentItem item);
        public abstract void Update(Equipment equipment);

        protected void Destroy(GameObject gameObject)
        {
            if (Application.isPlaying)
                GameObject.Destroy(gameObject);
            else
                GameObject.DestroyImmediate(gameObject);
        }
    }
}
