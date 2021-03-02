using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Combat
{
    public class WeaponState : StateMachineBehaviour
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animator.GetLayerWeight(layerIndex) > 0f)
                animator.GetComponent<BaseCombat>()?.OnAttackComplete();
        }
    }
}

