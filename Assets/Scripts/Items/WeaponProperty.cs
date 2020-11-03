using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using ARPG.Combat;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "WeaponItem", menuName = "Items/Weapon", order = 1)]
    public class WeaponProperty : EquipmentProperty
    {
        public float damage;
        public MonoScript behaviour;
        public AnimatorOverrideController animatorController;

        public override void Use(Item item)
        {
            base.Use(item);
        }
    }
}


