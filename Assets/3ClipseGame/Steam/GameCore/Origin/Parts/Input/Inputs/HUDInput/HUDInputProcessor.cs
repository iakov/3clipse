using System;
using System.Collections;
using UnityEngine;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Input.Inputs.HUDInput
{
    public class HUDInputProcessor : InputProcessor
    {
        [SerializeField] private HUDInputHandler _hudInputHandler;

        public event Action<float> LootScrolled;
        public bool GetIsInteracted() => _isInteracted;
        public bool GetIsShowWheel() => _isShowWheel;
        public bool GetIsToggleMenu() => _isToggleMenu;

        private bool _isInteracted;
        private bool _isShowWheel;
        private bool _isToggleMenu;

        private void Awake()
        {
            _hudInputHandler.Interacted += OnInteracted;
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

        private void OnInteracted()
            => StartCoroutine(InteractedWithDelay());

        private IEnumerator InteractedWithDelay()
        {
            _isInteracted = true;
            yield return null;
            _isInteracted = false;
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
