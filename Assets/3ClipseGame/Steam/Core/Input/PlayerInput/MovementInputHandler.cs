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
        public event Action LootInteractionPressed;
        private Vector2 _currentInput;
        private Vector2 _lastInput;
        private bool _isRunPressed;
        private bool _isCrouchPressed;

        public Vector2 GetCurrentInput() => _currentInput;
        public Vector2 GetLastInput() => _lastInput;
        public bool GetIsRunPressed() => _isRunPressed;
        public bool GetIsCrouchPressed() => _isCrouchPressed;

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
        
        // [NonSerialized] public bool IsCrouchPressed;
        // [NonSerialized] public bool IsJumpPressed;
        // [NonSerialized] public bool IsLootInteractionPressed;

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

            _movementInput.ExploreStateActionMap.LootInteraction.started += OnLootInteraction;
            
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
            _currentInput = Vector2.zero;
            _isRunPressed = false;
            _isCrouchPressed = false;
            _movementInput.Disable();
        }

        #endregion

        #region EventHandlers
        
        private void OnWalkChanged(InputAction.CallbackContext context) => AddInputLog(context.ReadValue<Vector2>());
        private void OnJumpChanged(InputAction.CallbackContext context) => JumpPressed?.Invoke();
        private void OnRunChanged(InputAction.CallbackContext context) => _isRunPressed = context.ReadValueAsButton();
        private void OnCrouchChanged(InputAction.CallbackContext context) => _isCrouchPressed = context.ReadValueAsButton();
        private void OnSwitch(InputAction.CallbackContext context) => ModeSwitchPressed?.Invoke();
        private void OnLootInteraction(InputAction.CallbackContext context) => LootInteractionPressed?.Invoke();
        

        #endregion

        #region PrivateMethods

        private void AddInputLog(Vector2 newInput)
        {
            _lastInput = _currentInput;
            _currentInput = newInput;
        }

        #endregion
    }
}
