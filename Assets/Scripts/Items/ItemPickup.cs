using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Items
{
    public class ItemPickup : MonoBehaviour
    {
        [SerializeField] Item item;

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<Inventory>().AddItem(item);
                Destroy(gameObject);
            }
        }
    }
}