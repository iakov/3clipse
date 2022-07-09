using System;
using _3ClipseGame.Steam.Global.Input.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _3ClipseGame.Steam.Global.Input.PlayerInput
{
    public class MovementInputHandler : InputHandler
    {
        #region Initialization
        [NonSerialized] public Vector2 CurrentInput;
        [NonSerialized] public Vector2 LastInput;
        [NonSerialized] public bool IsRunPressed;
        [NonSerialized] public bool IsCrouchPressed;
        [NonSerialized] public bool IsJumpPressed;

        private MovementInput _movementInput;
        #endregion

        #region MonoBehaviourMethods

        private void OnEnable()
        {
            _movementInput = new MovementInput();
            _movementInput.Enable();
            _movementInput.ExploreStateActionMap.Enable();

            _movementInput.ExploreStateActionMap.Walk.started += OnWalkChanged;
            _movementInput.ExploreStateActionMap.Walk.performed += OnWalkChanged;
            _movementInput.ExploreStateActionMap.Walk.canceled += OnWalkChanged;

            _movementInput.ExploreStateActionMap.Run.started += OnRunChanged;
            _movementInput.ExploreStateActionMap.Run.canceled += OnRunChanged;

            _movementInput.ExploreStateActionMap.Crouch.started += OnCrouchChanged;
            _movementInput.ExploreStateActionMap.Crouch.canceled += OnCrouchChanged;

            _movementInput.ExploreStateActionMap.Jump.started += OnJumpChanged;
            _movementInput.ExploreStateActionMap.Jump.canceled += OnJumpChanged;
        }

        private void OnDisable() => _movementInput.Disable();
        

        #endregion

        #region EventHandlers
        
        private void OnWalkChanged(InputAction.CallbackContext context) => AddInputLog(context.ReadValue<Vector2>());
        private void OnJumpChanged(InputAction.CallbackContext context) => IsJumpPressed = context.ReadValueAsButton();
        private void OnRunChanged(InputAction.CallbackContext context) => IsRunPressed = context.ReadValueAsButton();
        private void OnCrouchChanged(InputAction.CallbackContext context) => IsCrouchPressed = context.ReadValueAsButton();


        #endregion

        #region PublicMethods

        public override void Disable()
        {
            CurrentInput = Vector2.zero;
            IsRunPressed = false;
            IsCrouchPressed = false;
            IsJumpPressed = false;
            _movementInput.Disable();
        }

        public override void Enable() => _movementInput.Enable();

        #endregion

        #region PrivateMethods

        private void AddInputLog(Vector2 newInput)
        {
            LastInput = CurrentInput;
            CurrentInput = newInput;
        }

        #endregion
    }
}
