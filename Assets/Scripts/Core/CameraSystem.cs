using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace ARPG.Core
{
    public class CameraSystem : MonoBehaviour
    {
        [SerializeField] InputHandler inputHandler;
        [SerializeField] CinemachineFreeLook freeLookCamera;
        [SerializeField] CinemachineFreeLook aimCamera;
        [SerializeField] [Range(1f, 5f)] float rotationMult = 1f; 
        [SerializeField] CameraEvent onCameraFreeLook, onCameraAim;

        CinemachineFreeLook activeCamera;

        void Awake()
        {
            freeLookCamera.gameObject.SetActive(true);
            aimCamera.gameObject.SetActive(false);
            activeCamera = freeLookCamera;
        }

        void OnEnable()
        {
            inputHandler.rotateCameraEvent += OnRotateCameraEvent;
            onCameraFreeLook.OnEventRaised += OnCameraFreeLook;
            onCameraAim.OnEventRaised += OnCameraAim;
        }

        void OnDisable()
        {
            inputHandler.rotateCameraEvent -= OnRotateCameraEvent;
        }

        void LateUpdate()
        {
        }

        void OnRotateCameraEvent(Vector2 value)
        {
            activeCamera.m_XAxis.m_InputAxisValue = -value.x * Time.smoothDeltaTime * rotationMult;
            activeCamera.m_YAxis.m_InputAxisValue = -value.y * Time.smoothDeltaTime * rotationMult;
        }

        void OnCameraFreeLook()
        {
            if (activeCamera == freeLookCamera)
                return;

            freeLookCamera.m_XAxis.Value = aimCamera.m_XAxis.Value;
            freeLookCamera.m_YAxis.Value = aimCamera.m_YAxis.Value;

            ResetCameraInput();

            freeLookCamera.gameObject.SetActive(true);
            aimCamera.gameObject.SetActive(false);
            activeCamera = freeLookCamera;
        }

        void OnCameraAim()
        {
            if (activeCamera == aimCamera)
                return;

            aimCamera.m_XAxis.Value = freeLookCamera.m_XAxis.Value;
            aimCamera.m_YAxis.Value = freeLookCamera.m_YAxis.Value;

            ResetCameraInput();

            freeLookCamera.gameObject.SetActive(false);
            aimCamera.gameObject.SetActive(true);
            activeCamera = aimCamera;
        }

        void ResetCameraInput()
        {
            freeLookCamera.m_XAxis.m_InputAxisValue = 0f;
            freeLookCamera.m_YAxis.m_InputAxisValue = 0f;
            aimCamera.m_XAxis.m_InputAxisValue = 0f;
            aimCamera.m_YAxis.m_InputAxisValue = 0f;
        }
    }
}


