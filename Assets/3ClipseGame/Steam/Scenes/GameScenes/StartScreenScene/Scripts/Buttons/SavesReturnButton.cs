using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.GameScenes.StartScreenScene.Scripts.Buttons
{
    public class SavesReturnButton : MonoBehaviour
    {
        [SerializeField] private StartMenuUI _startMenuUI;

        public void EnableMain()
        {
            _startMenuUI.Journey();
        }
    }
}
