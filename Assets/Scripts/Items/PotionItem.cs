using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "Potion", menuName = "Items/Potion", order = 1)]
    public sealed class PotionItem : Item
    {
        public float hp;

        public override void OnUse(GameObject target)
        {
        }
    }
}

