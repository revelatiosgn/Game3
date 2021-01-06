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

        Quaternion rotation = Quaternion.identity;
        public CameraState state = CameraState.Regular;

        public static CameraFollow instance = null;

        void Awake()
        {
            if (instance == null)
                instance = this;
        }

        void Update()
        {
            if (InputHandler.testInput)
            {
                regularCamera.gameObject.SetActive(!regularCamera.gameObject.activeSelf);
                aimCamera.gameObject.SetActive(!regularCamera.gameObject.activeSelf);
            }
        }

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
        }

        public static void SetState(CameraState cameraState)
        {
            instance.state = cameraState;
            instance.regularCamera.gameObject.SetActive(cameraState == CameraState.Regular);
            instance.aimCamera.gameObject.SetActive(cameraState == CameraState.Aiming);
        }
    }
}


