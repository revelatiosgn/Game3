﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSRPG
{
    public class Teleport : MonoBehaviour
    {
        public Transform targetTransform;

        void OnTriggerEnter(Collider other)
        {
            other.transform.position = targetTransform.position;
        }
    }
}

