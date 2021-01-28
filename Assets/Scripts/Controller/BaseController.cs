using System.Collections;
using System.Collections.Generic;
using ARPG.Stats;
using UnityEngine;

namespace ARPG.Controller
{
    public enum CharacterGroup
    {
        Neutral = 0,
        Enemy
    }

    public class BaseController : MonoBehaviour
    {
        public bool isInteracting = false;
        public CharacterGroup characterGroup;
        public CharacterStats characterStats;

        protected virtual void Awake()
        {
            characterStats = GetComponent<CharacterStats>();
        }
    }
}


