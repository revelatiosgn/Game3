using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;

namespace ARPG.Gear
{
    [System.Serializable]
    public abstract class EquipmentArmorSlot : EquipmentSlot
    {
        [SerializeField] SkinnedMeshRenderer baseMesh;
        [SerializeField] ArmorItem defaultItem;

        const string namePrefix = "Set Character_";

        public override void Equip(EquipmentItem item, GameObject target)
        {
            base.Equip(item, target);

            ArmorItem armorItem = item as ArmorItem;

            GameObject itemInstance = GameObject.Instantiate(armorItem.prefab);
            itemInstance.name = armorItem.name;

            itemInstance.transform.position = baseMesh.transform.position;
            itemInstance.transform.parent = baseMesh.transform.parent;

            SkinnedMeshRenderer targetMesh = itemInstance.GetComponent<SkinnedMeshRenderer>();
            targetMesh.rootBone = baseMesh.rootBone;
            targetMesh.bones = baseMesh.bones;

            Material[] sharedMaterials = targetMesh.sharedMaterials;
            sharedMaterials[0] = baseMesh.sharedMaterials[0];
            targetMesh.sharedMaterials = sharedMaterials;
        }

        public override void Unequip(GameObject target)
        {
            Transform transform = baseMesh.transform.parent.Find(item.name);
            if (transform != null)
                Destroy(transform.gameObject);

            base.Unequip(target);
        }

        public override void EquipDefault(GameObject target)
        {
            if (defaultItem != null)
            {
                base.Equip(defaultItem, target);
                Equip(defaultItem, target);
            }
        }

        private void MatchTransform(Transform obj, Transform target)
        {
            obj.position = target.position;
            obj.rotation = target.rotation;
        }
    }
}


