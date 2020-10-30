using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG
{
    public class MovementBehaviour : StateMachineBehaviour
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PlayerController playerController = animator.GetComponent<PlayerController>();
            if (playerController)
            {
                playerController.isInteracting = false;
            }
        }
    }
}

