using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;

namespace ARPG.AI
{
    [CreateAssetMenu(menuName = "AI/States/Chase")]
    public class AIChaseState : AIState
    {
        public override void OnStateEnter(AIController controller)
        {
            controller.combatTarget = controller.charactersCanSee.Find(target => {
                if (target.characterGroup != controller.characterGroup)
                    return true;
                return false;
            });

            if (controller.combatTarget)
                controller.aiMovement.SetRunning(true);
        }
    }
}


