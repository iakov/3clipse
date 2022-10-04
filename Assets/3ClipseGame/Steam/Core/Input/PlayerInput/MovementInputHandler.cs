using System;
using _3ClipseGame.Steam.Global.Input.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _3ClipseGame.Steam.Core.Input.PlayerInput
{
    public class MovementInputHandler : InputHandler
    {
        #region Public

        public event Action ModeSwitchPressed;
        public event Action JumpPressed;
        public event Action<Vector2> InputChanged;
        public event Action<bool> CrouchChanged;
        public event Action<bool> SprintChanged;

        public void SwitchToAnimalControls()
        {
            _movementInput.ExploreStateActionMap.Disable();
            _movementInput.FightStateActionMap.Disable();
            
            _movementInput.AnimalStateActionMap.Enable();
            _lastActionMap = _movementInput.AnimalStateActionMap;
        }

        public void SwitchToExploreControls()
        {
            _movementInput.AnimalStateActionMap.Disable();
            _movementInput.FightStateActionMap.Disable();
            
            _movementInput.ExploreStateActionMap.Enable();
            _lastActionMap = _movementInput.ExploreStateActionMap;
        }

        public void SwitchToFightControls()
        {
            _movementInput.AnimalStateActionMap.Disable();
            _movementInput.ExploreStateActionMap.Disable();
            
            _movementInput.FightStateActionMap.Enable();
            _lastActionMap = _movementInput.FightStateActionMap;
        }

        public override void Disable() => OnDisable();

        public override void Enable() => OnEnable();

        private MovementInput _movementInput;
        private InputActionMap _lastActionMap;
        
        #endregion

        #region MonoBehaviourMethods

        private void Awake()
        {
            _movementInput = new MovementInput();
            _lastActionMap = _movementInput.ExploreStateActionMap;
        }
        
        private void OnEnable()
        {
            _movementInput.Enable();
            _lastActionMap.Enable();

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

            _movementInput.AnimalStateActionMap.Crouch.started += OnCrouchChanged;
            _movementInput.AnimalStateActionMap.Crouch.canceled += OnCrouchChanged;

            _movementInput.AnimalStateActionMap.Jump.started += OnJumpChanged;
            _movementInput.AnimalStateActionMap.Jump.canceled += OnJumpChanged;
            
            _movementInput.AnimalStateActionMap.SwitchToCharacter.started += OnSwitch;
        }

        private void OnDisable()
        {
            _movementInput.Disable();
        }

        #endregion

        #region EventHandlers

        private void OnWalkChanged(InputAction.CallbackContext context) 
            => InputChanged?.Invoke(context.ReadValue<Vector2>());
        
        private void OnJumpChanged(InputAction.CallbackContext context) 
            => JumpPressed?.Invoke();

        private void OnRunChanged(InputAction.CallbackContext context)
            => SprintChanged?.Invoke(context.ReadValueAsButton());
        
        private void OnCrouchChanged(InputAction.CallbackContext context) 
            => CrouchChanged?.Invoke(context.ReadValueAsButton());
        
        private void OnSwitch(InputAction.CallbackContext context)
            => ModeSwitchPressed?.Invoke();

        #endregion
    }
}
