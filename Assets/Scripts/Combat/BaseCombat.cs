using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Gear;
using UnityEngine.Events;

namespace ARPG.Combat
{
    public abstract class BaseCombat : MonoBehaviour
    {
        public enum CombatState
        {
            Idle,
            Combat
        }

        public CombatState combatState;

        public Quaternion aimRotation;
        public Vector3 targetPosition;

        protected Animator animator;
        protected Equipment equipment;

        public UnityAction onCombatStateChanged;
        public UnityAction onAttackBegin;
        public UnityAction onAttackEnd;
        public UnityAction onAttackComplete;

        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
            equipment = GetComponent<Equipment>();
        }

        public virtual void AttackBegin()
        {
            onAttackBegin?.Invoke();
        }

        public virtual void AttackEnd()
        {
            onAttackEnd?.Invoke();
        }

        public virtual void DefenceBegin()
        {
        }

        public virtual void DefenceEnd()
        {
        }

        public virtual void OnAnimationEvent(string animationEvent)
        {
        }

        public virtual void OnAttackComplete()
        {
            onAttackComplete?.Invoke();
        }

        public virtual void OnAnimatorIK(int layerIndex)
        {
        }
    }
}


