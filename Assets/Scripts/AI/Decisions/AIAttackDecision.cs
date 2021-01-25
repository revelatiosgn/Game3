using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;
using ARPG.Movement;

namespace ARPG.AI
{
    [CreateAssetMenu(menuName = "AI/Decisions/Attack")]
    public class AIAttackDecision : AIDecision
    {
        public override bool Decide(AIController controller)
        {
            GameObject player = GameObject.FindGameObjectWithTag(Constants.Tags.Player);
            return Vector3.Distance(controller.transform.position, player.transform.position) < 3f;
        }
    }
}


