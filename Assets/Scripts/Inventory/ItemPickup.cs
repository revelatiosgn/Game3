using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;

namespace ARPG.Inventory
{
    public class ItemPickup : MonoBehaviour
    {
        public Item item;

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == Constants.Tags.Player)
            {
                if (other.GetComponent<ItemsContainer>().AddItem(item))
                    Destroy(gameObject);
            }
        }
    }
}
