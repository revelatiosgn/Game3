using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

namespace ARPG.Core
{
    public class CameraFollow : MonoBehaviour
    {
        public enum CameraState
        {
            Regular,
            Aiming
        }

        [SerializeField] InputHandler inputHandler;
        [SerializeField] CinemachineVirtualCamera regularCamera;
        [SerializeField] CinemachineVirtualCamera aimCamera;
        [SerializeField] CameraEvent onCameraFreeLook, onCameraAim;

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

            inputHandler.rotateCameraEvent += OnRotateCameraEvent;
            onCameraFreeLook.OnEventRaised += OnCameraFreeLook;
            onCameraAim.OnEventRaised += OnCameraAim;
        }

        void LateUpdate()
        {
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

            transform.rotation = Quaternion.Euler(angles);
        }

        private void OnRotateCameraEvent(Vector2 value)
        {
            Debug.Log("ROT");
            rotation *= Quaternion.AngleAxis(value.x * rotationSpeed * Time.smoothDeltaTime, Vector3.up);
            rotation *= Quaternion.AngleAxis(-value.y * rotationSpeed * Time.smoothDeltaTime, Vector3.right);
        }

        public static void SetState(CameraState cameraState)
        {
            // instance.state = cameraState;
            // instance.regularCamera.gameObject.SetActive(cameraState == CameraState.Regular);
            // instance.aimCamera.gameObject.SetActive(cameraState == CameraState.Aiming);
        }

        void OnCameraFreeLook()
        {
            instance.regularCamera.gameObject.SetActive(true);
            instance.aimCamera.gameObject.SetActive(false);
        }

        void OnCameraAim()
        {

            instance.regularCamera.gameObject.SetActive(false);
            instance.aimCamera.gameObject.SetActive(true);
        }
    }
}


