using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;
using ARPG.Inventory;

namespace ARPG.Combat
{
    public class RangedBehaviour : WeaponBehaviour
    {
        bool isAttackBegin = false;
        bool isAttackEneded;
        bool isReadyToLaunch;
        GameObject arrowPrefab;
        LayerMask layerMask;

        Vector3 hitV;

        Quaternion q;

        void Update()
        {
            if (isAttackBegin)
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, float.MaxValue, layerMask))
                {
                    hitV = hit.point;
                }

                // transform.rotation *= Quaternion.AngleAxis(Time.deltaTime * 10f, Vector3.up);
            }

            // Vector3 direction = Camera.main.transform.forward;
            // direction += Camera.main.transform.forward;
            // direction.y = 0f;
            // direction.Normalize();

            // Transform follow = transform.Find("CameraFollow");
            // Vector3 rot = follow.localRotation.eulerAngles;
            // rot.y = 0f;
            // Debug.Log(rot);
            // transform.rotation = Quaternion.LookRotation(rot);

            // transform.rotation = Utils.QuaternionUtil.SmoothDamp(transform.rotation, Camera.main.transform.rotation, ref q, 0.1f);
        }

        private void OnDrawGizmos()
        {
            if (isAttackBegin)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawWireSphere(hitV, 0.1f);
            }
        }

        public override bool AttackBegin()
        {
            // isAttackBegin = true;

            // Equipment equipment = GetComponent<Equipment>();
            // EquipmentSlot arrowsSlot = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Arrows);

            // if (arrowsSlot.Item == null)
            //     return false;

            // ArrowProperty arrowProperty = arrowsSlot.Item.property as ArrowProperty;
            // arrowPrefab = arrowProperty.arrowPrefab;
            // layerMask = arrowProperty.layerMask;

            // ItemsContainer inventory = GetComponent<ItemsContainer>();
            // if (!inventory.RemoveItem(arrowsSlot.Item))
            //     return false;

            isAttackEneded = false;
            isReadyToLaunch = false;

            animator.Play("RangedAttackGetMissile");

            return true;
        }
        
        public override void AttackEnd()
        {
            isAttackEneded = true;
            if (isAttackEneded && isReadyToLaunch)
                animator.Play("RangedAttackEnd");

            isAttackBegin = false;
        }

        public void ReadyToLaunch()
        {
            isReadyToLaunch = true;
            if (isAttackEneded && isReadyToLaunch)
                animator.Play("RangedAttackEnd");
        }

        public void LaunchMissile()
        {
            // Equipment equipment = GetComponent<Equipment>();
            // EquipmentSlot weaponSlot = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Weapon);
            // foreach (WeaponHolder holder in weaponSlot.holders)
            // {
            //     if (holder.hand == WeaponHolder.Hand.Left)
            //     {
            //         GameObject arrow = Instantiate(arrowPrefab);
            //         arrow.transform.position = holder.transform.position;
            //         arrow.transform.rotation = transform.rotation;
            //     }
            // }
        }
    }
}


