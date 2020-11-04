using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;

namespace ARPG.Combat
{
    public class ArrowsHolder : EquipmentHolder
    {
        public override void AttachItem(Item item)
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);

            if (item != null)
            {
                ArrowProperty property = item.property as ArrowProperty;
                GameObject gameObject = Instantiate(property.prefab);
                gameObject.transform.parent = transform;
                gameObject.transform.localPosition = Vector3.zero;
                gameObject.transform.localRotation = Quaternion.identity;
            }
        }
    }
}


