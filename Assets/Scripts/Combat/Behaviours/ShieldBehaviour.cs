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

        public ShieldBehaviour(BaseCombat combat) : base(combat)
        {
            item = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Shield).item as ShieldItem;
            maskLayerIndex = animator.GetLayerIndex(item.maskLayer);

            animator.SetLayerWeight(maskLayerIndex, 1f);
            animator.SetFloat("shield", 1f);
        }

        public override void Dispose()
        {
            animator.SetLayerWeight(maskLayerIndex, 0f);
            animator.SetFloat("shield", 0f);
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
                animator.SetLayerWeight(maskLayerIndex, 0f);
            }
        }
    }
}
