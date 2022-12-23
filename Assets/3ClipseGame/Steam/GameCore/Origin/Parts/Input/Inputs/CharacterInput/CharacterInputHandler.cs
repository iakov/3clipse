using System;
using UnityEngine.InputSystem;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Input.Inputs.CharacterInput
{
    public class CharacterInputHandler : InputHandler
    {
        public event Action EnvironmentInteracted;
        
        private PlayerInputMap _playerInputMap;

        private void Awake()
        {
            _playerInputMap = new PlayerInputMap();
            
            _playerInputMap.PlayerInput.EnvironmentInteraction.started += OnEnvironmentInteracted;
        }

        private void OnEnable()
        {
            Enable();
        }

        public override void Enable()
        {
            _playerInputMap.PlayerInput.Enable();
        }

        private void OnDisable()
        {
            Disable();
        }

        public override void Disable()
        {
            _playerInputMap.PlayerInput.Disable();
        }

        private void OnEnvironmentInteracted(InputAction.CallbackContext context)
            => EnvironmentInteracted?.Invoke();
    }
}
