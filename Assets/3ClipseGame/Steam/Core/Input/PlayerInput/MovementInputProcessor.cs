using System.Collections;
using UnityEngine;

namespace _3ClipseGame.Steam.Core.Input.PlayerInput
{
    public class MovementInputProcessor
    {
        #region Initialization

        public MovementInputProcessor(MovementInputHandler handler)
        {
            _inputHandler = handler;
            Subscribe();
        }

        private MovementInputHandler _inputHandler;

        private void Subscribe()
        {
            _inputHandler.InputChanged += OnInputChanged;
            _inputHandler.SprintChanged += OnRunChanged;
            _inputHandler.CrouchChanged += OnCrouchChanged;
            _inputHandler.JumpPressed += OnJumpPressed;
            _inputHandler.ModeSwitchPressed += OnModeSwitchPressed;
        }

        private void Unsubscribe()
        {
            _inputHandler.InputChanged -= OnInputChanged;
            _inputHandler.SprintChanged -= OnRunChanged;
            _inputHandler.CrouchChanged -= OnCrouchChanged;
            _inputHandler.JumpPressed -= OnJumpPressed;
            _inputHandler.ModeSwitchPressed -= OnModeSwitchPressed;
        }

        #endregion

        #region EventHandlers

        private Vector2 _currentInput;
        private Vector2 _previousInput;

        private void OnInputChanged(Vector2 newInput)
        {
            _previousInput = _currentInput;
            _currentInput = newInput;
        }

        private bool _isRunPressed;

        private void OnRunChanged(bool isRunning)
        {
            _isRunPressed = isRunning;
        }

        private bool _isCrouchPressed;

        private void OnCrouchChanged(bool isCrouched)
        {
            _isCrouchPressed = isCrouched;
        }

        private bool _isJumpPressedRecently;

        private void OnJumpPressed()
        {
            _isJumpPressedRecently = true;
            _inputHandler.StartCoroutine(JumpButtonDelay());
        }

        private IEnumerator JumpButtonDelay()
        {
            yield return null;
            _isJumpPressedRecently = false;
        }

        private bool _isSwitchPressedRecently;

        private void OnModeSwitchPressed()
        {
            _isSwitchPressedRecently = true;
            _inputHandler.StartCoroutine(SwitchButtonDelay());
        }

        private IEnumerator SwitchButtonDelay()
        {
            yield return null;
            _isJumpPressedRecently = false;
        }
        
        #endregion

        #region Public

        public Vector2 GetCurrentInput() => _currentInput;
        public Vector2 GetPreviousInput() => _previousInput;
        
        public bool GetIsRunPressed() => _isRunPressed;
        public bool GetIsCrouchPressed() => _isCrouchPressed;
        
        public bool GetIsJumpPressedRecently() => _isJumpPressedRecently;
        public bool GetIsSwitchPressedRecently() => _isSwitchPressedRecently;

        #endregion
    }
}