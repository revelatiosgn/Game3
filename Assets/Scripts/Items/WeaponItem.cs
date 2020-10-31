using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Combat;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "WeaponItem", menuName = "Scriptable Objects/Items/Weapon", order = 1)]
    public class WeaponItem : Item
    {
        public GameObject prefab;

        public override void Use()
        {
            Equipment equipment = GameObject.FindGameObjectWithTag(Constants.Tags.Player).GetComponent<Equipment>();
            equipment.Equip(Instantiate(prefab).GetComponent<Weapon>());
        }
    }
}


