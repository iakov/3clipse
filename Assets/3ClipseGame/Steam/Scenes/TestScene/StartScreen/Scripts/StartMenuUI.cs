using _3ClipseGame.Steam.Scenes.TestScene.StartScreen.Scripts.Panels;
using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.TestScene.StartScreen.Scripts
{
    public class StartMenuUI : MonoBehaviour
    {
        [Header("Global settings")]
        [SerializeField] private Panel _savesPanel;
        [SerializeField] private Panel _journeyPanel;
        [SerializeField] private Panel _settingsPanel;
        
        [Header("Start")]
        [SerializeField] private Panel _currentPanel;

        private void OnEnable()
        {
            _currentPanel.Enable();
        }

        private void OnDisable()
        {
            _currentPanel.Disable();
        }

        public void Saves()
        {
            Debug.Log("Saves");
            _currentPanel.Disable();
            _currentPanel = _savesPanel;
            _currentPanel.Enable();
        }

        public void Journey()
        {
            Debug.Log("Journey");
            _currentPanel.Disable();
            _currentPanel = _journeyPanel;
            _currentPanel.Enable();
        }

        public void Settings()
        {
            Debug.Log("Settings");
            _currentPanel.Disable();
            _currentPanel = _settingsPanel;
            _currentPanel.Enable();
        }
    }
}
