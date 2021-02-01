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
            if (!CheckTransitions(controller))
                DoActions(controller);
        }

        private void DoActions(AIController controller)
        {
            foreach (AIAction action in actions)
            {
                action.Act(controller);
            }
        }

        private bool CheckTransitions(AIController controller)
        {
            foreach (AITransition transition in controller.anyStateTransitions)
            {
                if (!transition.isActive)
                    continue;
    
                bool isTransited = transition.decision.Decide(controller) ? controller.TransitionToState(transition.trueState) : controller.TransitionToState(transition.falseState);
                if (isTransited)
                    return true;
            }

            foreach (AITransition transition in transitions)
            {
                if (!transition.isActive)
                    continue;
    
                bool isTransited = transition.decision.Decide(controller) ? controller.TransitionToState(transition.trueState) : controller.TransitionToState(transition.falseState);
                if (isTransited)
                    return true;
            }

            return false;
        }
    }
}


