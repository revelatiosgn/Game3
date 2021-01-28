using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Stats
{
    public class CharacterStats : MonoBehaviour
    {
        public float health;

        public void TakeDamage(float value)
        {
            // health = Mathf.Clamp(health - value, 0f, float.MaxValue);

            // if (health > 0f)
            // {
            //     GetComponent<Animator>().SetTrigger("takeDamage");
            // }
            // else
            // {
            //     GetComponent<Animator>().CrossFade("None", 0.2f);
            //     GetComponent<Animator>().CrossFade("Death", 0.2f);
            // }
        }

        public bool IsDead()
        {
            return health <= 0f;
        }
    }
}