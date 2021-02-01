using System.Collections;
using System.Collections.Generic;
using ARPG.Controller;
using UnityEngine;

namespace ARPG.Stats
{
    public class CharacterStats : MonoBehaviour
    {
        public float health;

        public void TakeDamage(float value)
        {
            if (gameObject.name == "Player")
                return;

            if (health <= 0f)
                return;

            health = Mathf.Clamp(health - value, 0f, float.MaxValue);

            if (health > 0f)
            {
                GetComponent<Animator>().SetTrigger("damage");
            }
            else
            {
                GetComponent<Animator>().CrossFade("None", 0.2f);
                GetComponent<Animator>().SetTrigger("death");
            }
        }

        public bool IsDead()
        {
            return health <= 0f;
        }
    }
}