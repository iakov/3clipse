using UnityEngine;
using UnityEngine.EventSystems;

namespace _3ClipseGame.Steam.Scenes.TestScene.StartScreen.Scripts.Buttons
{
    public class SettingsButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private RectTransform _mainCanvas;
        [SerializeField] private RectTransform _settingCanvas;

        public void OnPointerClick(PointerEventData eventData)
        {
            _mainCanvas.gameObject.SetActive(false);
            _settingCanvas.gameObject.SetActive(true);
        }
    }
}
