using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Combat
{
    public abstract class BaseCombat : MonoBehaviour
    {
        public Quaternion aimRotation;
        public Vector3 targetPosition;

        protected Animator animator;

        private WeaponBehaviour weaponBehaviour;
        private ShieldBehaviour shieldBehaviour;

        public WeaponBehaviour WeaponBehaviour
        {
            set
            {
                if (weaponBehaviour != null)
                    weaponBehaviour.Dispose();
                weaponBehaviour = value;
            }

            get
            {
                return weaponBehaviour;
            }
        }

        public ShieldBehaviour ShieldBehaviour
        {
            set
            {
                if (shieldBehaviour != null)
                    shieldBehaviour.Dispose();
                shieldBehaviour = value;
            }

            get
            {
                return shieldBehaviour;
            }
        }

        protected virtual void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }

        public virtual bool AttackBegin()
        {
            return weaponBehaviour != null && weaponBehaviour.AttackBegin();
        }

        public virtual bool AttackEnd()
        {
            return weaponBehaviour != null && weaponBehaviour.AttackEnd();
        }

        public virtual bool DefenceBegin()
        {
            if (shieldBehaviour != null && shieldBehaviour.DefenceBegin())
                return true;

            if (weaponBehaviour != null && weaponBehaviour.DefenceBegin())
                return true;

            return false;
        }

        public virtual bool DefenceEnd()
        {
            if (shieldBehaviour != null && shieldBehaviour.DefenceEnd())
                return true;

            if (weaponBehaviour != null && weaponBehaviour.DefenceEnd())
                return true;

            return false;
        }

        public virtual void OnAnimationEvent(string animationEvent)
        {
            weaponBehaviour?.OnAnimationEvent(animationEvent);
        }

        public virtual void OnAttackComplete()
        {
            weaponBehaviour?.OnAttackComplete();
        }

        public virtual void OnAnimatorIK(int layerIndex)
        {
            weaponBehaviour?.OnAnimatorIK(layerIndex);
        }
    }
}


