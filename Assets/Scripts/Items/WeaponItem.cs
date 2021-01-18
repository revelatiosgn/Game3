using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Gear;
using ARPG.Combat;

namespace ARPG.Items
{
    public abstract class WeaponItem : EquipmentItem
    {
        public enum Type
        {
            OneHanded,
            TwoHanded
        }

        public GameObject prefab;
        public float baseDamage;
        public AnimatorOverrideController animatorContoller;
        public EquipmentWeaponSlot.Hand hand;
        public Type type = Type.OneHanded;
        public string layerName = "";

        public override EquipmentSlot.SlotType GetSlotType()
        {
            return EquipmentSlot.SlotType.Weapon;
        }
    }
}

