using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;

namespace ARPG.Inventory
{
    [System.Serializable]
    public class ItemSlot
    {
        public Item item;
        public int count;
    }
}
