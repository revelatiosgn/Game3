using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Combat
{
    public class MeleeBehaviour : WeaponBehaviour
    {
        void SetDamaging(int value)
        {

        }

        public override void AttackBegin()
        {
            animator.CrossFade("MeleeAttack", 0.2f);
        }
    }
}


