using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Stats;

namespace ARPG.Combat
{
    public class Weapon : MonoBehaviour
    {
        Collider damagingCollider;

        public void SetDamaging(bool value)
        {
            damagingCollider.enabled = value;
        }

        void Awake()
        {
            damagingCollider = GetComponent<Collider>();
            damagingCollider.enabled = false;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == Constants.Tags.Enemy)
            {
                MakeDamage(other.GetComponent<Attributes>());
            }
        }

        void MakeDamage(Attributes target)
        {
            // target.TakeDamage(damage);
        }
    }
}