using System.Collections;
using System.Collections.Generic;
using ARPG.Gear;
using UnityEngine;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "Shield", menuName = "Items/Equipment/Shield", order = 1)]
    public sealed class ShieldItem : EquipmentItem
    {
        public GameObject shieldPrefab;
        public float baseArmor;
        public string layerName = "";

        public override EquipmentSlot.SlotType GetSlotType()
        {
            return EquipmentSlot.SlotType.Shield;
        }
    }
}

