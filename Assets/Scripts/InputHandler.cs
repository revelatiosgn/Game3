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
        public static bool testInput;

        private InputActions inputActions;

        void Awake()
        {
            inputActions = new InputActions();
            inputActions.PlayerMovement.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
            inputActions.PlayerMovement.Camera.performed += ctx => cameraInput = ctx.ReadValue<Vector2>();
            inputActions.PlayerMovement.Zoom.performed += ctx => zoomInput = ctx.ReadValue<Vector2>();
        }

        void OnEnable()
        {
            inputActions.Enable();
        }

        void OnDisable()
        {
            inputActions.Disable();
        }

        void FixedUpdate()
        {
            jumpInput = inputActions.PlayerActions.Jump.triggered;
            testInput = inputActions.Debug.Test.triggered;
            
            // jumpInput = inputActions.PlayerActions.Jump.phase == UnityEngine.InputSystem.InputActionPhase.Started;
        }

        // void Update()
        // {
        //     movementInput.x = 0f;
        //     movementInput.x += Input.GetKey(KeyCode.D) ? 1f : 0f;
        //     movementInput.x += Input.GetKey(KeyCode.A) ? -1f : 0f;
        //     movementInput.y = 0f;
        //     movementInput.y += Input.GetKey(KeyCode.W) ? 1f : 0f;
        //     movementInput.y += Input.GetKey(KeyCode.S) ? -1f : 0f;

        //     cameraInput.x = 0f;
        //     cameraInput.x += Input.GetKey(KeyCode.J) ? 1f : 0f;
        //     cameraInput.x += Input.GetKey(KeyCode.L) ? -1f : 0f;

        //     cameraInput.y = 0f;
        //     cameraInput.y += Input.GetKey(KeyCode.I) ? 1f : 0f;
        //     cameraInput.y += Input.GetKey(KeyCode.K) ? -1f : 0f;
        // }
    }
}

