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
        public CharacterGroup characterGroup;

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
            BaseController controller = collider.GetComponent<BaseController>();
            if (controller != null && controller.characterGroup != characterGroup)
            {
                controller.GetComponent<CharacterStats>().TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}


