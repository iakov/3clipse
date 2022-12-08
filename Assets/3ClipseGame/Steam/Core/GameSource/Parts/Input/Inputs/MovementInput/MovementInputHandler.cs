    using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Input.Inputs.MovementInput
{
    public class MovementInputHandler : InputHandler
    {
        public event Action<Vector2> InputChanged;
        public event Action<bool> CrouchChanged;
        public event Action<bool> SprintChanged;
        public event Action JumpPressed;
        public event Action SwitchToAnotherEntityPressed;

        private MovementInputMap _movementActionMap;

        private void Awake()
        {
            _movementActionMap = new MovementInputMap();
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _movementActionMap.Movement.Walk.started += OnWalk;
            _movementActionMap.Movement.Walk.performed += OnWalk;
            _movementActionMap.Movement.Walk.canceled += OnWalk;

            _movementActionMap.Movement.Sprint.started += OnSprint;
            _movementActionMap.Movement.Sprint.canceled += OnSprint;

            _movementActionMap.Movement.Crouch.started += OnCrouch;
            _movementActionMap.Movement.Crouch.canceled += OnCrouch;

            _movementActionMap.Movement.SwitchToAnotherEntity.started += OnSwitchPressed;
            
            _movementActionMap.Movement.Jump.started += OnJump;
        }

        private void OnWalk(InputAction.CallbackContext context)
            => InputChanged?.Invoke(context.ReadValue<Vector2>());

        private void OnSprint(InputAction.CallbackContext context)
            => SprintChanged?.Invoke(context.ReadValueAsButton());

        private void OnJump(InputAction.CallbackContext context)
            => JumpPressed?.Invoke();

        private void OnCrouch(InputAction.CallbackContext context)
            => CrouchChanged?.Invoke(context.ReadValueAsButton());

        private void OnSwitchPressed(InputAction.CallbackContext context)
            => SwitchToAnotherEntityPressed?.Invoke();

        public override void Enable()
        {   
            _movementActionMap.Enable();
        }

        public override void Disable()
        {
            _movementActionMap.Disable();
        }
    }
}
