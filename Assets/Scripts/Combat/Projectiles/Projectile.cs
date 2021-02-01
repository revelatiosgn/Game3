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

        void Start()
        {
            Destroy(gameObject, 5f);
        }

        void Update()
        {
            transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);
        }

        void OnTriggerEnter(Collider collider)
        {
            BaseController targetController = collider.GetComponent<BaseController>();
            if (targetController != null && owner != null && targetController.characterGroup != owner.characterGroup)
                targetController.OnTakeDamage(owner, damage);

            Destroy(gameObject);
        }
    }
}


