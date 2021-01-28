﻿using System.Collections;
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
            if (controller.chaseTarget)
                controller.aiMovement.Move(controller.chaseTarget.transform.position);
        }
    }
}


