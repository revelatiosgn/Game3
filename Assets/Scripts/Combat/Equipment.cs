using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using ARPG.Items;

namespace ARPG.Combat
{
    public class Equipment : MonoBehaviour
    {
        public List<EquipmentSlot> equipmentSlots;

        Animator animator;

        public UnityAction<Item> onEquip;
        public UnityAction<Item> onUnequip;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Equip(Item item)
        {
            EquipmentProperty property = item.property as EquipmentProperty;
            EquipmentSlot slot = GetEquipmentSlot(property.slotType);

            if (slot.Item == null)
            {
                slot.Item = item;
                onEquip(item);
            }
            else
            {
                if (slot.Item == item)
                {
                    slot.Item = null;
                    onUnequip(item);
                }
                else
                {
                    onUnequip(slot.Item);
                    slot.Item = item;
                    onEquip(item);
                }
            }
        }

        EquipmentSlot GetEquipmentSlot(Item item)
        {
            return equipmentSlots.Find(equipmentSlot => equipmentSlot.Item == item);
        }

        EquipmentSlot GetEquipmentSlot(EquipmentSlot.SlotType slotType)
        {
            return equipmentSlots.Find(equipmentSlot => equipmentSlot.slotType == slotType);
        }
    }
}
