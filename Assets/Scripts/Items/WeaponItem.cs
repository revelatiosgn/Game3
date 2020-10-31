using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "WeaponItem", menuName = "Scriptable Objects/Items/Weapon", order = 1)]
    public class WeaponItem : Item
    {
        public GameObject prefab;
        public AnimatorOverrideController animatorOverrideController;

        public override void Use()
        {
            Inventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
            inventory.Equip(this);
        }
    }
}


