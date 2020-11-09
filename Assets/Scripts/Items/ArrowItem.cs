using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Gear;

namespace ARPG.Items
{
    public sealed class ArrowItem : EquipmentItem
    {
        public ArrowStatement statement;

        // Item

        public override Sprite GetIcon()
        {
            return statement.icon;
        }

        public override void OnUse()
        {
            Transform target = GameObject.FindGameObjectWithTag(Constants.Tags.Player).transform;
            Equipment equipment = target.GetComponent<Equipment>();
            if (equipment.IsEquipped(this))
                equipment.UnEquip(this);
            else
                equipment.Equip(this);
        }

        public override bool IsEquals(Item other)
        {
            ArrowItem arrowItem = other as ArrowItem;
            if (arrowItem != null && arrowItem.statement == statement)
                return true;

            return false;
        }

        // EquipmentItem

        public override EquipmentSlot.SlotType GetSlotType()
        {
            return EquipmentSlot.SlotType.Arrow;
        }

        public override void OnEquip(EquipmentSlot equipmentSlot)
        {
        }

        public override void OnUnequip(EquipmentSlot equipmentSlot)
        {
        }

        public ArrowStatement GetStatement()
        {
            return statement;
        }
    }
}