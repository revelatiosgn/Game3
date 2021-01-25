using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Core;
using ARPG.Controller;
using ARPG.Items;
using ARPG.Movement;

namespace ARPG.Combat
{
    public class PlayerCombat : BaseCombat
    {
        [SerializeField] CameraEvent onCameraFreeLook, onCameraAim;

        void Update()
        {
            aimRotation = Camera.main.transform.rotation;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.rotation * Vector3.forward, out hit))
            {
                targetPosition = hit.point;
            }
            else
            {
                targetPosition = Camera.main.transform.position + Camera.main.transform.forward * 30f;
            }
        }

        public override void AttackBegin()
        {
            WeaponBehaviour weaponBehaviour = GetComponent<WeaponBehaviour>();
            if (weaponBehaviour != null && weaponBehaviour.AttackBegin())
            {
                if (GetComponent<RangedBehaviour>())
                {
                    StopAllCoroutines();
                    StartCoroutine(AimCamera());
                    GetComponent<PlayerMovement>().state = PlayerMovement.MovementState.Aim;
                }
            }
        }

        public override void AttackEnd()
        {
            WeaponBehaviour weaponBehaviour = GetComponent<WeaponBehaviour>();
            if (weaponBehaviour != null && weaponBehaviour.AttackEnd())
            {
                if (GetComponent<RangedBehaviour>())
                {
                    StopAllCoroutines();
                    StartCoroutine(FreeLookCamera());
                    GetComponent<PlayerMovement>().state = PlayerMovement.MovementState.Regular;
                }
            }
        }

        public override void DefenceBegin()
        {
            WeaponBehaviour weaponBehaviour = GetComponent<WeaponBehaviour>();
            if (weaponBehaviour != null && weaponBehaviour.DefenceBegin())
                GetComponent<PlayerMovement>().state = PlayerMovement.MovementState.Aim;
                
            ShieldBehaviour shieldBehaviour = GetComponent<ShieldBehaviour>();
            if (shieldBehaviour != null && shieldBehaviour.DefenceBegin())
                GetComponent<PlayerMovement>().state = PlayerMovement.MovementState.Aim;
        }

        public override void DefenceEnd()
        {
            WeaponBehaviour weaponBehaviour = GetComponent<WeaponBehaviour>();
            if (weaponBehaviour != null && weaponBehaviour.DefenceEnd())
                GetComponent<PlayerMovement>().state = PlayerMovement.MovementState.Regular;
                
            ShieldBehaviour shieldBehaviour = GetComponent<ShieldBehaviour>();
            if (shieldBehaviour != null && shieldBehaviour.DefenceEnd())
                GetComponent<PlayerMovement>().state = PlayerMovement.MovementState.Regular;
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


