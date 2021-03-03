using System.Collections;
using System.Collections.Generic;
using ARPG.Gear;
using UnityEngine;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "FootsArmor", menuName = "Items/Equipment/Armor/Foots Armor", order = 1)]
    public class FootsArmorItem : ArmorItem
    {
        public override EquipmentSlot.SlotType GetSlotType()
        {
            return EquipmentSlot.SlotType.FootsArmor;
        }
    }
}

