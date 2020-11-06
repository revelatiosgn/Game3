using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;

namespace ARPG.Equipment
{
    [System.Serializable]
    public class EquipmentWeaponSlot : EquipmentSlot
    {
        public Transform leftHand;
        public Transform rightHand;

        public override SlotType GetSlotType()
        {
            return SlotType.Weapon;
        }
    }
}


