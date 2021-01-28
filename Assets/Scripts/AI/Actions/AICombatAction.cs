using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;
using ARPG.Movement;

namespace ARPG.AI
{
    [CreateAssetMenu(menuName = "AI/Actions/Combat")]
    public class AICombatAction : AIAction
    {
        public override void Act(AIController controller)
        {
            controller.aiMovement.Stop();

            if (controller.chaseTarget == null)
                return;

            Transform chaseTarget = controller.chaseTarget.transform;
            controller.aiCombat.targetPosition = chaseTarget.position + Vector3.up * 1.5f;
            Vector3 lookPosition = chaseTarget.position;
            lookPosition.y = controller.transform.position.y;
            controller.transform.LookAt(lookPosition, Vector3.up);
            // controller.aiCombat.DefenceBegin();

            if (controller.aiCombat.attackTimer > 20f)
            {
                controller.aiCombat.AttackBegin();
                controller.aiCombat.AttackEnd();
            }

            // if (controller.aiCombat.attackTimer > 0f)

            controller.aiCombat.aimRotation = Quaternion.LookRotation(chaseTarget.position - controller.transform.position, Vector3.up);
        }
    }
}


