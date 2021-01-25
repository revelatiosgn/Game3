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
        public float range = 3f;
        public AnimatorOverrideController animatorContoller;
        public EquipmentWeaponSlot.Hand hand;
        public Type type = Type.OneHanded;
        public string actionLayer = "Sword1H";
        public string maskLayer = "RWeaponMask";

        public override EquipmentSlot.SlotType GetSlotType()
        {
            return EquipmentSlot.SlotType.Weapon;
        }
    }
}

