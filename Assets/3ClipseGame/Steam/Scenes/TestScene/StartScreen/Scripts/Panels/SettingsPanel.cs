using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.TestScene.StartScreen.Scripts.Panels
{
    public class SettingsPanel : Panel
    {
        [SerializeField] private RectTransform _panelVisual;
        [SerializeField] private CursorScript _cursorScript;
        [SerializeField] private FirstScreenCamera _firstScreenCamera;
        [SerializeField] private float _pointToMoveTo;

        public override void Enable()
        {
            _cursorScript.SwitchCursorVisibility(true);
            _cursorScript.SwitchObjectTrack(false);
            
            _firstScreenCamera.MoveFinished += EndEnable;
            _firstScreenCamera.MoveToPoint(_pointToMoveTo);
        }

        private void EndEnable()
        {
            _firstScreenCamera.MoveFinished -= EndEnable;
            
            _panelVisual.gameObject.SetActive(true);
        }

        public override void Disable()
        {
            _panelVisual.gameObject.SetActive(false);
        }
    }
}
