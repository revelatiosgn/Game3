using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;

namespace ARPG.Gear
{
    [System.Serializable]
    public sealed class EquipmentArrowSlot : EquipmentSlot
    {
        [SerializeField] Transform quiverHolder;
        [SerializeField] Transform arrowHolder;

        public override SlotType GetSlotType()
        {
            return SlotType.Arrow;
        }

        public override void Equip(EquipmentItem item, GameObject target)
        {
            base.Equip(item, target);

            ArrowItem arrowItem = item as ArrowItem;

            if (quiverHolder != null)
                GameObject.Instantiate(arrowItem.quiverPrefab, quiverHolder);

            if (arrowHolder != null)
                GameObject.Instantiate(arrowItem.arrowPrefab, arrowHolder);

            SetArrowActive(false);
        }

        public override void Unequip(GameObject target)
        {
            base.Unequip(target);
            
            if (quiverHolder != null)
            {
                int childCount = quiverHolder.childCount;
                for (int i = childCount - 1; i >= 0; i--)
                    Destroy(quiverHolder.GetChild(i).gameObject);
            }

            if (arrowHolder != null)
            {
                int childCount = arrowHolder.childCount;
                for (int i = childCount - 1; i >= 0; i--)
                    Destroy(arrowHolder.GetChild(i).gameObject);
            }
        }

        public void SetArrowActive(bool isActive)
        {
            if (arrowHolder != null && arrowHolder.childCount > 0)
                arrowHolder.GetChild(0).gameObject.SetActive(isActive);
        }
    }
}


