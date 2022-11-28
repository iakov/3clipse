using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.TestScene.StartScreen.Scripts.Panels
{
    public class SavesPanel : Panel
    {
        [SerializeField] private RectTransform _panelVisual;
        [SerializeField] private CursorScript _cursorScript;
        [SerializeField] private FirstScreenCamera _firstScreenCamera;
        [SerializeField] private float _pointToMoveTo;
        
        public override void Enable()
        {
            _cursorScript.SwitchCursorVisibility(false);

            _firstScreenCamera.MoveFinished += EndEnable;
            _firstScreenCamera.MoveToPoint(_pointToMoveTo);
        }

        private void EndEnable()
        {
            _firstScreenCamera.MoveFinished -= EndEnable;

            _cursorScript.SwitchObjectTrack(true);
            _cursorScript.SwitchObjectVisibility(true);
            _panelVisual.gameObject.SetActive(true);
        }

        public override void Disable()
        {
            _panelVisual.gameObject.SetActive(false);
        }
    }
}
