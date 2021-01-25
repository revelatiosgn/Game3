using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;

namespace ARPG.Combat
{
    public class AICombat : BaseCombat
    {
        AIController aiController;

        public float attackTimer = 0f;

        protected override void Awake()
        {
            base.Awake();

            animator = GetComponent<Animator>();
            aiController = GetComponent<AIController>();
        }

        void Update()
        {
            attackTimer += Time.deltaTime;
        }

        public override void AttackBegin()
        {
            WeaponBehaviour weaponBehaviour = GetComponent<WeaponBehaviour>();
            if (weaponBehaviour && weaponBehaviour.AttackBegin())
                attackTimer = 0f;
        }

        public override void AttackEnd()
        {
            GetComponent<WeaponBehaviour>()?.AttackEnd();
        }

        public override void DefenceBegin()
        {
            GetComponent<WeaponBehaviour>()?.DefenceBegin();
            GetComponent<ShieldBehaviour>()?.DefenceBegin();
        }

        public override void DefenceEnd()
        {
            GetComponent<WeaponBehaviour>()?.DefenceEnd();
            GetComponent<ShieldBehaviour>()?.DefenceEnd();
        }
    }
}