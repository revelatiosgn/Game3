using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using ARPG.Items;

namespace ARPG.Inventory
{
    public class ItemsContainer : MonoBehaviour
    {
        [SerializeReference]
        public List<ItemSlot> itemSlots;
    }
}
