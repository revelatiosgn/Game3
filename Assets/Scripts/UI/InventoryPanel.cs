using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using ARPG.Inventory;
using ARPG.Gear;
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
        }

        void Start()
        {
            close.onClick.AddListener(() => {
                InputHandler.inventoryInput = true;
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

            equipment.onEquip.AddListener(OnEquip);
            equipment.onUnequip.AddListener(OnUnequip);
        }

        void OnDisable()
        {
            foreach (Transform child in grid.transform)
                child.GetComponent<InventorySlot>().ItemSlot = null;
            
            equipment.onEquip.RemoveListener(OnEquip);
            equipment.onUnequip.RemoveListener(OnUnequip);
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

