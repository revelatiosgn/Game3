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

        public float rotationSpeed = 10f;
        public float zoomSpeed = 0.5f;
        [Range(0.5f, 10f)] public float zoom = 4f;

        void LateUpdate()
        {
            transform.position = targetTransform.position;
            cameraTransform.position = pivotTransform.position;
            cameraTransform.LookAt(targetTransform.position);

            Vector3 direction = transform.rotation.eulerAngles;
            direction.x -= InputHandler.cameraInput.y * rotationSpeed * Time.deltaTime;
            direction.y += InputHandler.cameraInput.x * rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(direction);

            zoom = Mathf.Clamp(zoom - InputHandler.zoomInput.y * zoomSpeed * Time.deltaTime, 0.5f, 10f);
            pivotTransform.LookAt(targetTransform.position);
            pivotTransform.localPosition = Vector3.back * zoom;
        }
    }
}


