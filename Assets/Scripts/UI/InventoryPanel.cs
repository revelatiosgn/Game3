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
        [SerializeField] VoidEvent onPlayerUIInventory;
        [SerializeField] EquipmentItemEvent onEquip;
        [SerializeField] EquipmentItemEvent onUnequip;
        [SerializeField] VoidEvent onPlayerUIUseItem;
        [SerializeField] VoidEvent onPlayerUIDropItem;

        [SerializeField] GridLayoutGroup grid;
        [SerializeField] Text UseHint;
        [SerializeField] Text DropHint;

        ItemsContainer itemsContainer;
        Equipment equipment;
        InventorySlot selectedSlot;

        void Awake()
        {
            GameObject player = GameObject.FindGameObjectWithTag(Constants.Tags.Player);
            itemsContainer = player.GetComponent<ItemsContainer>();
            equipment = player.GetComponent<Equipment>();
        }

        void OnEnable()
        {
            UseHint.gameObject.SetActive(false);
            DropHint.gameObject.SetActive(false);

            onEquip.onEventRaised += OnEquip;
            onUnequip.onEventRaised += OnUnequip;
            onPlayerUIUseItem.onEventRaised += OnPlayerUIUseItem;
            onPlayerUIDropItem.onEventRaised += OnPlayerUIDropItem;
            
            UpdateSlots();
        }

        void Start()
        {
            UpdateSlots();
        }

        void OnDisable()
        {
            foreach (Transform child in grid.transform)
                child.GetComponent<InventorySlot>().ItemSlot = null;

            onEquip.onEventRaised -= OnEquip;
            onUnequip.onEventRaised -= OnUnequip;
            onPlayerUIUseItem.onEventRaised -= OnPlayerUIUseItem;
            onPlayerUIDropItem.onEventRaised -= OnPlayerUIDropItem;
        }

        void Update()
        {
            UseHint.gameObject.SetActive(selectedSlot != null);
            DropHint.gameObject.SetActive(selectedSlot != null);
        }

        void UpdateSlots()
        {
            // List<ItemSlot> itemSlots = itemsContainer.ItemSlots;
            // for (int i = 0; i < grid.transform.childCount; i++)
            // {
            //     InventorySlot slot = grid.transform.GetChild(i).GetComponent<InventorySlot>();
            //     if (i < itemSlots.Count)
            //     {
            //         slot.ItemSlot = itemSlots[i];
            //         slot.SetEquipped(equipment.IsEquipped(itemSlots[i].item));
            //     }
            //     else
            //     {
            //         slot.ItemSlot = null;
            //     }
            //     slot.SetSelected(false);
            // }

            // selectedSlot = null;
        }

        public void OnSlotSelected(InventorySlot slot)
        {
            if (selectedSlot != null)
                selectedSlot.SetSelected(false);

            selectedSlot = slot;
            selectedSlot.SetSelected(true);
        }

        public void OnClose()
        {
            onPlayerUIInventory.RaiseEvent();
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

        void OnEquip(Item item, GameObject target)
        {
            if (target != itemsContainer.gameObject)
                return;
                
            InventorySlot inventorySlot = GetInventorySlot(item);
            if (inventorySlot)
                inventorySlot.SetEquipped(true);
        }

        void OnUnequip(Item item, GameObject target)
        {
            if (target != itemsContainer.gameObject)
                return;

            InventorySlot inventorySlot = GetInventorySlot(item);
            if (inventorySlot)
                inventorySlot.SetEquipped(false);
        }

        void OnPlayerUIUseItem()
        {
            if (selectedSlot != null)
                selectedSlot.UseItem();
        }

        void OnPlayerUIDropItem()
        {
            // if (selectedSlot != null && selectedSlot.ItemSlot != null)
            // {
            //     equipment.UnEquip(selectedSlot.ItemSlot.item);
            //     itemsContainer.ReduceItemSlot(selectedSlot.ItemSlot);
            //     UpdateSlots();
            // }
        }
    }
}

