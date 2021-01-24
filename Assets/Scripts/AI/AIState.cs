using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;

namespace ARPG.AI
{
    [CreateAssetMenu(menuName = "AI/State")]
    public class AIState : ScriptableObject
    {
        public AIAction[] actions;
        public AITransition[] transitions;

        public void OnStateEnter(AIController controller)
        {
            foreach (AIAction action in actions)
            {
                action.OnStateEnter(controller);
            }
        }

        public void OnStateExit(AIController controller)
        {
            foreach (AIAction action in actions)
            {
                action.OnStateExit(controller);
            }
        }

        public void UpdateState(AIController controller)
        {
            DoActions(controller);
            CheckTransitions(controller);
        }

        private void DoActions(AIController controller)
        {
            foreach (AIAction action in actions)
            {
                action.Act(controller);
            }
        }

        private void CheckTransitions(AIController controller)
        {
            foreach (AITransition transition in transitions)
            {
                if (transition.decision.Decide(controller))
                {
                    controller.TransitionToState(transition.trueState);
                }
                else
                {
                    controller.TransitionToState(transition.falseState);
                }
            }
        }
    }
}


