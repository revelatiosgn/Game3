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

        public override void Equip(EquipmentItem item, GameObject target)
        {
            base.Equip(item, target);

            ArmorItem armorItem = item as ArmorItem;

            GameObject itemInstance = GameObject.Instantiate(armorItem.prefab);
            itemInstance.name = GetSlotType().ToString();

            itemInstance.transform.position = baseMesh.transform.position;
            itemInstance.transform.parent = baseMesh.transform.parent;

            SkinnedMeshRenderer currentMesh = itemInstance.GetComponent<SkinnedMeshRenderer>();
            currentMesh.rootBone = baseMesh.rootBone;
            currentMesh.bones = baseMesh.bones;

            UpdateMaterials();
        }

        public override void Unequip(GameObject target)
        {
            Transform transform = baseMesh.transform.parent.Find(GetSlotType().ToString());
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

        protected override void UpdateMaterials()
        {
            SkinnedMeshRenderer currentMesh = GetCurrentMesh();
            if (currentMesh == null)
                return;

            Material[] sharedMaterials = currentMesh.sharedMaterials;
            sharedMaterials[0] = skinMaterial;
            currentMesh.sharedMaterials = sharedMaterials;
        }

        protected SkinnedMeshRenderer GetCurrentMesh()
        {
            Transform transform = baseMesh.transform.parent.Find(GetSlotType().ToString());
            if (transform == null)
                return null;

            return transform.GetComponent<SkinnedMeshRenderer>();
        }
    }
}


