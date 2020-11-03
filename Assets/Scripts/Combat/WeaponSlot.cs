using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;

namespace ARPG.Combat
{
    public class WeaponSlot : EquipmentSlot
    {
        public enum Hand
        {
            Left,
            Right
        }

        public Hand hand;

        WeaponSlot()
        {
            slotType = SlotType.Weapon;
        }

        protected override void AttachItem(Item item)
        {
            if (item != null)
            {
                EquipmentProperty property = item.property as EquipmentProperty;
                GameObject gameObject = Instantiate(property.prefab);
                gameObject.transform.parent = transform;
                gameObject.transform.localPosition = Vector3.zero;
                gameObject.transform.localRotation = Quaternion.identity;
            }
        }

        protected override void DetachItem()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }
    }
}


