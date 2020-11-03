using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using ARPG.Combat;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "ArrowItem", menuName = "Items/Arrow", order = 1)]
    public class ArrowProperty : EquipmentProperty
    {
        public float damage;

        public override void Use(Item item)
        {
        }
    }
}


