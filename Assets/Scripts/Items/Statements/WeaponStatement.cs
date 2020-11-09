using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Gear;

namespace ARPG.Items
{
    public abstract class WeaponStatement : ItemStatement
    {
        public GameObject prefab;
        public float baseDamage;
        public AnimatorOverrideController animatorContoller;
        public EquipmentWeaponSlot.Hand hand;

        public abstract WeaponItem CreateItem();
    }
}

