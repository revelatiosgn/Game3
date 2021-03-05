using System.Collections;
using System.Collections.Generic;
using ARPG.Gear;
using UnityEngine;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "ChestArmor", menuName = "Items/Equipment/Armor/Head Armor", order = 1)]
    public class HeadArmorItem : ArmorItem
    {
        public bool isHair = false;

        public override EquipmentSlot.SlotType GetSlotType()
        {
            return EquipmentSlot.SlotType.HeadArmor;
        }
    }
}

