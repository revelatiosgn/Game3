using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using ARPG.Combat;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "PotionItem", menuName = "Items/Potion", order = 1)]
    public class PotionProperty : ItemProperty
    {
        public float health;

        public override void Use(Item item)
        {
            base.Use(item);
        }
    }
}


