using System.Collections;
using System.Collections.Generic;
using ARPG.Gear;
using UnityEngine;

namespace ARPG.Combat
{
    public class MeleeBehaviour : WeaponBehaviour
    {
        public override bool AttackBegin()
        {
            animator.CrossFade("MeleeAttack", 0.2f);

            return true;
        }

        void SetDamaging(int value)
        {
        }

        void OnDamage(Transform target)
        {
            
        }
    }
}


