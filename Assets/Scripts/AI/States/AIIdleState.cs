using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;

namespace ARPG.AI
{
    [CreateAssetMenu(menuName = "AI/States/Idle")]
    public class AIIdleState : AIState
    {
        public override void OnStateEnter(AIController controller)
        {
            controller.aiMovement.Stop();
            controller.aiMovement.SetRunning(false);
        }
    }
}


