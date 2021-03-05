using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;

namespace ARPG.Gear
{
    [System.Serializable]
    public sealed class EquipmentBeardSlot : EquipmentArmorSlot
    {
        public override SlotType GetSlotType()
        {
            return SlotType.Beard;
        }
        
        protected override void UpdateMaterials()
        {
            SkinnedMeshRenderer currentMesh = GetCurrentMesh();

            if (currentMesh == null)
                return;

            Material[] sharedMaterials = currentMesh.sharedMaterials;
            sharedMaterials[0] = hairMaterial;
            currentMesh.sharedMaterials = sharedMaterials;
        }
    }
}


