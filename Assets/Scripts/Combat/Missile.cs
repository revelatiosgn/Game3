using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Combat
{
    public class Missile : MonoBehaviour
    {
        void Update()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 10);
        }
    }
}

