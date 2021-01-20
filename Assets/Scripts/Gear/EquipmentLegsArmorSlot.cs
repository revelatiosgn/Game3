using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;

namespace ARPG.Gear
{
    [System.Serializable]
    public sealed class EquipmentLegsArmorSlot : EquipmentArmorSlot
    {
        public override SlotType GetSlotType()
        {
            return SlotType.LegsArmor;
        }
    }
}


