using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Combat
{
    public class Arrow : Weapon
    {
        public float speed;
        public float gravity;

        float fallingVelocity = 0f;
        Vector3 lastPosition;
        Quaternion rotation;

        void Start()
        {
            lastPosition = transform.position;
            rotation = transform.rotation;

            Destroy(gameObject, 10f);
        }

        void Update()
        {
            transform.Translate(rotation * Vector3.forward * Time.deltaTime * speed, Space.World);
            transform.Translate(Vector3.down * Time.deltaTime * fallingVelocity, Space.World);
            fallingVelocity += gravity * Time.deltaTime;

            transform.rotation = Quaternion.LookRotation(transform.position - lastPosition);
            lastPosition = transform.position;
        }
        
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == Constants.Tags.Enemy)
            {
                onDamage.Invoke(other.transform);
            }

            Destroy(gameObject);
        }
    }
}

