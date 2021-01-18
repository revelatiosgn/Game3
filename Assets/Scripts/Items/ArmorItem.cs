using System.Collections;
using System.Collections.Generic;
using ARPG.Gear;
using UnityEngine;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "Armor", menuName = "Items/Equipment/Armor", order = 1)]
    public sealed class ArmorItem : EquipmentItem
    {
        public GameObject prefab;

        public override EquipmentSlot.SlotType GetSlotType()
        {
            return EquipmentSlot.SlotType.Armor;
        }
    }
}

