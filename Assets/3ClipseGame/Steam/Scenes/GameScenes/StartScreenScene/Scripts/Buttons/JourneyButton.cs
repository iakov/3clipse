using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.GameScenes.StartScreenScene.Scripts.Buttons
{
    public class JourneyButton : MonoBehaviour
    {
        [SerializeField] private StartMenuUI _startMenuUI;

        public void EnterSaves()
        {
            _startMenuUI.Saves();
        }
    }
}
