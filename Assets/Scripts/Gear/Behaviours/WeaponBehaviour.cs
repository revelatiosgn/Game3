using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Gear;
using ARPG.Controller;
using ARPG.Items;
using ARPG.Combat;

namespace ARPG.Gear
{
    public abstract class WeaponBehaviour : MonoBehaviour
    {
        protected EquipmentWeaponSlot slot;
        protected BaseCombat baseCombat;
        protected BaseController baseController;
        protected Animator animator;

        protected int movementLayerIndex;
        protected int attackLayerIndex;

        public virtual void Init(WeaponItem weaponItem, GameObject target)
        {
            baseCombat = target.GetComponent<BaseCombat>();
            animator = target.GetComponent<Animator>();

            movementLayerIndex = animator.GetLayerIndex(weaponItem.GetAnimatorLayer());
            attackLayerIndex = animator.GetLayerIndex(weaponItem.GetAnimatorLayer() + "Attack");

            baseCombat.onAttackBegin += OnAttackBegin;
            baseCombat.onAttackEnd += OnAttackEnd;
            baseCombat.onAttackComplete += OnAttackComplete;

            Activate();
        }

        void OnDisable()
        {
            baseCombat.onAttackBegin -= OnAttackBegin;
            baseCombat.onAttackEnd -= OnAttackEnd;
            baseCombat.onAttackComplete -= OnAttackComplete;

            Dispose();
        }

        public virtual void Activate()
        {   
            animator.SetLayerWeight(movementLayerIndex, 0f);
            animator.SetLayerWeight(attackLayerIndex, 0f);
        }

        public virtual void Dispose()
        {
            animator.SetLayerWeight(movementLayerIndex, 0f);
            animator.SetLayerWeight(attackLayerIndex, 0f);
        }

        protected abstract void OnAttackBegin();
        protected abstract void OnAttackEnd();
        protected abstract void OnAttackComplete();
    }
}
