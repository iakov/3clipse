using System;
using System.Collections;
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
        [NonSerialized] public bool IsSwitchPressed;
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

            //Set Explore Handlers
            _movementInput.ExploreStateActionMap.Walk.started += OnWalkChanged;
            _movementInput.ExploreStateActionMap.Walk.performed += OnWalkChanged;
            _movementInput.ExploreStateActionMap.Walk.canceled += OnWalkChanged;

            _movementInput.ExploreStateActionMap.Run.started += OnRunChanged;
            _movementInput.ExploreStateActionMap.Run.canceled += OnRunChanged;

            _movementInput.ExploreStateActionMap.Crouch.started += OnCrouchChanged;
            _movementInput.ExploreStateActionMap.Crouch.canceled += OnCrouchChanged;

            _movementInput.ExploreStateActionMap.Jump.started += OnJumpChanged;
            _movementInput.ExploreStateActionMap.Jump.canceled += OnJumpChanged;

            _movementInput.ExploreStateActionMap.SwitchToAnimal.started += OnSwitch;
            
            //Set Animal Handlers
            _movementInput.AnimalStateActionMap.Walk.started += OnWalkChanged;
            _movementInput.AnimalStateActionMap.Walk.performed += OnWalkChanged;
            _movementInput.AnimalStateActionMap.Walk.canceled += OnWalkChanged;

            _movementInput.AnimalStateActionMap.Run.started += OnRunChanged;
            _movementInput.AnimalStateActionMap.Run.canceled += OnRunChanged;
            
            _movementInput.AnimalStateActionMap.SwitchToCharacter.started += OnSwitch;
        }

        private void OnDisable()
        {
            CurrentInput = Vector2.zero;
            IsRunPressed = false;
            IsCrouchPressed = false;
            IsJumpPressed = false;
            _movementInput.Disable();
        }


        #endregion

        #region EventHandlers
        
        private void OnWalkChanged(InputAction.CallbackContext context) => AddInputLog(context.ReadValue<Vector2>());
        private void OnJumpChanged(InputAction.CallbackContext context) => IsJumpPressed = context.ReadValueAsButton();
        private void OnRunChanged(InputAction.CallbackContext context) => IsRunPressed = context.ReadValueAsButton();
        private void OnCrouchChanged(InputAction.CallbackContext context) => IsCrouchPressed = context.ReadValueAsButton();
        private void OnSwitch(InputAction.CallbackContext context) => IsSwitchPressed = true;
        

        #endregion

        #region PublicMethods

        public void SwitchToAnimalControls()
        {
            _movementInput.ExploreStateActionMap.Disable();
            _movementInput.FightStateActionMap.Disable();
            
            _movementInput.AnimalStateActionMap.Enable();
        }

        public void SwitchToExploreControls()
        {
            _movementInput.AnimalStateActionMap.Disable();
            _movementInput.FightStateActionMap.Disable();
            
            _movementInput.ExploreStateActionMap.Enable();
        }

        public void SwitchToFightControls()
        {
            _movementInput.AnimalStateActionMap.Disable();
            _movementInput.ExploreStateActionMap.Disable();
            
            _movementInput.FightStateActionMap.Enable();
        }

        public override void Disable() => OnDisable();

        public override void Enable() => OnEnable();

        #endregion

        #region PrivateMethods

        private void AddInputLog(Vector2 newInput)
        {
            LastInput = CurrentInput;
            CurrentInput = newInput;
            Debug.Log(CurrentInput);
        }

        #endregion
    }
}
