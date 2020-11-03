using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Combat
{
    public class RangedBehaviour : WeaponBehaviour
    {
        private bool isAttackEneded;
        private bool isReadyToLaunch;
        private GameObject arrow;

        public override void AttackBegin()
        {
            isAttackEneded = false;
            isReadyToLaunch = false;
            arrow = null;

            animator.Play("RangedAttackGetMissile");
        }
        
        public override void AttackEnd()
        {
            isAttackEneded = true;
            if (isAttackEneded && isReadyToLaunch)
                animator.Play("RangedAttackEnd");
        }

        public void ReadyToLaunch()
        {
            isReadyToLaunch = true;
            if (isAttackEneded && isReadyToLaunch)
                animator.Play("RangedAttackEnd");
        }

        public void LaunchMissile()
        {
            // Equipment equipment = GetComponent<Equipment>();
            // GameObject arrow = Instantiate(equipment.arrow);
            // arrow.transform.position = equipment.GetWeaponHolder(WeaponHolder.HolderType.RightHand).transform.position;
            // arrow.transform.rotation = transform.rotation;
        }
    }
}


