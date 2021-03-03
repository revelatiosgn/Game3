using System.Collections;
using System.Collections.Generic;
using ARPG.Gear;
using UnityEngine;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "HandsArmor", menuName = "Items/Equipment/Armor/Hands Armor", order = 1)]
    public class HandsArmorItem : ArmorItem
    {
        public override EquipmentSlot.SlotType GetSlotType()
        {
            return EquipmentSlot.SlotType.HandsArmor;
        }
    }
}

