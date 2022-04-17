using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Input
{
    public class MovementInputHandler : MonoBehaviour
    {
        [NonSerialized] public Vector2 CurrentInput;
        [NonSerialized] public Vector2 LastInput;
        [NonSerialized] public bool IsRunPressed;
        [NonSerialized] public bool IsCrouchPressed;

        private MovementInput _movementInput;

        private void OnEnable()
        {
            _movementInput = new MovementInput();
            _movementInput.Enable();
            _movementInput.ExploreStateActionMap.Enable();

            _movementInput.ExploreStateActionMap.Walk.started += OnWalkChanged;
            _movementInput.ExploreStateActionMap.Walk.performed += OnWalkChanged;
            _movementInput.ExploreStateActionMap.Walk.canceled += OnWalkChanged;

            _movementInput.ExploreStateActionMap.Run.started += OnJumpChanged;
            _movementInput.ExploreStateActionMap.Run.canceled += OnJumpChanged;

            _movementInput.ExploreStateActionMap.Crouch.started += OnCrouchChanged;
            _movementInput.ExploreStateActionMap.Crouch.canceled += OnCrouchChanged;
        }

        private void OnDisable()
        {
            _movementInput.ExploreStateActionMap.Disable();
            
            _movementInput.ExploreStateActionMap.Walk.started -= OnWalkChanged;
            _movementInput.ExploreStateActionMap.Walk.performed -= OnWalkChanged;
            _movementInput.ExploreStateActionMap.Walk.canceled -= OnWalkChanged;
            
            _movementInput.ExploreStateActionMap.Run.started -= OnJumpChanged;
            _movementInput.ExploreStateActionMap.Run.canceled -= OnJumpChanged;
            
            _movementInput.ExploreStateActionMap.Crouch.started -= OnCrouchChanged;
            _movementInput.ExploreStateActionMap.Crouch.canceled -= OnCrouchChanged;
        }

        private void OnWalkChanged(InputAction.CallbackContext context) => AddInputLog(context.ReadValue<Vector2>());
        private void OnJumpChanged(InputAction.CallbackContext context) => IsRunPressed = context.ReadValueAsButton();
        private void OnCrouchChanged(InputAction.CallbackContext context) => IsCrouchPressed = context.ReadValueAsButton();

        private void AddInputLog(Vector2 newInput)
        {
            LastInput = CurrentInput;
            CurrentInput = newInput;
        }
    }
}
