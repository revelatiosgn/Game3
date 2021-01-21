using System.Collections;
using System.Collections.Generic;
using ARPG.Gear;
using UnityEngine;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "Shield", menuName = "Items/Equipment/Shield", order = 1)]
    public sealed class ShieldItem : EquipmentItem
    {
        public GameObject prefab;
        public float baseArmor;
        public string maskLayer = "ShieldMask";

        public override EquipmentSlot.SlotType GetSlotType()
        {
            return EquipmentSlot.SlotType.Shield;
        }
    }
}

