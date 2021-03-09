using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Gear;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "Quiver", menuName = "Items/Equipment/Quiver", order = 1)]
    public class QuiverItem : EquipmentItem
    {
        public override EquipmentSlot.Type GetSlotType()
        {
            return EquipmentSlot.Type.Quiver;
        }
    }
}

