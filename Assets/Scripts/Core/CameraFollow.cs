using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace ARPG.Core
{
    public class CameraFollow : MonoBehaviour
    {
        public enum CameraState
        {
            Regular,
            Aiming
        }

        [SerializeField] CinemachineVirtualCamera regularCamera;
        [SerializeField] CinemachineVirtualCamera aimCamera;

        [SerializeField][Range(1f, 10f)] float rotationSpeed = 5f;
        [SerializeField][Range(0f, 90f)] float minY = 70f;
        [SerializeField][Range(0f, 90f)] float maxY = 70f;

        public CameraState state = CameraState.Regular;
        public CameraState State
        {
            get => state;
            set
            {
                state = value;
            }
        }

        Quaternion rotation = Quaternion.identity;

        void LateUpdate()
        {
            rotation *= Quaternion.AngleAxis(InputHandler.cameraInput.x * rotationSpeed * Time.deltaTime, Vector3.up);
            rotation *= Quaternion.AngleAxis(-InputHandler.cameraInput.y * rotationSpeed * Time.deltaTime, Vector3.right);

            Vector3 angles = rotation.eulerAngles;
            angles.z = 0f;

            if (angles.x > 180f && angles.x < 360f - minY)
            {
                angles.x = 360f - minY;
            }
            else if (angles.x < 180f && angles.x > maxY)
            {
                angles.x = maxY;
            }

            rotation = Quaternion.Euler(angles);
            transform.rotation = rotation;

            if (InputHandler.testInput)
            {
                // regularCamera.gameObject.SetActive(!regularCamera.gameObject.activeSelf);
                // aimCamera.gameObject.SetActive(!regularCamera.gameObject.activeSelf);
                aimCamera.gameObject.SetActive(true);
            }
        }
    }
}


