using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;
using ARPG.Movement;

namespace ARPG.AI
{
    [CreateAssetMenu(menuName = "AI/Actions/Attack")]
    public class AIAttackAction : AIAction
    {
        public override void Act(AIController controller)
        {
            controller.aiMovement.target = null;

            GameObject player = GameObject.FindGameObjectWithTag(Constants.Tags.Player);
            controller.transform.LookAt(player.transform, Vector3.up);
            controller.aiCombat.AttackBegin();
        }
    }
}


