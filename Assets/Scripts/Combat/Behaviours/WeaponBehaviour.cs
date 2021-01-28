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
        protected int actionLayerIndex;

        public WeaponBehaviour(BaseCombat combat) : base(combat)
        {
            item = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Weapon).item as WeaponItem;
            maskLayerIndex = animator.GetLayerIndex(item.maskLayer);
            actionLayerIndex = animator.GetLayerIndex(item.actionLayer);

            animator.SetLayerWeight(maskLayerIndex, 1f);
            animator.SetLayerWeight(actionLayerIndex, 1f);
        }

        public override void Dispose()
        {   
            animator.SetLayerWeight(maskLayerIndex, 0f);
            animator.SetLayerWeight(actionLayerIndex, 0f);
        }
        
        public abstract bool AttackBegin();
        public abstract bool AttackEnd();
        public abstract void OnAttackComplete();
        public abstract void OnAnimatorIK(int layerIndex);
    }
}
