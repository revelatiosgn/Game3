using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Gear;

namespace ARPG.Combat
{
    public class EquipmentBehaviour : MonoBehaviour
    {
        protected Animator animator;
        protected Equipment equipment;

        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
            equipment = GetComponent<Equipment>();
        }
    }
}


