using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;

namespace ARPG.AI
{
    public abstract class AIAction : ScriptableObject
    {
        public virtual void OnStateEnter(AIController controller) {}
        public virtual void OnStateExit(AIController controller) {}
        public virtual void Act(AIController controller) {}
    }
}


