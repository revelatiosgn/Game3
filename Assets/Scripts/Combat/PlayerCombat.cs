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

        void Update()
        {
            WeaponBehaviour weaponBehaviour = GetComponent<WeaponBehaviour>();
            if (weaponBehaviour != null)
            {
                if (InputHandler.attackBeginInput)
                    weaponBehaviour.AttackBegin();

                if (InputHandler.attackEndInput)
                    weaponBehaviour.AttackEnd();

                if (InputHandler.defenceBeginInput)
                    weaponBehaviour.DefenceBegin();

                if (InputHandler.defenceEndInput)
                    weaponBehaviour.DefenceEnd();
            }

            ShieldBehaviour shieldBehaviour = GetComponent<ShieldBehaviour>();
            if (shieldBehaviour != null)
            {
                if (InputHandler.defenceBeginInput)
                    shieldBehaviour.DefenceBegin();

                if (InputHandler.defenceEndInput)
                    shieldBehaviour.DefenceEnd();
            }
        }
    }
}


