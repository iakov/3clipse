using System;
using UnityEngine.InputSystem;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Input.Inputs.MenuInput
{
    public class MenuInputHandler : InputHandler
    {
        public event Action ExitPressed;
        private MenuInputMap _menuInputActions;

        private void Awake()
        {
            _menuInputActions = new MenuInputMap();
            _menuInputActions.MenuActions.Exit.started += OnExitPressed;
        }
        
        public override void Enable()
        {
            _menuInputActions.MenuActions.Enable();
        }

        public override void Disable()
        {
            _menuInputActions.MenuActions.Disable();
        }

        private void OnExitPressed(InputAction.CallbackContext context)
            => ExitPressed?.Invoke();
    }
}
