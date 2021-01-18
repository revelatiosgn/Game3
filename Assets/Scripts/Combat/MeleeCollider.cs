using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Combat
{
    public class MeleeCollider : MonoBehaviour
    {
        public WeaponEvent weaponEvent = new WeaponEvent();

        private void OnTriggerEnter(Collider other)
        {
            weaponEvent.Invoke(other.transform);
            Destroy(gameObject);
        }
    }
}

