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
        WeaponItem item;
        int actionLayerIndex, maskLayerIndex;

        protected virtual void OnEnable()
        {
            item = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Weapon).item as WeaponItem;
            actionLayerIndex = animator.GetLayerIndex(item.actionLayer);
            maskLayerIndex = animator.GetLayerIndex(item.maskLayer);

            animator.SetLayerWeight(actionLayerIndex, 1f);
            animator.SetLayerWeight(maskLayerIndex, 1f);
        }

        protected virtual void OnDisable()
        {
            animator.SetLayerWeight(actionLayerIndex, 0f);
            animator.SetLayerWeight(maskLayerIndex, 0f);
        }
        
        public abstract void AttackBegin();
        public abstract void AttackEnd();
        public abstract void AttackComplete();
        public abstract void DefenceBegin();
        public abstract void DefenceEnd();
    }
}
