using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.TestScene.StartScreen.Scripts.Buttons
{
    public class SettingsButton : MonoBehaviour
    {
        [SerializeField] private StartMenuUI _startMenuUI;

        public void EnterSettings()
        {
            _startMenuUI.Settings();
        }
    }
}
