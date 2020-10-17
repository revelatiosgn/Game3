using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSRPG
{
    public class InputHandler : MonoBehaviour
    {
        public static Vector2 movementInput;

        private InputActions inputActions;

        void Awake()
        {
            inputActions = new InputActions();
            inputActions.Player.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
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

