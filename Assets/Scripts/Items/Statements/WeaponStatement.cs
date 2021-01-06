using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Gear;

namespace ARPG.Items
{
    public abstract class WeaponStatement : ItemStatement
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

        public abstract WeaponItem CreateItem();
    }
}

