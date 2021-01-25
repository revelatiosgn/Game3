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
        
        protected int actionLayerIndex, maskLayerIndex;
        protected float targetMaskLayerWeight = 0f;

        private float maskLayerWeightVel = 0f;

        void Update()
        {
            animator.SetLayerWeight(maskLayerIndex, Mathf.SmoothDamp(animator.GetLayerWeight(maskLayerIndex), targetMaskLayerWeight, ref maskLayerWeightVel, 0.1f));
        } 

        protected virtual void OnEnable()
        {
            item = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Weapon).item as WeaponItem;
            actionLayerIndex = animator.GetLayerIndex(item.actionLayer);
            maskLayerIndex = animator.GetLayerIndex(item.maskLayer);

            animator.SetLayerWeight(actionLayerIndex, 1f);
            targetMaskLayerWeight = 1f;
        }

        protected virtual void OnDisable()
        {
            animator.SetLayerWeight(actionLayerIndex, 0f);
            animator.SetLayerWeight(maskLayerIndex, 0f);
        }
        
        public abstract bool AttackBegin();
        public abstract bool AttackEnd();
        public abstract void AttackComplete();
        public abstract bool DefenceBegin();
        public abstract bool DefenceEnd();
    }
}
