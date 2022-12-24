using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.GameScenes.StartScreenScene.Scripts.Buttons
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
