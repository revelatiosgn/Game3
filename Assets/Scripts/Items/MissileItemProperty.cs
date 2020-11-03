using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using ARPG.Combat;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "Missiletem", menuName = "Items/Missile", order = 1)]
    public class MissileItemProperty : ItemProperty
    {
        public WeaponHolder.HolderType holderType;
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
                equipment.EquipArrows(item);
            }
        }
    }
}


