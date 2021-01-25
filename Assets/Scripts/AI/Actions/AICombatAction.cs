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
        public override void OnStateEnter(AIController controller)
        {
            controller.aiCombat.AttackBegin();
        }

        public override void OnStateExit(AIController controller)
        {
            controller.aiCombat.DefenceEnd();
        }

        public override void Act(AIController controller)
        {
            controller.aiMovement.Stop();

            GameObject player = GameObject.FindGameObjectWithTag(Constants.Tags.Player);
            controller.aiCombat.targetPosition = player.transform.position + Vector3.up * 1.5f;
            Vector3 lookPosition = player.transform.position;
            lookPosition.y = controller.transform.position.y;
            controller.transform.LookAt(lookPosition, Vector3.up);
            // controller.aiCombat.DefenceBegin();

            if (controller.aiCombat.attackTimer > 2f)
            {
                controller.aiCombat.AttackBegin();
                controller.aiCombat.AttackEnd();
            }

            // if (controller.aiCombat.attackTimer > 0f)

            controller.aiCombat.aimRotation = Quaternion.LookRotation(player.transform.position - controller.transform.position, Vector3.up);
        }
    }
}


