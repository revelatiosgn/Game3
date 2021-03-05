using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;

namespace ARPG.Gear
{
    [System.Serializable]
    public class EquipmentSlot
    {
        public enum SlotType
        {
            None,
            Weapon,
            Arrow,
            Shield,
            ChestArmor,
            LegsArmor,
            FootsArmor,
            HandsArmor,
            HeadArmor,
            Eyebrows,
            Beard,
            Face
        }

        public EquipmentItem item;

        protected Material skinMaterial;
        protected Material hairMaterial;

        public virtual SlotType GetSlotType()
        {
            return SlotType.None;
        }

        public virtual void Equip(EquipmentItem item, GameObject target)
        {
            this.item = item;
        }

        public virtual void Unequip(GameObject target)
        {
            this.item = null;
        }

        public virtual void EquipDefault(GameObject target)
        {
        }

        public virtual void AddBehaviour(GameObject target)
        {
        }

        public void SetMaterials(Material skinMaterial, Material hairMaterial)
        {
            this.skinMaterial = skinMaterial;
            this.hairMaterial = hairMaterial;
            
            UpdateMaterials();
        }

        protected virtual void UpdateMaterials()
        {
        }

        protected void Destroy(Object obj)
        {
            if (Application.isPlaying)
                GameObject.Destroy(obj);
            else
                GameObject.DestroyImmediate(obj);
        }
    }
}


