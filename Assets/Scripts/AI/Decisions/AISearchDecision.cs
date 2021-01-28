using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;

namespace ARPG.AI
{
    [CreateAssetMenu(menuName = "AI/Decisions/Search")]
    public class AISearchDecision : AIDecision
    {
        public override bool Decide(AIController controller)
        {
            if (controller.currentStateTime > 3f || controller.chaseTarget == null || controller.chaseTarget.characterStats.IsDead())
                return false;

            return true;
        }
    }
}


