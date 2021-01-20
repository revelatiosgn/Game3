using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using ARPG.Inventory;
using ARPG.Gear;
using ARPG.Core;
using ARPG.Items;
using ARPG.Events;

namespace ARPG.UI
{
    public class InventoryPanel : MonoBehaviour
    {
        [SerializeField] VoidEvent onPlayerInventory;

        [SerializeField] GridLayoutGroup grid;
        [SerializeField] Button close;
        [SerializeField] ItemEvent onEquip;
        [SerializeField] ItemEvent onUnequip;

        ItemsContainer itemsContainer;
        Equipment equipment;

        void Awake()
        {
            GameObject player = GameObject.FindGameObjectWithTag(Constants.Tags.Player);
            itemsContainer = player.GetComponent<ItemsContainer>();
            equipment = player.GetComponent<Equipment>();
        }

        void Start()
        {
            close.onClick.AddListener(() => {
                onPlayerInventory.RaiseEvent();
            });
        }

        void OnEnable()
        {
            List<ItemSlot> itemSlots = itemsContainer.ItemSlots;
            for (int i = 0; i < itemSlots.Count; i++)
            {
                ItemSlot itemSlot = itemSlots[i];
                InventorySlot inventorySlot = grid.transform.GetChild(i).GetComponent<InventorySlot>();
                inventorySlot.ItemSlot = itemSlot;
                inventorySlot.SetEquipped(equipment.IsEquipped(itemSlot.item));
            }

            onEquip.OnEventRaised += OnEquip;
            onUnequip.OnEventRaised += OnUnequip;
        }

        void OnDisable()
        {
            foreach (Transform child in grid.transform)
                child.GetComponent<InventorySlot>().ItemSlot = null;

            onEquip.OnEventRaised -= OnEquip;
            onUnequip.OnEventRaised -= OnUnequip;
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

