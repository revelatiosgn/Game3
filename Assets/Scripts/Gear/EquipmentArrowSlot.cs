using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;

namespace ARPG.Gear
{
    [System.Serializable]
    public class EquipmentArrowSlot : EquipmentSlot
    {
        [SerializeField] Transform holder;

        public override SlotType GetSlotType()
        {
            return SlotType.Arrow;
        }

        public override void Equip(EquipmentItem item, GameObject target)
        {
            base.Equip(item, target);

            ArrowItem arrowItem = item as ArrowItem;

            if (holder != null)
                GameObject.Instantiate(arrowItem.quiverPrefab, holder);
        }

        public override void Unequip(GameObject target)
        {
            base.Unequip(target);

            while(holder.childCount != 0)
                GameObject.DestroyImmediate(holder.GetChild(0).gameObject);
        }
    }
}


