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
            Weapon currentWeapon = GetComponent<Equipment>().currentWeapon;
            if (!currentWeapon)
                return;
            
            if (currentWeapon.weaponType == Weapon.WeaponType.Melee)
            {
                if (InputHandler.attackInput && !playerController.isInteracting)
                {
                    playerController.isInteracting = true;
                    animator.SetBool("meleeAttack", true);
                }
                else
                {
                    animator.SetBool("meleeAttack", false);
                }
            }
            else if (currentWeapon.weaponType == Weapon.WeaponType.Ranged)
            {
                if (InputHandler.attackInput && !playerController.isInteracting && !animator.GetBool("rangedAttack"))
                {
                    playerController.isInteracting = true;
                    animator.SetBool("rangedAttack", true);
                }
                else if (!InputHandler.attackInput && animator.GetBool("rangedAttack"))
                {
                    animator.SetBool("rangedAttack", false);
                }
            }
        }
    }
}


