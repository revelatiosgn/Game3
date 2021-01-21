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

        public void DefenceBegin()
        {
            animator.SetBool("defence", true);
            GetComponent<PlayerMovement>().state = PlayerMovement.MovementState.Aim;
        }

        public void DefenceEnd()
        {
            animator.SetBool("defence", false);
            GetComponent<PlayerMovement>().state = PlayerMovement.MovementState.Regular;
        }
    }
}
