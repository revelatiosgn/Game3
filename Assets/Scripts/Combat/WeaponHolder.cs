using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;

namespace ARPG.Combat
{
    public class WeaponHolder : EquipmentHolder
    {
        public enum Hand
        {
            Left,
            Right
        }

        public Hand hand;

        public override void AttachItem(Item item)
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);

            if (item != null)
            {
                WeaponProperty property = item.property as WeaponProperty;
                if (property.hand == hand)
                {
                    GameObject gameObject = Instantiate(property.prefab);
                    gameObject.transform.parent = transform;
                    gameObject.transform.localPosition = Vector3.zero;
                    gameObject.transform.localRotation = Quaternion.identity;
                }
            }
        }
    }
}


