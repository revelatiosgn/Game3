using System.Collections;
using System.Collections.Generic;
using ARPG.Gear;
using UnityEngine;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "LegsArmor", menuName = "Items/Equipment/Armor/Legs Armor", order = 1)]
    public class LegsArmorItem : ArmorItem
    {
        public override EquipmentSlot.SlotType GetSlotType()
        {
            return EquipmentSlot.SlotType.LegsArmor;
        }
    }
}

