using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using ARPG.Items;

namespace ARPG.Inventory
{
    public class ItemsContainer : MonoBehaviour
    {
        [SerializeField] int capacity;
        [SerializeField] List<ItemSlot> itemSlots;

        public ItemEvent onAddItem = new ItemEvent();
        public ItemEvent onRemoveItem = new ItemEvent();

        void Start()
        {
        }

        public bool AddItem(Item item, int count = 1)
        {
            ItemSlot itemSlot = GetItemSlot(item.property);
            if (itemSlot != null)
            {
                itemSlot.count += count;
                onAddItem.Invoke(item);

                return true;
            }
            else if (itemSlots.Count < capacity)
            {
                itemSlot = new ItemSlot();
                itemSlot.item = item;
                itemSlot.count = count;
                itemSlots.Add(itemSlot);
                onAddItem.Invoke(item);

                return true;
            }

            return false;
        }
        

        public bool RemoveItem(Item item, int count = 1)
        {
            ItemSlot itemSlot = GetItemSlot(item);
            if (itemSlot != null && itemSlot.count >= count)
            {
                itemSlot.count -= count;
                if (itemSlot.count == 0)
                    itemSlots.Remove(itemSlot);
                onRemoveItem.Invoke(item);

                return true;
            }

            return false;
        }

        public List<ItemSlot> GetItemSlots()
        {
            return itemSlots;
        }

        public ItemSlot GetItemSlot(Item item)
        {
            foreach (ItemSlot itemSlot in itemSlots)
            {
                if (itemSlot.item == item)
                    return itemSlot;
            }

            return null;
        }

        public ItemSlot GetItemSlot(ItemProperty itemProperty)
        {
            foreach (ItemSlot itemSlot in itemSlots)
            {
                if (itemSlot.item.property == itemProperty)
                    return itemSlot;
            }

            return null;
        }
    }
}
