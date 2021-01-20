using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Core;
using ARPG.Controller;
using ARPG.Items;

namespace ARPG.Combat
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] CameraEvent onCameraFreeLook, onCameraAim;
        
        Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void AttackBegin()
        {
            GetComponent<WeaponBehaviour>()?.AttackBegin();
            if (GetComponent<RangedBehaviour>())
            {
                StopAllCoroutines();
                StartCoroutine(AimCamera());
            }
        }

        public void AttackEnd()
        {
            GetComponent<WeaponBehaviour>()?.AttackEnd();
            if (GetComponent<RangedBehaviour>())
            {
                StopAllCoroutines();
                StartCoroutine(FreeLookCamera());
            }
        }

        public void DefenceBegin()
        {
            GetComponent<WeaponBehaviour>()?.DefenceBegin();
            GetComponent<ShieldBehaviour>()?.DefenceBegin();
        }

        public void DefenceEnd()
        {
            GetComponent<WeaponBehaviour>()?.DefenceEnd();
            GetComponent<ShieldBehaviour>()?.DefenceEnd();
        }

        IEnumerator AimCamera()
        {
            yield return new WaitForSeconds(0f);
            onCameraAim.RaiseEvent();
        }

        IEnumerator FreeLookCamera()
        {
            yield return new WaitForSeconds(2f);
            onCameraFreeLook.RaiseEvent();
        }
    }
}


