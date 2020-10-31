﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Core
{
    public class InputHandler : MonoBehaviour
    {
        public static Vector2 movementInput;
        public static Vector2 cameraInput;
        public static Vector3 zoomInput;

        public static bool jumpInput;
        public static bool walkInput;
        public static bool attackInput;

        public static bool inventoryInput;

        public static bool testInput;

        private InputActions inputActions;

        public void SetInUI(bool inUI)
        {
            if (inUI)
            {
                inputActions.PlayerMovement.Disable();
                inputActions.PlayerActions.Disable();

                movementInput = Vector2.zero;
                cameraInput = Vector2.zero;
                zoomInput = Vector3.zero;
            }
            else
            {
                inputActions.PlayerMovement.Enable();
                inputActions.PlayerActions.Enable();
            }
        }

        void OnEnable()
        {
            inputActions = new InputActions();
            inputActions.PlayerMovement.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
            inputActions.PlayerMovement.Camera.performed += ctx => cameraInput = ctx.ReadValue<Vector2>();
            inputActions.PlayerMovement.Zoom.performed += ctx => zoomInput = ctx.ReadValue<Vector2>();

            inputActions.PlayerActions.Jump.started += ctx => jumpInput = true;
            inputActions.PlayerMovement.Walk.started += ctx => walkInput = true;
            inputActions.PlayerMovement.Walk.canceled += ctx => walkInput = false;
            inputActions.PlayerActions.Attack.started += ctx => attackInput = true;
            inputActions.PlayerActions.Attack.canceled += ctx => attackInput = false;

            inputActions.UI.Inventory.started += ctx => inventoryInput = true;

            inputActions.Debug.Test.started += ctx => testInput = true;
            
            inputActions.Enable();
        }

        void OnDisable()
        {
            inputActions.Disable();
        }

        void LateUpdate()
        {
            jumpInput = false;

            inventoryInput = false;

            testInput = false;
        }
    }
}

