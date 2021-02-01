using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;

namespace ARPG.AI
{
    [CreateAssetMenu(menuName = "AI/States/Death")]
    public class AIDeathState : AIState
    {
        public override void OnStateEnter(AIController controller)
        {
            controller.aiMovement.Stop();
            controller.characterCollider.enabled = false;
        }
    }
}


