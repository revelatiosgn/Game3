using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;
using ARPG.Movement;

namespace ARPG.AI
{
    [CreateAssetMenu(menuName = "AI/Decisions/Combat")]
    public class AICombatDecision : AIDecision
    {
        [SerializeField] AIState combatState;

        public override bool Decide(AIController controller)
        {
            GameObject player = GameObject.FindGameObjectWithTag(Constants.Tags.Player);

            float dist = controller.IsState(combatState) ? 103f : 102f;

            return Vector3.Distance(controller.transform.position, player.transform.position) < dist;
        }
    }
}


