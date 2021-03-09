using System.Collections;
using System.Collections.Generic;
using ARPG.Combat;
using ARPG.Gear;
using UnityEngine;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "Shield", menuName = "Items/Equipment/Shield", order = 1)]
    public class ShieldItem : EquipmentItem
    {
        public ShieldBehaviour prefab;

        public override EquipmentSlot.Type GetSlotType()
        {
            return EquipmentSlot.Type.Shield;
        }
    }
}

