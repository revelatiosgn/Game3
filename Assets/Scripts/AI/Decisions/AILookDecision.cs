using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;

namespace ARPG.AI
{
    [CreateAssetMenu(menuName = "AI/Decisions/Look")]
    public class AILookDecision : AIDecision
    {
        public override bool Decide(AIController controller)
        {
            BaseController targetController = controller.charactersCanSee.Find(target => {
                if (target.characterGroup != controller.characterGroup)
                    return true;
                return false;
            });

            if (targetController == null)
                Debug.Log("CANT SEE");

            if (targetController != null && !targetController.characterStats.IsDead())
                return true;

            return false;
        }
    }
}


