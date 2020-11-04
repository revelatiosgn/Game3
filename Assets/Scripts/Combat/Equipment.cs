using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;
using ARPG.Inventory;

namespace ARPG.Combat
{
    public class Equipment : MonoBehaviour
    {
        public List<EquipmentSlot> equipmentSlots;

        Animator animator;
        ItemsContainer inventory;
        
        public ItemEvent onEquip = new ItemEvent();
        public ItemEvent onUnequip = new ItemEvent();

        void Awake()
        {
            animator = GetComponent<Animator>();
            inventory = GetComponent<ItemsContainer>();
        }

        void Start()
        {
            inventory.onRemoveItem.AddListener(OnRemoveItem);
            EquipDefault(GetEquipmentSlot(EquipmentSlot.SlotType.Weapon));
        }

        public void Equip(Item item)
        {
            EquipmentProperty property = item.property as EquipmentProperty;
            EquipmentSlot slot = GetEquipmentSlot(property.slotType);

            if (slot.Item == null)
            {
                slot.Item = item;
                onEquip.Invoke(item);
            }
            else
            {
                if (slot.Item == item)
                {
                    slot.Item = null;
                    onUnequip.Invoke(item);
                }
                else
                {
                    onUnequip.Invoke(slot.Item);
                    slot.Item = item;
                    onEquip.Invoke(item);
                }
            }

            if (slot.Item == null)
                EquipDefault(slot);
        }

        public bool IsEquipped(Item item)
        {
            return GetEquipmentSlot(item) != null;
        }

        public EquipmentSlot GetEquipmentSlot(Item item)
        {
            return equipmentSlots.Find(equipmentSlot => equipmentSlot.Item == item);
        }

        public EquipmentSlot GetEquipmentSlot(EquipmentSlot.SlotType slotType)
        {
            return equipmentSlots.Find(equipmentSlot => equipmentSlot.slotType == slotType);
        }

        void EquipDefault(EquipmentSlot equipmentSlot)
        {
            if (equipmentSlot.defaultEquipment)
            {
                equipmentSlot.Item = new Item(equipmentSlot.defaultEquipment);
                onEquip.Invoke(equipmentSlot.Item);
            }
        }

        void OnRemoveItem(Item item)
        {
            EquipmentProperty property = item.property as EquipmentProperty;
            if (inventory.GetItemSlot(item) == null && property)
            {
                EquipmentSlot slot = GetEquipmentSlot(item);
                slot.Item = null;
                onUnequip.Invoke(item);
                EquipDefault(slot);
            }

        }
    }
}
