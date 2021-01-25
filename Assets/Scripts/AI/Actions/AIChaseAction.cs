using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;

namespace ARPG.AI
{
    [CreateAssetMenu(menuName = "AI/Actions/Chase")]
    public class AIChaseAction : AIAction
    {
        public override void Act(AIController controller)
        {
            controller.aiMovement.Move(GameObject.FindGameObjectWithTag(Constants.Tags.Player).transform.position);
            controller.aiMovement.SetRunning(true);
        }
    }
}


