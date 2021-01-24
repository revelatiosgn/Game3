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
            GameObject player = GameObject.FindGameObjectWithTag(Constants.Tags.Player);
            Vector3 eyes = controller.eyes.position;
            Vector3 target = player.transform.position + Vector3.up;

            if (Vector3.Distance(target, eyes) < 10f)
            {
                Vector3 direction = (target - eyes);
                float angle = Vector3.Angle(controller.transform.forward, direction);

                if (angle * 2f < 120f)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(eyes, direction.normalized, out hit, 10f))
                    {
                        if (hit.collider.gameObject == player)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}


