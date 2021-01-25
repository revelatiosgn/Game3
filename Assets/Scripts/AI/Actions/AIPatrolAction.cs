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
                Debug.Log("INDEX " + controller.waypointIndex);
                controller.aiMovement.target = controller.waypoints[controller.waypointIndex];
            }
            
            controller.aiMovement.SetRunning(false);
        }
    }
}


