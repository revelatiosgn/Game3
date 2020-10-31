using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Stats;

namespace ARPG.Combat
{
    public class Weapon : MonoBehaviour
    {
        public enum WeaponType
        {
            Melee,
            Ranged
        }

        public float damage;
        public WeaponHolder.HolderType holderType;
        public WeaponType weaponType;
        public AnimatorOverrideController animatorOverrideController;

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
            target.TakeDamage(damage);
        }
    }
}