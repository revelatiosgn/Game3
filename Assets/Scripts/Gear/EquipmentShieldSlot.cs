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

            AddBehaviour(target);
        }

        public override void Unequip(GameObject target)
        {
            int childCount = holder.childCount;
            for (int i = childCount - 1; i >= 0; i--)
                Destroy(holder.GetChild(i).gameObject);

            target.GetComponent<BaseCombat>().ShieldBehaviour = null;

            base.Unequip(target);
        }
        
        public override void AddBehaviour(GameObject target)
        {
            if (item == null)
                return;

            BaseCombat combat = target.GetComponent<BaseCombat>();
            combat.ShieldBehaviour = new ShieldBehaviour(combat);
        }
    }
}


