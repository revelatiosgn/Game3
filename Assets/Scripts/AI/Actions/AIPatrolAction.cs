using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;
using ARPG.Movement;

namespace ARPG.AI
{
    [CreateAssetMenu(menuName = "AI/Actions/Patrol")]
    public class AIPatrolAction : AIAction
    {
        public override void OnStateEnter(AIController controller)
        {
            if (controller.waypoints.Length > 0)
            {
                controller.waypointIndex = (controller.waypointIndex + 1) % controller.waypoints.Length;
                controller.aiMovement.Move(controller.waypoints[controller.waypointIndex].position);
            }
            
            controller.aiMovement.SetRunning(false);
        }
    }
}


