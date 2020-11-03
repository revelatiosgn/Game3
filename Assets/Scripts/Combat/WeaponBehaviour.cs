using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Combat
{
    public class WeaponBehaviour : MonoBehaviour
    {
        protected Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public virtual void AttackBegin() {}
        public virtual void AttackEnd() {}
    }
}
