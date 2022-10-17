using System;
using UnityEngine.InputSystem;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Input.Inputs.HUDInput
{
    public class HUDInputHandler : InputHandler
    {
        public event Action<bool> ShowWheelChanged;
        public event Action ToggleMainMenuPressed;
        public event Action<float> LootScrollPerformed;
        public event Action LootInteracted;
        
        private HUDInputMap _hudInputActions;

        private void Awake()
        {
            _hudInputActions = new HUDInputMap();

            _hudInputActions.HUDActions.ShowElementalWheel.started += OnShowWheelChanged;
            _hudInputActions.HUDActions.ShowElementalWheel.canceled += OnShowWheelChanged;

            _hudInputActions.HUDActions.ToggleMainMenu.started += OnToggleMainMenuPressed;

            _hudInputActions.HUDActions.LootScroll.performed += OnLootScrollPerformed;

            _hudInputActions.HUDActions.LootInteracted.started += OnLootInteracted;
        }
        
        public override void Enable()
        {
            _hudInputActions.HUDActions.Enable();
        }

        public override void Disable()
        {
            _hudInputActions.HUDActions.Disable();
        }

        private void OnShowWheelChanged(InputAction.CallbackContext context)
            => ShowWheelChanged?.Invoke(context.ReadValueAsButton());

        private void OnToggleMainMenuPressed(InputAction.CallbackContext context)
            => ToggleMainMenuPressed?.Invoke();
        

        private void OnLootScrollPerformed(InputAction.CallbackContext context)
            => LootScrollPerformed?.Invoke(context.ReadValue<float>());
        
        private void OnLootInteracted(InputAction.CallbackContext context)
            => LootInteracted?.Invoke();
    }
}
