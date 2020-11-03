using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;

namespace ARPG.Combat
{
    public class WeaponHolder : MonoBehaviour
    {
        public enum HolderType
        {
            LeftHand,
            RightHand,
            Back
        }

        public HolderType holderType;
        public Item item { get; private set; } = null;

        public void Equip(Item item)
        {
            Unequip();

            GameObject gameObject = Instantiate(item.property.prefab);
            gameObject.transform.parent = transform;
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.identity;

            this.item = item;
        }

        public void Unequip()
        {
            item = null;
            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }
    }
}


