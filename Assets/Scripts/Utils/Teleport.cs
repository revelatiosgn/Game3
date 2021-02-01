using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Utils
{
    public class Teleport : MonoBehaviour
    {
        public Transform targetTransform;

        void OnTriggerEnter(Collider other)
        {
            Debug.Log("TP " + other.name);

            other.GetComponent<CharacterController>().enabled = false;
            other.transform.position = targetTransform.position;
            other.GetComponent<CharacterController>().enabled = true;
        }
    }
}

