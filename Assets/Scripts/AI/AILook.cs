using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Controller;

namespace ARPG.AI
{
    public class AILook : MonoBehaviour
    {
        public AIController controller;
        public List<BaseController> targets;

        LayerMask layerMask;

        void Awake()
        {
            layerMask = LayerMask.GetMask("Environment");
        }

        void Update()
        {
            controller.charactersCanSee.Clear();
            targets.ForEach(target => {
                if (IsCanSee(target))
                    controller.charactersCanSee.Add(target);
                else
                    controller.charactersCanSee.Remove(target);
            });
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == controller.gameObject)
                return;

            targets.Add(other.GetComponent<BaseController>());
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == controller.gameObject)
                return;

            targets.Remove(other.GetComponent<BaseController>());
        }

        private bool IsCanSee(BaseController target)
        {
            Vector3 direction = ((target.transform.position + Vector3.up) - transform.position);
            float angle = Vector3.Angle(transform.forward, direction);

            if (angle * 2f > 120f)
                return false;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction.normalized, out hit, direction.magnitude, layerMask))
                return false;

            return true;
        }
    }
}
