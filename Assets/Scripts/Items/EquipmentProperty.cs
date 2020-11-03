using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using ARPG.Combat;

namespace ARPG.Items
{
    public class EquipmentProperty : ItemProperty
    {
        public EquipmentSlot.SlotType slotType;
        public GameObject prefab;

        public override void Use(Item item)
        {
            base.Use(item);

            GameObject player = GameObject.FindGameObjectWithTag(Constants.Tags.Player);
            Equipment equipment = player.GetComponent<Equipment>();
            equipment.Equip(item);
        }
    }
}


