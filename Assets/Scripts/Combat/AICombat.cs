using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;

namespace ARPG.Combat
{
    public class AICombat : MonoBehaviour
    {
        Animator animator;
        AIController aiController;

        void Awake()
        {
            animator = GetComponent<Animator>();
            aiController = GetComponent<AIController>();
        }

        void Update()
        {
            WeaponBehaviour weaponBehaviour = GetComponent<WeaponBehaviour>();
            if (weaponBehaviour == null)
                return;

            // if (aiController.state == AIController.State.Attack)
            //     weaponBehaviour.AttackBegin();
        }
    }
}