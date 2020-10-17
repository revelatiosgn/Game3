using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSRPG
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform targetTransform;
        public Transform cameraTransform;
        public Transform pivotTransform;

        public float speed = 10f;

        void Update()
        {
            transform.position = targetTransform.position;
            cameraTransform.position = pivotTransform.position;
            cameraTransform.LookAt(targetTransform.position);

            Vector3 direction = transform.rotation.eulerAngles;
            direction.x -= InputHandler.cameraInput.y * speed * Time.deltaTime;
            direction.y += InputHandler.cameraInput.x * speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(direction);
        }
    }
}


