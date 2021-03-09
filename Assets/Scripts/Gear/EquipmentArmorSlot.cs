using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;
using ARPG.Events;

namespace ARPG.Gear
{
    [System.Serializable]
    public class EquipmentArmorSlot : EquipmentSlot
    {
        [System.Serializable]
        public class ArmorHolder
        {
            public ArmorItem.ArmorType type;
            public ArmorItem item;
            public SkinnedMeshRenderer mesh;
            public SkinnedMeshRenderer defaultMesh;
        }

        [SerializeField] SkinnedMeshRenderer baseMesh;
        [SerializeField] Material skinMaterial;
        [SerializeField] List<ArmorHolder> armorHolders;
        [SerializeField] List<SkinnedMeshRenderer> baseMeshes;

        public EquipmentArmorSlot()
        {
            type = Type.Armor;
        }

        public override bool Equip(Equipment equipment, EquipmentItem item)
        {
            ArmorItem armorItem = item as ArmorItem;
            if (armorItem == null)
                return false;

            ArmorHolder armorHolder = armorHolders.Find(armorHolder => armorHolder.type == armorItem.armorType);
            if (armorHolder == null)
                return false;

            Unequip(equipment, armorItem.armorType);

            GameObject itemInstance = GameObject.Instantiate(armorItem.mesh.gameObject, baseMesh.transform.parent);
            itemInstance.name = armorItem.name.ToString();
            itemInstance.transform.position = baseMesh.transform.position;
            itemInstance.transform.parent = baseMesh.transform.parent;

            SkinnedMeshRenderer mesh = itemInstance.GetComponent<SkinnedMeshRenderer>();
            mesh.rootBone = baseMesh.rootBone;
            mesh.bones = baseMesh.bones;
            armorHolder.item = armorItem;
            armorHolder.mesh = mesh;

            Material[] sharedMaterials = mesh.sharedMaterials;
            sharedMaterials[0] = skinMaterial;
            mesh.sharedMaterials = sharedMaterials;

            if (armorHolder.defaultMesh != null)
                armorHolder.defaultMesh.gameObject.SetActive(false);

            equipment.onEquip.RaiseEvent(armorHolder.item, equipment.gameObject);

            return true;
        }

        public override bool Unequip(Equipment equipment, EquipmentItem item)
        {
            ArmorItem armorItem = item as ArmorItem;
            if (armorItem == null)
                return false;

            return Unequip(equipment, armorItem.armorType);
        }

        public bool Unequip(Equipment equipment, ArmorItem.ArmorType armorType)
        {
            ArmorHolder armorHolder = armorHolders.Find(armorHolder => armorHolder.type == armorType);

            if (armorHolder == null || armorHolder.mesh == null)
                return false;

            equipment.onUnequip.RaiseEvent(armorHolder.item, equipment.gameObject);

            Destroy(armorHolder.mesh.gameObject);
            armorHolder.item = null;
            armorHolder.mesh = null;
            
            if (armorHolder.defaultMesh != null)
                armorHolder.defaultMesh.gameObject.SetActive(true);

            return true;
        }

        public override bool IsEquipped(EquipmentItem item)
        {
            return armorHolders.Find(armorHolder => armorHolder.item == item) != null;
        }
        
        public override void Update(Equipment equipment)
        {
            foreach (ArmorHolder armorHolder in armorHolders)
            {
                if (armorHolder.mesh != null)
                {
                    Material[] sharedMaterials = armorHolder.mesh.sharedMaterials;
                    sharedMaterials[0] = skinMaterial;
                    armorHolder.mesh.sharedMaterials = sharedMaterials;
                }
                
                if (armorHolder.defaultMesh != null)
                {
                    Material[] sharedMaterials = armorHolder.defaultMesh.sharedMaterials;
                    sharedMaterials[0] = skinMaterial;
                    armorHolder.defaultMesh.sharedMaterials = sharedMaterials;
                }
            }

            foreach (SkinnedMeshRenderer baseMesh in baseMeshes)
            {
                if (baseMesh == null)
                    continue;

                Material[] sharedMaterials = baseMesh.sharedMaterials;
                sharedMaterials[0] = skinMaterial;
                baseMesh.sharedMaterials = sharedMaterials;
            }
        }
    }
}
