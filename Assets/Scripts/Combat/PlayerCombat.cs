using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Core;
using ARPG.Controller;

namespace ARPG.Combat
{
    public class PlayerCombat : MonoBehaviour
    {
        Animator animator;
        PlayerController playerController;

        void Awake()
        {
            animator = GetComponent<Animator>();
            playerController = GetComponent<PlayerController>();
        }

        void Update()
        {
            Attack();
        }

        void Attack()
        {
            if (InputHandler.attackInput && !playerController.isInteracting)
            {
                playerController.isInteracting = true;
                animator.CrossFade("Attack", 0.2f);
            }
        }
    }
}


