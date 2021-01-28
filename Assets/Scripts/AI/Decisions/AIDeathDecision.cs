using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;
using ARPG.Movement;
using ARPG.Stats;

namespace ARPG.AI
{
    [CreateAssetMenu(menuName = "AI/Decisions/Death")]
    public class AIDeathDecision : AIDecision
    {
        public override bool Decide(AIController controller)
        {
            return controller.GetComponent<CharacterStats>().health <= 0f;
        }
    }
}


