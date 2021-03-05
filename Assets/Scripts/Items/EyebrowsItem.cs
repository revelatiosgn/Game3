using System.Collections;
using System.Collections.Generic;
using ARPG.Gear;
using UnityEngine;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "Eyebrows", menuName = "Items/Equipment/Armor/Eyebrows", order = 1)]
    public class EyebrowsItem : ArmorItem
    {
        public override EquipmentSlot.SlotType GetSlotType()
        {
            return EquipmentSlot.SlotType.Eyebrows;
        }
    }
}

