using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Inventory
{
    public class ItemPickup : MonoBehaviour
    {
        ItemsContainer itemsContainer;

        void Awake()
        {
            itemsContainer = GetComponent<ItemsContainer>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == Constants.Tags.Player)
            {
                Debug.Log("pickup");
                other.GetComponent<ItemsContainer>().Merge(itemsContainer);
            }
        }

        // void Grab(ItemsContainer destination)
        // {
        //     List<ItemSlot> itemSlots = itemsContainer.GetItemSlots();
        //     foreach (ItemSlot itemSlot in itemSlots)
        //     {
        //         destination.AddItem(itemSlot.item, itemSlot.count);
        //     }

        //     Destroy(gameObject);
        // }
    }
}
