using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon", order = 1)]
    public class Weapon : ScriptableObject
    {
        public AnimatorOverrideController animatorOverrideController;
    }
}


