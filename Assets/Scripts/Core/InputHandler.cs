using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

using ARPG.Events;
using System;

namespace ARPG.Core
{
    [CreateAssetMenu(fileName = "InputHandler", menuName = "Game/InputHandler")]
    public class InputHandler : ScriptableObject, InputActions.IPlayerMovementActions, InputActions.IPlayerActionsActions, InputActions.IUIActions
    {
        private InputActions inputActions;

        [SerializeField] Vector2Event onPlayerMove;
        [SerializeField] Vector2Event onPlayerRotateCamera;
        [SerializeField] VoidEvent onPlayerInvetory;
        [SerializeField] VoidEvent onPlayerAttackBegin;
        [SerializeField] VoidEvent onPlayerAttackEnd;
        [SerializeField] VoidEvent onPlayerDefenceBegin;
        [SerializeField] VoidEvent onPlayerDefenceEnd;

        [SerializeField] BoolEvent onLockPlayerActions;

        void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new InputActions();
                inputActions.PlayerMovement.SetCallbacks(this);
                inputActions.PlayerActions.SetCallbacks(this);
                inputActions.UI.SetCallbacks(this);
            }

            inputActions.Enable();

            onLockPlayerActions.OnEventRaised += OnLockPlayerActions;
        }

        void OnDisable()
        {
            inputActions.Disable();

            onLockPlayerActions.OnEventRaised -= OnLockPlayerActions;
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            //movementEvent.Invoke(context.ReadValue<Vector2>());
            onPlayerMove.RaiseEvent(context.ReadValue<Vector2>());
        }

        public void OnRotateCamera(InputAction.CallbackContext context)
        {
            onPlayerRotateCamera.RaiseEvent(context.ReadValue<Vector2>());
        }

        public void OnZoom(InputAction.CallbackContext context)
        {
        }

        public void OnWalk(InputAction.CallbackContext context)
        {
        }

        public void OnJump(InputAction.CallbackContext context)
        {
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                onPlayerAttackBegin.RaiseEvent();

            if (context.phase == InputActionPhase.Canceled)
                onPlayerAttackEnd.RaiseEvent();
        }

        public void OnDefence(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                onPlayerDefenceBegin.RaiseEvent();

            if (context.phase == InputActionPhase.Canceled)
                onPlayerDefenceEnd.RaiseEvent();
        }

        public void OnInventory(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                onPlayerInvetory.RaiseEvent();
        }

        private void OnLockPlayerActions(bool value)
        {
            if (value)
            {
                inputActions.PlayerMovement.Disable();
                inputActions.PlayerActions.Disable();
            }
            else
            {
                inputActions.PlayerMovement.Enable();
                inputActions.PlayerActions.Enable();
            }
        }
    }
}

