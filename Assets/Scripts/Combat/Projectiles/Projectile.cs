using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Stats;
using ARPG.Controller;

namespace ARPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        public float speed;
        public float damage;
        public BaseController owner;

        private bool isLaunched;

        public void Launch()
        {
            isLaunched = true;
            Destroy(gameObject, 5f);
        }

        void Update()
        {
            if (!isLaunched)
                return;

            transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);
        }

        void OnTriggerEnter(Collider collider)
        {
            if (!isLaunched)
                return;

            BaseController targetController = collider.GetComponent<BaseController>();
            if (targetController != null && owner != null && targetController.characterGroup != owner.characterGroup)
                targetController.OnTakeDamage(owner, damage);

            if (targetController != owner)
                Destroy(gameObject);
        }
    }
}


