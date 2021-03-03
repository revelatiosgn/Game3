using System.Collections;
using System.Collections.Generic;
using ARPG.Gear;
using UnityEngine;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "Shield", menuName = "Items/Equipment/Weapon/Shield", order = 1)]
    public sealed class ShieldItem : EquipmentItem
    {
        public GameObject prefab;
        public float baseArmor;
        public string animationLayer = "Shield";

        public override EquipmentSlot.SlotType GetSlotType()
        {
            return EquipmentSlot.SlotType.Shield;
        }
    }
}

