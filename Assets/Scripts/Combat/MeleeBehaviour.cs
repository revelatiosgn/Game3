using System.Collections;
using System.Collections.Generic;
using ARPG.Gear;
using UnityEngine;

using ARPG.Core;
using ARPG.Movement;

namespace ARPG.Combat
{
    public class MeleeBehaviour : WeaponBehaviour
    {
        public override bool AttackBegin()
        {
            animator.SetTrigger("attackBegin");

            return true;
        }

        public override void AttackEnd()
        {
        }

        public override bool DefenceBegin()
        {
            animator.SetBool("aim", true);
            GetComponent<PlayerMovement>().state = PlayerMovement.MovementState.Aim;

            return true;
        }

        public override void DefenceEnd()
        {
            animator.SetBool("aim", false);
            GetComponent<PlayerMovement>().state = PlayerMovement.MovementState.Regular;
        }
    }
}


