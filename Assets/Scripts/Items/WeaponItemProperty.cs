using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using ARPG.Combat;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "WeaponItem", menuName = "Items/Weapon", order = 1)]
    public class WeaponItemProperty : ItemProperty
    {
        public enum WeaponType
        {
            Melee,
            Ranged
        }

        public WeaponType type;
        public WeaponHolder.HolderType holderType;
        public AnimatorOverrideController animatorOverrideController;
        public MonoScript behaviour;
        public bool isDualHanded;
        public float baseDamage;

        public override void Use(Item item)
        {
            Equipment equipment = GameObject.FindGameObjectWithTag(Constants.Tags.Player).GetComponent<Equipment>();
            if (equipment.GetWeaponHolder(item))
            {
                equipment.Unequip(holderType);
            }
            else
            {
                equipment.EquipWeapon(item);
            }
        }
    }
}


