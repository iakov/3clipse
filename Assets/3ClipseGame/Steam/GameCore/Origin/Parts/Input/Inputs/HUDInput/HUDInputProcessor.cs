using System;
using System.Collections;
using UnityEngine;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Input.Inputs.HUDInput
{
    public class HUDInputProcessor : InputProcessor
    {
        [SerializeField] private HUDInputHandler _hudInputHandler;

        public event Action<float> LootScrolled;
        public bool GetIsLootInteracted() => _isLootInteracted;
        public bool GetIsShowWheel() => _isShowWheel;
        public bool GetIsToggleMenu() => _isToggleMenu;

        private bool _isLootInteracted;
        private bool _isShowWheel;
        private bool _isToggleMenu;

        private void Awake()
        {
            _hudInputHandler.LootInteracted += OnLootInteracted;
            _hudInputHandler.LootScrollPerformed += OnLootScrollPerformed;
            _hudInputHandler.ShowWheelChanged += OnShowWheelChanged;
            _hudInputHandler.ToggleMainMenuPressed += OnToggleMainMenuPressed;
        }

        public override void Enable()
        {
            _hudInputHandler.Enable();
        }

        public override void Disable()
        {
            _hudInputHandler.Disable();
        }

        private void OnLootInteracted()
            => StartCoroutine(LootInteractedWithDelay());

        private IEnumerator LootInteractedWithDelay()
        {
            _isLootInteracted = true;
            yield return null;
            _isLootInteracted = false;
        }

        private void OnLootScrollPerformed(float value)
            => LootScrolled?.Invoke(value);

        private void OnShowWheelChanged(bool isVisible)
            => _isShowWheel = isVisible;

        private void OnToggleMainMenuPressed()
            => StartCoroutine(ToggleWithDelay());
        
            
        private IEnumerator ToggleWithDelay()
        {
            _isToggleMenu = true;
            yield return null;
            _isToggleMenu = false;
        }
    }
}
