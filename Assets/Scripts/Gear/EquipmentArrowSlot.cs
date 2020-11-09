using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;

namespace ARPG.Gear
{
    [System.Serializable]
    public class EquipmentArrowSlot : EquipmentSlot
    {
        [SerializeField] Transform back;

        public GameObject currentQuiver;
        public ArrowStatement defaultArrow;

        public override SlotType GetSlotType()
        {
            return SlotType.Arrow;
        }

        protected override void Equip()
        {
            ArrowItem arrowItem = Item as ArrowItem;
            currentQuiver = GameObject.Instantiate(arrowItem.GetStatement().quiverPrefab, back);
        }

        protected override void Unequip()
        {
            if (currentQuiver)
                GameObject.Destroy(currentQuiver);
        }

        protected override EquipmentItem GetDefaultItem()
        {
            if (defaultArrow != null)
                return defaultArrow.CreateItem();

            return null;
        }
    }
}


