using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Stats;

namespace ARPG.Combat
{
    public class Weapon : MonoBehaviour
    {
        public WeaponEvent onDamage = new WeaponEvent();

        Collider damageCollider;

        public void SetDamaging(bool value)
        {
            damageCollider.enabled = value;
        }

        void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.enabled = false;
        }
    }
}