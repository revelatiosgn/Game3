using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Gear;
using ARPG.Controller;

namespace ARPG.Combat
{
    public abstract class EquipmentBehaviour
    {
        protected BaseCombat combat;
        protected Equipment equipment;
        protected Animator animator;
        protected BaseController controller;

        public EquipmentBehaviour(BaseCombat combat)
        {
            this.combat = combat;
            this.equipment = combat.GetComponent<Equipment>();
            this.animator = combat.GetComponentInChildren<Animator>();
            this.controller = combat.GetComponent<BaseController>();
        }

        public abstract void Dispose();
        public abstract bool DefenceBegin();
        public abstract bool DefenceEnd();
        public abstract void OnAnimationEvent(string animationEvent);
    }
}


