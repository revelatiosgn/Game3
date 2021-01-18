using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;

namespace ARPG.Gear
{
    [System.Serializable]
    public class EquipmentShieldSlot : EquipmentSlot
    {
        [SerializeField] Transform holder;

        public override SlotType GetSlotType()
        {
            return SlotType.Shield;
        }

        public override void Equip(EquipmentItem item, GameObject target)
        {
            base.Equip(item, target);

            ShieldItem shieldItem = item as ShieldItem;

            if (holder != null)
                GameObject.Instantiate(shieldItem.shieldPrefab, holder);

            ShieldBehaviour shieldBehaviour = target.GetComponent<ShieldBehaviour>();
            if (shieldBehaviour != null)
                GameObject.DestroyImmediate(shieldBehaviour);
        }

        public override void Unequip(GameObject target)
        {
            base.Unequip(target);

            while(holder.childCount != 0)
                GameObject.DestroyImmediate(holder.GetChild(0).gameObject);
        }
    }
}


