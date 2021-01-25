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

        public void AttackBegin()
        {
            GetComponent<WeaponBehaviour>()?.AttackBegin();
        }

        public void AttackEnd()
        {
            GetComponent<WeaponBehaviour>()?.AttackEnd();
        }

        public void DefenceBegin()
        {
            GetComponent<WeaponBehaviour>()?.DefenceBegin();
            GetComponent<ShieldBehaviour>()?.DefenceBegin();
        }

        public void DefenceEnd()
        {
            GetComponent<WeaponBehaviour>()?.DefenceEnd();
            GetComponent<ShieldBehaviour>()?.DefenceEnd();
        }
    }
}