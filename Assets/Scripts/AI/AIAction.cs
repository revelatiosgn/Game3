using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;

namespace ARPG.AI
{
    public abstract class AIAction : ScriptableObject
    {
        public abstract void Act(AIController controller);
    }
}


