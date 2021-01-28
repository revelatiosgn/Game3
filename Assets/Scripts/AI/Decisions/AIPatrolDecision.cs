using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;

namespace ARPG.AI
{
    [CreateAssetMenu(menuName = "AI/Decisions/Patrol")]
    public class AIPatrolDecision : AIDecision
    {
        public override bool Decide(AIController controller)
        {
            if (controller.currentStateTime > 2f && controller.waypoints.Length > 0)
                return true;

            return false;
        }
    }
}


