using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Events;
using Cinemachine;
using UnityEngine;

namespace ARPG.Core
{
    public class CameraSystem : MonoBehaviour
    {
        [SerializeField] Vector2Event onPlayerRotateCamera;
        [SerializeField] BoolEvent onInventoryActive;

        [SerializeField] CinemachineFreeLook freeLookCamera;
        [SerializeField] CinemachineFreeLook aimCamera;
        [SerializeField] [Range(0f, 50f)] float rotationMult = 1f; 
        [SerializeField] CameraEvent onCameraFreeLook, onCameraAim;
        [SerializeField] Camera InteractionCamera;

        public static CinemachineFreeLook activeCamera;

        void OnPlayerRotateCamera(Vector2 value)
        {
        }

        void Awake()
        {
            freeLookCamera.gameObject.SetActive(true);
            aimCamera.gameObject.SetActive(false);
            activeCamera = freeLookCamera;
        }

        void OnEnable()
        {
            onPlayerRotateCamera.onEventRaised += OnPlayerRotateCamera;
            onCameraFreeLook.onEventRaised += OnCameraFreeLook;
            onCameraAim.onEventRaised += OnCameraAim;
            onInventoryActive.onEventRaised += OnInventoryActive;
        }

        void OnDisable()
        {
            onPlayerRotateCamera.onEventRaised -= OnPlayerRotateCamera;
            onCameraFreeLook.onEventRaised -= OnCameraFreeLook;
            onCameraAim.onEventRaised -= OnCameraAim;
            onInventoryActive.onEventRaised -= OnInventoryActive;
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

        void OnInventoryActive(bool isInventoryActive)
        {
            InteractionCamera.gameObject.SetActive(!isInventoryActive);
        }
    }
}


