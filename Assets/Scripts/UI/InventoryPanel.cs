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
            for (int i = 0; i < grid.transform.childCount; i++)
            {
                InventorySlot slot = grid.transform.GetChild(i).GetComponent<InventorySlot>();
                if (i < itemSlots.Count)
                {
                    slot.ItemSlot = itemSlots[i];
                    slot.SetEquipped(equipment.IsEquipped(itemSlots[i].item));
                }
                else
                {
                    slot.ItemSlot = null;
                }
            }

            onEquip.onEventRaised += OnEquip;
            onUnequip.onEventRaised += OnUnequip;
        }

        void OnDisable()
        {
            foreach (Transform child in grid.transform)
                child.GetComponent<InventorySlot>().ItemSlot = null;

            onEquip.onEventRaised -= OnEquip;
            onUnequip.onEventRaised -= OnUnequip;
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

