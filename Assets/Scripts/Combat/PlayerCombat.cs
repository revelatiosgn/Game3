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
            WeaponBehaviour weaponBehaviour = GetComponent<Equipment>().weaponBehaviour;
            if (!weaponBehaviour)
                return;

            if (InputHandler.attackBeginInput && !playerController.isInteracting)
            {
                playerController.isInteracting = true;
                weaponBehaviour.AttackBegin();
            }

            if (InputHandler.attackEndInput)
            {
                weaponBehaviour.AttackEnd();
            }
        }
    }
}


