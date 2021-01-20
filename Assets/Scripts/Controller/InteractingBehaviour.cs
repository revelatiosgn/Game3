using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Controller
{
    public class InteractingBehaviour : StateMachineBehaviour
    {
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PlayerController playerController = animator.GetComponent<PlayerController>();
            if (playerController)
            {
            }
        }
    }
}

