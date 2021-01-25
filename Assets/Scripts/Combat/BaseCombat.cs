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

        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public abstract void AttackBegin();
        public abstract void AttackEnd();
        public abstract void DefenceBegin();
        public abstract void DefenceEnd();
    }
}


