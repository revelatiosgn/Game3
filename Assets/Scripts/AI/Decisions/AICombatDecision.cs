using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;
using ARPG.Movement;
using ARPG.Combat;

namespace ARPG.AI
{
    [CreateAssetMenu(menuName = "AI/Decisions/Combat")]
    public class AICombatDecision : AIDecision
    {
        [SerializeField] AIState combatState;

        public override bool Decide(AIController controller)
        {
            if (controller.combatTarget != null)
            {
                WeaponBehaviour weaponBehaviour = controller.aiCombat.WeaponBehaviour;
                if (weaponBehaviour == null)
                    return false;

                float dist = controller.IsState(combatState) ? weaponBehaviour.item.range : weaponBehaviour.item.range - 0.1f;
                return Vector3.Distance(controller.transform.position, controller.combatTarget.transform.position) < dist;
            }

            return false;
        }
    }
}


