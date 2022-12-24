using _3ClipseGame.Steam.Scenes.GameScenes.StartScreenScene.Scripts.Panels;
using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.GameScenes.StartScreenScene.Scripts
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
            _currentPanel.Disable();
            _currentPanel = _savesPanel;
            _currentPanel.Enable();
        }

        public void Journey()
        {
            _currentPanel.Disable();
            _currentPanel = _journeyPanel;
            _currentPanel.Enable();
        }

        public void Settings()
        {
            _currentPanel.Disable();
            _currentPanel = _settingsPanel;
            _currentPanel.Enable();
        }
    }
}
