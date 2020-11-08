using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Gear;

namespace ARPG.Combat
{
    public class WeaponBehaviour : MonoBehaviour
    {
        protected Animator animator;
        protected Equipment equipment;

        void Awake()
        {
            animator = GetComponent<Animator>();
            equipment = GetComponent<Equipment>();
        }

        public virtual bool AttackBegin() { return false; }
        public virtual void AttackEnd() {}
    }
}
