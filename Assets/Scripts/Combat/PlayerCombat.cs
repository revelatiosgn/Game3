using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Core;
using ARPG.Controller;
using ARPG.Items;

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

        void Start()
        {
        }

        void Update()
        {
            Attack();
        }

        void Attack()
        {
            WeaponBehaviour weaponBehaviour = GetComponent<WeaponBehaviour>();
            if (!weaponBehaviour)
                return;

            if (InputHandler.attackBeginInput)
            {
                playerController.isInteracting = weaponBehaviour.AttackBegin();
            }

            if (InputHandler.attackEndInput)
            {
                weaponBehaviour.AttackEnd();
            }
        }
    }
}


