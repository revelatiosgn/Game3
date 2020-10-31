using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Stats
{
    public class Attributes : MonoBehaviour
    {
        public float health;
        public float mana;
        public float staminа;

        public float strength;

        public void TakeDamage(float value)
        {
            health = Mathf.Clamp(health - value, 0f, float.MaxValue);
        }
    }

}