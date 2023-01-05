using System.Collections;
using UnityEngine;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Input.Inputs.HUDInput
{
    public class HUDInputProcessor : InputProcessor
    {
        [SerializeField] private HUDInputHandler _hudInputHandler;
        
        public bool GetIsShowWheel() => _isShowWheel;
        public bool GetIsToggleMenu() => _isToggleMenu;

        private bool _isShowWheel;
        private bool _isToggleMenu;

        private void Awake()
        {
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
