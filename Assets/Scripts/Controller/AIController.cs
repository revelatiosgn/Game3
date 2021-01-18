using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using ARPG.Inventory;

namespace ARPG.Controller
{
    public class AIController : MonoBehaviour
    {
        public enum State
        {
            None,
            Chase,
            Attack
        }

        public Transform targetTransform;
        Animator animator;

        public State state = State.None;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        void Start()
        {
            List<ItemSlot> itemSlots = GetComponent<ItemsContainer>().ItemSlots;
            foreach (ItemSlot itemSlot in itemSlots)
            {
                if(itemSlot.item.GetType() == typeof(ARPG.Items.MeleeWeaponItem))
                {
                    itemSlot.item.OnUse(gameObject);
                    break;
                }
            }
        }

        void Update()
        {
            return;

            targetTransform = GameObject.FindGameObjectWithTag(Constants.Tags.Player).transform;

            if (Vector3.Distance(transform.position, targetTransform.position) < 3f)
            {
                state = State.Attack;
            }
            else if (CheckFOV())
            {
                state = State.Chase;
            }
            else
            {
                state = State.None;
            }
        }

        bool CheckFOV()
        {
            targetTransform = GameObject.FindGameObjectWithTag(Constants.Tags.Player).transform;
            if (Vector3.Distance(transform.position, targetTransform.position) < 10f)
            {
                Vector3 dir = targetTransform.position - transform.position;
                float angle = Vector3.Angle(transform.forward, dir);

                if (angle * 2f < 120f)
                {
                    if (!Physics.Raycast(transform.position, targetTransform.position - transform.position, 10f, LayerMask.GetMask("Environment")))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, 5f);

            

            
        }
    }
}
