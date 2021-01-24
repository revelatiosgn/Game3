using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;
using ARPG.Movement;

namespace ARPG.AI
{
    [CreateAssetMenu(menuName = "AI/Decisions/Idle")]
    public class AIIdleDecision : AIDecision
    {
        public override bool Decide(AIController controller)
        {
            return controller.aiMovement.target == null;
        }
    }
}


