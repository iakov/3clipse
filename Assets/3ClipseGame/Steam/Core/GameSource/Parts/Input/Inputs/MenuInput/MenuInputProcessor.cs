using System.Collections;
using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Input.Inputs.MenuInput
{
    public class MenuInputProcessor : InputProcessor
    {
        [SerializeField] private MenuInputHandler _menuInputHandler;

        public bool GetIsExitPressed() => _isExitPressed;

        private bool _isExitPressed;

        private void Awake()
        {
            _menuInputHandler.ExitPressed += OnExitPressed;
        }

        public override void Enable()
        {
            _menuInputHandler.Enable();
        }

        public override void Disable()
        {
            _menuInputHandler.Disable();
        }

        private void OnExitPressed()
            => StartCoroutine(ExitWithDelay());

        private IEnumerator ExitWithDelay()
        {
            _isExitPressed = true;
            yield return null;
            _isExitPressed = false;
        }
    }
}