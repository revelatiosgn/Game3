using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ARPG.Inventory
{
    public class ItemsContainer : MonoBehaviour
    {
        [SerializeField] List<ItemSlot> itemSlots;

        public List<ItemSlot> ItemSlots
        {
            get => itemSlots;
            private set { itemSlots = value; }
        }

        public bool AddItemSlot(ItemSlot itemSlot)
        {
            ItemSlot eqSlot = itemSlots.Find(slot => slot.item == itemSlot.item);
            if (eqSlot != null)
            {
                eqSlot.count += itemSlot.count;
            }
            else
            {
                itemSlots.Add(itemSlot);
            }

            return true;
        }

        public bool RemoveItemSlot(ItemSlot itemSlot)
        {
            return itemSlots.Remove(itemSlot);
        }

        public bool AddItemSlots(List<ItemSlot> itemSlots)
        {
            foreach (ItemSlot itemSlot in itemSlots)
                AddItemSlot(itemSlot);

            return true;
        }

        public bool Merge(ItemsContainer itemsContainer)
        {
            return AddItemSlots(itemsContainer.itemSlots);
        }
    }
}
