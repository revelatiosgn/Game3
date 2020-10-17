using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSRPG
{
    public class InputHandler : MonoBehaviour
    {
        public static Vector2 movementInput;
        public static Vector2 cameraInput;

        private InputActions inputActions;

        void Awake()
        {
            inputActions = new InputActions();
            inputActions.Player.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
            inputActions.Player.Camera.performed += ctx => cameraInput = ctx.ReadValue<Vector2>();
        }

        void OnEnable()
        {
            inputActions.Enable();
        }

        void OnDisable()
        {
            inputActions.Disable();
        }
    }
}

