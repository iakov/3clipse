using UnityEngine;
using UnityEngine.InputSystem;

namespace _3ClipseGame.Steam.Scenes.TestScene.StartScreen.Scripts.Panels
{
    public class SettingsPanel : MonoBehaviour
    {
        [SerializeField] private RectTransform _mainButtons;
        [SerializeField] private InputAction _exitButton;
        
        private void Start()
        {
            _exitButton.Enable();
        }
        
        private void OnEnable()
        {
            _exitButton.started += OnExitPressed;
        }
        
        private void OnDisable()
        {
            _exitButton.started -= OnExitPressed;
        }

        private void OnExitPressed(InputAction.CallbackContext context)
        {
            _mainButtons.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
