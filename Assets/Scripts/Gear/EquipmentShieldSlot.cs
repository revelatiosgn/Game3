using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;
using ARPG.Combat;

namespace ARPG.Gear
{
    [System.Serializable]
    public sealed class EquipmentShieldSlot : EquipmentSlot
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
                GameObject.Instantiate(shieldItem.prefab, holder);

            ShieldBehaviour shieldBehaviour = target.GetComponent<ShieldBehaviour>();
            if (shieldBehaviour != null)
                Destroy(shieldBehaviour);

            target.AddComponent<ShieldBehaviour>();
        }

        public override void Unequip(GameObject target)
        {
            int childCount = holder.childCount;
            for (int i = childCount - 1; i >= 0; i--)
                Destroy(holder.GetChild(i).gameObject);

            ShieldBehaviour weaponBehaviour = target.GetComponent<ShieldBehaviour>();
            if (weaponBehaviour != null)
                Destroy(weaponBehaviour);

            base.Unequip(target);
        }
    }
}


