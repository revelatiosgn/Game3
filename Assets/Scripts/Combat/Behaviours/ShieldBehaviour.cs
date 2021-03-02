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
        ShieldItem item;

        private int movementLayerIndex;
        private int defenceLayerIndex;

        public ShieldBehaviour(BaseCombat combat) : base(combat)
        {
            item = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Shield).item as ShieldItem;

            movementLayerIndex = animator.GetLayerIndex(item.animationLayer);
            defenceLayerIndex = animator.GetLayerIndex(item.animationLayer + "Defence");

            animator.SetLayerWeight(movementLayerIndex, 1f);
            animator.SetLayerWeight(defenceLayerIndex, 1f);
            animator.SetBool("shield", true);
        }

        public override void Dispose()
        {
            animator.SetLayerWeight(movementLayerIndex, 0f);
            animator.SetLayerWeight(defenceLayerIndex, 0f);
            animator.SetBool("shield", false);
        }

        public override bool DefenceBegin()
        {
            animator.SetBool("defence", true);

            return true;
        }

        public override bool DefenceEnd()
        {
            animator.SetBool("defence", false);

            return true;
        }

        public override void OnAnimationEvent(string animationEvent)
        {
            if (animationEvent == "Death")
            {
                animator.SetLayerWeight(movementLayerIndex, 0f);
                animator.SetLayerWeight(defenceLayerIndex, 0f);
            }
        }
    }
}
