using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using ARPG.Items;

namespace ARPG.UI
{
    public class InventoryPanel : MonoBehaviour
    {
        [SerializeField] GridLayoutGroup grid;
        [SerializeField] GameObject inventoryItemPrefab;

        Items.Inventory playerInventory;

        void Awake()
        {
            playerInventory = GameObject.FindGameObjectWithTag(Constants.Tags.Player).GetComponent<Inventory>();
        }

        void OnEnable()
        {
            Clear();

            List<Item> items = playerInventory.GetItems();
            foreach (Item item in items)
            {
                InventoryItem inventoryItem = Instantiate(inventoryItemPrefab, grid.transform).GetComponent<InventoryItem>();
                inventoryItem.SetItem(item);
            }
        }

        void Clear()
        {
            foreach(Transform item in grid.transform)
                Destroy(item.gameObject);
        }
    }
}

