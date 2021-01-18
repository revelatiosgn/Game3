using System.Collections;
using System.Collections.Generic;
using ARPG.Gear;
using UnityEngine;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "Arrow", menuName = "Items/Equipment/Arrow", order = 1)]
    public sealed class ArrowItem : EquipmentItem
    {
        public GameObject arrowPrefab;
        public GameObject quiverPrefab;
        public float baseDamage;
        [Range(0f, 100f)] public float speed = 20f;
        [Range(0f, 10f)] public float gravity = 9.8f;
        public Vector3 launchOffset;

        public override EquipmentSlot.SlotType GetSlotType()
        {
            return EquipmentSlot.SlotType.Arrow;
        }
    }
}

