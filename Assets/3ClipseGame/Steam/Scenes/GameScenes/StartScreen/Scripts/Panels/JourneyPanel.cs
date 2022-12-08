using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.GameScenes.StartScreen.Scripts.Panels
{
    public class JourneyPanel : Panel
    {
        [SerializeField] private RectTransform _panelVisual;
        [SerializeField] private CursorScript _cursorScript;
        [SerializeField] private HeadLookAt _headLookAtScript;
        [SerializeField] private FirstScreenCamera _firstScreenCamera;
        [SerializeField] private float _pointToMoveTo;

        public override void Enable()
        {
            _cursorScript.SwitchCursorVisibility(false);
            _cursorScript.SwitchObjectTrack(true);
            _cursorScript.SwitchObjectVisibility(true);
            
            _firstScreenCamera.MoveFinished += EndEnable;
            _firstScreenCamera.MoveToPoint(_pointToMoveTo);
        }

        private void EndEnable()
        {
            _firstScreenCamera.MoveFinished -= EndEnable;
            
            _panelVisual.gameObject.SetActive(true);
            _headLookAtScript.Switch(true);
        }

        public override void Disable()
        {
            _panelVisual.gameObject.SetActive(false);
            _headLookAtScript.Switch(false);
        }
    }
}
