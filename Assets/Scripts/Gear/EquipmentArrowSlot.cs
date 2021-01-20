using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;

namespace ARPG.Gear
{
    [System.Serializable]
    public sealed class EquipmentArrowSlot : EquipmentSlot
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

            int childCount = holder.childCount;
            for (int i = childCount - 1; i >= 0; i--)
                Destroy(holder.GetChild(i).gameObject);
        }
    }
}


