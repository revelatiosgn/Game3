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

            animator = GetComponentInChildren<Animator>();
            aiController = GetComponent<AIController>();
        }

        void Update()
        {
            attackTimer += Time.deltaTime;
        }

        public override void AttackBegin()
        {
            // if (base.AttackBegin())
            // {
            //     attackTimer = 0f;

            //     return true;
            // }

            // return false;
        }
    }
}