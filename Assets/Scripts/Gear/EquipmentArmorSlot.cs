﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;

namespace ARPG.Gear
{
    [System.Serializable]
    public abstract class EquipmentArmorSlot : EquipmentSlot
    {
        [SerializeField] Transform model;
        [SerializeField] ArmorItem defaultItem;

        const string namePrefix = "Set Character_";

        public override void Equip(EquipmentItem item, GameObject target)
        {
            base.Equip(item, target);

            ArmorItem armorItem = item as ArmorItem;

            GameObject itemInstance = GameObject.Instantiate(armorItem.prefab);
            itemInstance.name = itemInstance.name.Substring(0, itemInstance.name.Length - "(Clone)".Length);

            Transform[] allCharacterChildren = model.GetComponentsInChildren<Transform>();
            Transform[] allItemChildren = itemInstance.GetComponentsInChildren<Transform>();
            itemInstance.transform.position = model.position;
            itemInstance.transform.parent = model;

            string[] allItemChildren_NewNames = new string[allItemChildren.Length];

            for(int i = 0; i < allItemChildren.Length; i++)
            {
                //Match and parent bones
                for (int n = 0; n < allCharacterChildren.Length; n++)
                {
                    if(allItemChildren[i].name == allCharacterChildren[n].name)
                    {
                        MatchTransform(allItemChildren[i], allCharacterChildren[n]);
                        allItemChildren[i].parent = allCharacterChildren[n];
                    }
                }

                //Rename
                allItemChildren_NewNames[i] = allItemChildren[i].name;

                if (!allItemChildren[i].name.Contains(namePrefix))
                {
                    allItemChildren_NewNames[i] = namePrefix + allItemChildren[i].name;
                }

                if (!allItemChildren[i].name.Contains(itemInstance.name))
                {
                    allItemChildren_NewNames[i] += "_" + itemInstance.name;
                }
            }

            for(int i = 0; i < allItemChildren.Length; i++)
            {
                allItemChildren[i].name = allItemChildren_NewNames[i];
            }
        }

        public override void Unequip(GameObject target)
        {
            ArmorItem armorItem = item as ArmorItem;

            Transform[] allChildren = model.GetComponentsInChildren<Transform>();
            for (int i = allChildren.Length - 1; i >= 0 ; i--)
            {
                Transform obj = allChildren[i];
                if(obj.name.Contains(namePrefix) && obj.name.Contains(armorItem.prefab.name))
                {
                    Destroy(obj.gameObject);
                }
            }

            base.Unequip(target);
        }

        public override void EquipDefault(GameObject target)
        {
            base.Equip(defaultItem, target);
            Equip(defaultItem, target);
        }

        private void MatchTransform(Transform obj, Transform target)
        {
            obj.position = target.position;
            obj.rotation = target.rotation;
        }
    }
}


