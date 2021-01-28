using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;

namespace ARPG.AI
{
    [CreateAssetMenu(menuName = "AI/Decisions/Target Dead")]
    public class AITargetDeadDecision : AIDecision
    {
        public override bool Decide(AIController controller)
        {
            if (controller.chaseTarget != null && controller.chaseTarget.characterStats.health <= 0)
                return true;

            return false;
        }
    }
}


