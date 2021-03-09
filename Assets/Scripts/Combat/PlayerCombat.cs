using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Events;
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
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.rotation * Vector3.forward, out hit, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
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
            base.AttackBegin();

            // if (base.AttackBegin() && (WeaponBehaviour as RangedBehaviour) != null)
            // {
            //     StopAllCoroutines();
            //     StartCoroutine(AimCamera());
            //     GetComponent<PlayerMovement>().state = PlayerMovement.MovementState.Aim;

            //     return true;
            // }

            // return false;
        }

        public override void OnAttackComplete()
        {
            base.OnAttackComplete();

            // if ((WeaponBehaviour as RangedBehaviour) != null)
            // {
            //     StopAllCoroutines();
            //     StartCoroutine(FreeLookCamera());
            //     GetComponent<PlayerMovement>().state = PlayerMovement.MovementState.Regular;
            // }
        }

        IEnumerator AimCamera()
        {
            yield return new WaitForSeconds(0f);
            onCameraAim.RaiseEvent();
        }

        IEnumerator FreeLookCamera()
        {
            yield return new WaitForSeconds(0.5f);
            onCameraFreeLook.RaiseEvent();
        }
    }
}


