using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;

namespace ARPG.AI
{
    [CreateAssetMenu(menuName = "AI/States/State")]
    public class AIState : ScriptableObject
    {
        public AIAction[] actions;
        public AITransition[] transitions;

        public virtual void OnStateEnter(AIController controller)
        {
        }

        public virtual void OnStateExit(AIController controller)
        {
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
            foreach (AITransition transition in controller.anyStateTransitions)
            {
                if (!transition.isActive)
                    continue;

                if (transition.decision.Decide(controller))
                {
                    controller.TransitionToState(transition.trueState);
                }
                else
                {
                    controller.TransitionToState(transition.falseState);
                }
            }

            foreach (AITransition transition in transitions)
            {
                if (!transition.isActive)
                    continue;

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


