using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ARPG.Items
{
    [System.Serializable]
    public class Item
    {
        public ItemProperty property;

        public Item() {}

        public Item(ItemProperty property)
        {
            this.property = property;
        }

        public void UseItem()
        {
            property.Use(this);
        }
    }
}
