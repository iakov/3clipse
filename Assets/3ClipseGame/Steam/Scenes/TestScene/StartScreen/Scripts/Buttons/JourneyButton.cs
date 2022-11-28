using _3ClipseGame.Steam.Scenes.TestScene.StartScreen.Scripts.Panels;
using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.TestScene.StartScreen.Scripts.Buttons
{
    public class JourneyButton : MonoBehaviour
    {
        [SerializeField] private SavesPanel _savesPanel;
        [SerializeField] private JourneyPanel _journeyPanel;

        public void GoToSaves()
        {
            _savesPanel.Enable();
            _journeyPanel.gameObject.SetActive(false);
        }
    }
}
