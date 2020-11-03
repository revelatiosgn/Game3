using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using ARPG.Items;
using ARPG.Combat;
using ARPG.Inventory;
using ARPG.Core;

namespace ARPG.UI
{
    public class InventoryPanel : MonoBehaviour
    {
        [SerializeField] GridLayoutGroup grid;
        [SerializeField] Button close;

        ItemsContainer itemsContainer;
        Equipment equipment;

        void Awake()
        {
            GameObject player = GameObject.FindGameObjectWithTag(Constants.Tags.Player);
            itemsContainer = player.GetComponent<ItemsContainer>();
            equipment = player.GetComponent<Equipment>();

            equipment.onEquip += OnEquip;
            equipment.onUnequip += OnUnequip;
        }

        void Start()
        {
            close.onClick.AddListener(() => {
                InputHandler.inventoryInput = true;
            });
        }

        void OnEnable()
        {
            List<ItemSlot> itemSlots = itemsContainer.GetItemSlots();
            for (int i = 0; i < itemSlots.Count; i++)
            {
                ItemSlot itemSlot = itemSlots[i];
                grid.transform.GetChild(i).GetComponent<InventorySlot>().ItemSlot = itemSlot;
            }
        }

        InventorySlot GetInventorySlot(Item item)
        {
            foreach(Transform child in grid.transform)
            {
                InventorySlot inventorySlot = child.GetComponent<InventorySlot>();
                if (inventorySlot.ItemSlot != null && inventorySlot.ItemSlot.item == item)
                {
                    return inventorySlot;
                }
            }

            return null;
        }

        void OnEquip(Item item)
        {
            InventorySlot inventorySlot = GetInventorySlot(item);
            if (inventorySlot)
                inventorySlot.SetEquipped(true);
        }

        void OnUnequip(Item item)
        {
            InventorySlot inventorySlot = GetInventorySlot(item);
            if (inventorySlot)
                inventorySlot.SetEquipped(false);
        }
    }
}

