using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;

namespace ARPG.Gear
{
    [System.Serializable]
    public class EquipmentShieldSlot : EquipmentSlot
    {
        [SerializeField] Transform arm;

        public GameObject currentShield;
        public ShieldStatement defaultShield;

        public override SlotType GetSlotType()
        {
            return SlotType.Shield;
        }

        protected override void Equip()
        {
            ShieldItem shieldItem = Item as ShieldItem;
            currentShield = GameObject.Instantiate(shieldItem.GetStatement().shieldPrefab, arm);
        }

        protected override void Unequip()
        {
            if (currentShield)
                GameObject.Destroy(currentShield);
        }

        protected override EquipmentItem GetDefaultItem()
        {
            if (defaultShield != null)
                return defaultShield.CreateItem();

            return null;
        }
    }
}


