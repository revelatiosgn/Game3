using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;

namespace ARPG.AI
{
    [CreateAssetMenu(menuName = "AI/Actions/Idle")]
    public class AIIdleAction : AIAction
    {
        public override void Act(AIController controller)
        {
            controller.aiMovement.target = null;
            controller.aiMovement.SetRunning(false);
        }
    }
}


