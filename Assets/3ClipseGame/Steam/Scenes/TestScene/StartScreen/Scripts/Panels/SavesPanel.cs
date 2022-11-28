using UnityEngine;
using UnityEngine.EventSystems;

namespace _3ClipseGame.Steam.Scenes.TestScene.StartScreen.Scripts.Panels
{
    public class SavesPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private RectTransform _panelVisual;
        [SerializeField] private CursorScript _cursorScript;
        [SerializeField] private FirstScreenCamera _firstScreenCamera;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _cursorScript.UIMode(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _cursorScript.GameMode(true);
        }
        
        public void Enable()
        {
            Cursor.visible = false;
            _firstScreenCamera.MoveToPoint(4);
            _firstScreenCamera.MoveFinished += EndEnable;
        }

        private void EndEnable()
        {
            _cursorScript.GameMode(true);
            _panelVisual.gameObject.SetActive(true);
        }
    }
}
