using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "Shield", menuName = "Statements/Equipment/Shield", order = 1)]
    public sealed class ShieldStatement : ItemStatement
    {
        public GameObject shieldPrefab;
        public float baseArmor;

        public ShieldItem CreateItem()
        {
            ShieldItem item = new ShieldItem();
            item.statement = this;
            
            return item;
        }
    }
}

