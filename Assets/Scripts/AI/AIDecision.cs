using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;

namespace ARPG.AI
{
    public abstract class AIDecision : ScriptableObject
    {
        public abstract bool Decide(AIController controller);
    }
}


