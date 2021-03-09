using System.Collections;
using System.Collections.Generic;
using ARPG.Gear;
using UnityEngine;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "Armor", menuName = "Items/Equipment/Armor", order = 1)]
    public class ArmorItem : EquipmentItem
    {
        public enum ArmorType
        {
            Head,
            Chest,
            Hands,
            Legs,
            Foots
        }

        public ArmorType armorType;
        public SkinnedMeshRenderer mesh;

        public override EquipmentSlot.Type GetSlotType()
        {
            return EquipmentSlot.Type.Armor;
        }
    }
}

