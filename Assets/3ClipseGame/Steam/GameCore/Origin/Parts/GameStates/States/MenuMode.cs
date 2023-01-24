using _3ClipseGame.Steam.GameCore.Origin.Parts.Input;
using _3ClipseGame.Steam.GameCore.Origin.Parts.UserInterface;
using UnityEngine;
using CameraType = _3ClipseGame.Steam.GameCore.Origin.Parts.Camera.CameraType;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.GameStates.States
{
    public class MenuMode : GameMode
    {
        [SerializeField] private GameObject _menuPanel;
        
        public override void StartEnter()
        {
            GameSource.Instance.GetCameraManager().Enable(CameraType.Menu);
            PointerManager.SwitchPointerMode(CursorMode);
            StartCoroutine(TrackBlendCompletion(EndEnter));
        }
        
        private void EndEnter()
        {
            UIManager.DrawNewPanel(_menuPanel, DrawMode.Mono);
            GameSource.Instance.GetInputManager().Enable(InputType.Menu);
        }

        public override void Exit()
        {
            UIManager.HidePanel(_menuPanel);
            GameSource.Instance.GetInputManager().Disable(InputType.Menu);
        }
    }
}
