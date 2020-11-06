using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using ARPG.Combat;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "ArmorItem", menuName = "Items/Armor", order = 1)]
    public class ArmorProperty : EquipmentProperty
    {
        public float damage;

        public override void Use(Item item)
        {
            base.Use(item);
        }
    }
}


