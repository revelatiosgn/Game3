using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Movement;
using ARPG.Items;
using ARPG.Gear;

namespace ARPG.Combat
{
    public class ShieldBehaviour : EquipmentBehaviour
    {
        void OnEnable()
        {
            ShieldItem shieldItem = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Shield).item as ShieldItem;
            animator.SetLayerWeight(animator.GetLayerIndex(shieldItem.maskLayer), 1f);
            animator.SetFloat("shield", 1f);
        }

        void OnDisable()
        {
            ShieldItem shieldItem = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Shield).item as ShieldItem;
            animator.SetLayerWeight(animator.GetLayerIndex(shieldItem.maskLayer), 0f);
            animator.SetFloat("shield", 0f);
        }

        public bool DefenceBegin()
        {
            animator.SetBool("defence", true);

            return true;
        }

        public bool DefenceEnd()
        {
            animator.SetBool("defence", false);

            return true;
        }
    }
}
