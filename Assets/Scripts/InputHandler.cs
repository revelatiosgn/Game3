using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSRPG
{
    public class InputHandler : MonoBehaviour
    {
        public static Vector2 movementInput;
        public static Vector2 cameraInput;
        public static Vector3 zoomInput;
        public static bool jumpInput;
        public static bool walkInput;
        public static bool testInput;

        private InputActions inputActions;

        void Awake()
        {
            inputActions = new InputActions();
            inputActions.PlayerMovement.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
            inputActions.PlayerMovement.Camera.performed += ctx => cameraInput = ctx.ReadValue<Vector2>();
            inputActions.PlayerMovement.Zoom.performed += ctx => zoomInput = ctx.ReadValue<Vector2>();
            inputActions.PlayerMovement.Walk.started += ctx => walkInput = true;
            inputActions.PlayerMovement.Walk.canceled += ctx => walkInput = false;

            inputActions.PlayerActions.Jump.started += ctx => jumpInput = true;

            inputActions.Debug.Test.started += ctx => testInput = true;
        }

        void OnEnable()
        {
            inputActions.Enable();
        }

        void OnDisable()
        {
            inputActions.Disable();
        }

        void LateUpdate()
        {
            jumpInput = false;
            testInput = false;
        }
    }
}

