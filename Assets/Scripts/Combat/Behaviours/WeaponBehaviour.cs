using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Gear;
using ARPG.Controller;
using ARPG.Items;

namespace ARPG.Combat
{
    public abstract class WeaponBehaviour : EquipmentBehaviour
    {
        public WeaponItem item;

        protected int movementLayerIndex;
        protected int attackLayerIndex;
        protected int defenceLayerIndex;

        public WeaponBehaviour(BaseCombat combat) : base(combat)
        {
            item = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Weapon).item as WeaponItem;
            movementLayerIndex = animator.GetLayerIndex(item.animationLayer);
            attackLayerIndex = animator.GetLayerIndex(item.animationLayer + "Attack");
            defenceLayerIndex = animator.GetLayerIndex(item.animationLayer + "Defence");

            animator.SetLayerWeight(movementLayerIndex, 1f);
            animator.SetLayerWeight(attackLayerIndex, 1f);
            animator.SetLayerWeight(defenceLayerIndex, 1f);
        }

        public override void Dispose()
        {   
            animator.SetLayerWeight(movementLayerIndex, 0f);
            animator.SetLayerWeight(attackLayerIndex, 0f);
            animator.SetLayerWeight(defenceLayerIndex, 0f);
        }
        
        public abstract bool AttackBegin();
        public abstract bool AttackEnd();
        public abstract void OnAttackComplete();
        public abstract void OnAnimatorIK(int layerIndex);
    }
}
