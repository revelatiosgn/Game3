using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;
using ARPG.Movement;
using ARPG.Combat;

namespace ARPG.AI
{
    [CreateAssetMenu(menuName = "AI/Decisions/Chase")]
    public class AIChaseDecision : AIDecision
    {
        [SerializeField] AIState combatState;

        public override bool Decide(AIController controller)
        {
            return controller.combatTarget != null;
        }
    }
}


