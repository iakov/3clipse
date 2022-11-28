using UnityEngine;
using UnityEngine.InputSystem;

namespace _3ClipseGame.Steam.Scenes.TestScene.StartScreen.Scripts.Buttons
{
    public class ClosePanelWithButton : MonoBehaviour
    {
        [SerializeField] private InputAction _inputAction;
        [SerializeField] private StartMenuUI _startMenuUI;

        private void Start() => _inputAction.Enable();

        private void OnEnable() => _inputAction.started += GoToJourney;

        private void OnDisable() => _inputAction.started -= GoToJourney;

        private void GoToJourney(InputAction.CallbackContext context)
        {
            _startMenuUI.Journey();
        }
    } 
}
