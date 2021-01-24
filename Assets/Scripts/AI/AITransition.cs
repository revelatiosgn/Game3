using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.AI
{
    [System.Serializable]
    public class AITransition
    {
        public AIDecision decision;
        public AIState trueState;
        public AIState falseState; 
    }
}

