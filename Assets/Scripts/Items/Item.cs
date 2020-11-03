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

        public void UseItem()
        {
            property.Use(this);
        }

        public T GetItemProperty<T>()
        {
            if (typeof(T) == property.GetType())
                return (T) (object) property;

            return default(T);
        }
    }
}
