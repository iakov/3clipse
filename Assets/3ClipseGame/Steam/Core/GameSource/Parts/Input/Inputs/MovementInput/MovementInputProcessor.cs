using System.Collections;
using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Input.Inputs.MovementInput
{
    public class MovementInputProcessor : InputProcessor
    {
        [SerializeField] private MovementInputHandler _inputHandler;

        public Vector2 GetCurrentInput() => _currentInput;
        public Vector2 GetPreviousInput() => _previousInput;
        public bool GetIsJumpPressed() => _isJumped;
        public bool GetIsCrouchPressed() => _isCrouched;
        public bool GetIsSprintPressed() => _isSprinted;
        public bool GetIsSwitched() => _isSwitched;

        private Vector2 _currentInput;
        private Vector2 _previousInput;
        private bool _isJumped;
        private bool _isCrouched;
        private bool _isSprinted;
        private bool _isSwitched;

        private void Awake()
        {
            _inputHandler.CrouchChanged += OnCrouchChanged;
            _inputHandler.InputChanged += OnInputChanged;
            _inputHandler.SprintChanged += OnSprintChanged;
            _inputHandler.JumpPressed += OnJumpPressed;
            _inputHandler.SwitchToAnotherEntityPressed += OnSwitchPressed;
        }
        
        public override void Enable()
        {
            _inputHandler.Enable();
        }

        public override void Disable()
        {
            _inputHandler.Disable();
            ResetVariables();
        }

        private void ResetVariables()
        {
            _previousInput = _currentInput;
            _currentInput = Vector2.zero;
            _isCrouched = false;
            _isSprinted = false;
            _isJumped = false;
        }

        private void OnCrouchChanged(bool isCrouched)
            => _isCrouched = isCrouched;

        private void OnInputChanged(Vector2 input)
            => AddLog(input);

        private void AddLog(Vector2 input)
        {
            _previousInput = _currentInput;
            _currentInput = input;
        }

        private void OnSprintChanged(bool isSprinted)
            => _isSprinted = isSprinted;

        private void OnJumpPressed()
            => StartCoroutine(JumpWithDelay());

        private IEnumerator JumpWithDelay()
        {
            _isJumped = true;
            yield return null;
            _isJumped = false;
        }

        private void OnSwitchPressed()
            => StartCoroutine(SwitchWithDelay());

        private IEnumerator SwitchWithDelay()
        {
            _isSwitched = true;
            yield return null;
            _isSwitched = false;
        }
    }
}
