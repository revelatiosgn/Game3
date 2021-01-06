using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Gear;

namespace ARPG.Items
{
    public sealed class ShieldItem : EquipmentItem
    {
        public ShieldStatement statement;

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
            ShieldItem arrowItem = other as ShieldItem;
            if (arrowItem != null && arrowItem.statement == statement)
                return true;

            return false;
        }

        // EquipmentItem

        public override EquipmentSlot.SlotType GetSlotType()
        {
            return EquipmentSlot.SlotType.Shield;
        }

        public override void OnEquip(EquipmentSlot equipmentSlot)
        {
            Transform target = GameObject.FindGameObjectWithTag(Constants.Tags.Player).transform;

            ShieldBehaviour shieldBehaviour = target.GetComponent<ShieldBehaviour>();
            if (shieldBehaviour != null)
                GameObject.Destroy(shieldBehaviour);
            target.gameObject.AddComponent<ShieldBehaviour>();
        }

        public override void OnUnequip(EquipmentSlot equipmentSlot)
        {
            Transform target = GameObject.FindGameObjectWithTag(Constants.Tags.Player).transform;

            ShieldBehaviour shieldBehaviour = target.GetComponent<ShieldBehaviour>();
            if (shieldBehaviour != null)
                GameObject.Destroy(shieldBehaviour);
        }

        public ShieldStatement GetStatement()
        {
            return statement;
        }
    }
}