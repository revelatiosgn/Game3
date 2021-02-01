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
        [HideInInspector] public bool isInteracting = false;
        [HideInInspector] public CharacterGroup characterGroup;
        [HideInInspector] public CharacterStats characterStats;
        [HideInInspector] public Collider characterCollider;

        protected virtual void Awake()
        {
            characterStats = GetComponent<CharacterStats>();
            characterCollider = GetComponent<CapsuleCollider>();
        }

        public virtual void OnTakeDamage(BaseController source, float damage)
        {
            characterStats.TakeDamage(damage);
        }
    }
}


