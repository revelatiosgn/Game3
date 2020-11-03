using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;

namespace ARPG.Inventory
{
    public class ItemsContainer : MonoBehaviour
    {
        [SerializeField] int capacity;
        [SerializeField] List<ItemSlot> itemSlots;

        public bool AddItem(Item item)
        {
            if (itemSlots.Count >= capacity)
                return false;

            ItemSlot itemSlot = new ItemSlot();
            itemSlot.item = item;
            itemSlot.count = 1;
            itemSlots.Add(itemSlot);

            return true;
        }

        public bool RemoveItem(Item item)
        {
            ItemSlot itemSlot = GetItemSlot(item);
            if (itemSlot != null)
            {
                itemSlots.Remove(itemSlot);
                return true;
            }

            return false;
        }

        public List<ItemSlot> GetItemSlots()
        {
            return itemSlots;
        }

        ItemSlot GetItemSlot(Item item)
        {
            foreach (ItemSlot itemSlot in itemSlots)
            {
                if (itemSlot.item == item)
                    return itemSlot;
            }

            return null;
        }

        ItemSlot GetFreeSlot()
        {
            foreach (ItemSlot itemSlot in itemSlots)
            {
                if (itemSlot.count == 0)
                    return itemSlot;
            }

            return null;
        }
    }
}
