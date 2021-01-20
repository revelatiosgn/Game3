using System.Collections;
using System.Collections.Generic;
using ARPG.Gear;
using UnityEngine;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "ChestArmor", menuName = "Items/Equipment/Chest Armor", order = 1)]
    public class ChestArmorItem : ArmorItem
    {
        public override EquipmentSlot.SlotType GetSlotType()
        {
            return EquipmentSlot.SlotType.ChestArmor;
        }
    }
}

