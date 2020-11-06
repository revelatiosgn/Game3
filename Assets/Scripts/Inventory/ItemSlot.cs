using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;

namespace ARPG.Inventory
{
    [System.Serializable]
    public class ItemSlot
    {
        [SerializeReference]
        public IInventoryItem item;
        public int count = 1;
    }
}
