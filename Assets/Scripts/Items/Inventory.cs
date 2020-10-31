using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Items
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] List<Item> items;

        Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public List<Item> GetItems()
        {
            return items;
        }

        public void AddItem(Item item)
        {
            items.Add(item);

            item.Use();
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }
    }
}
