using _3ClipseGame.Steam.GameCore.Origin.Parts.Input;
using CameraType = _3ClipseGame.Steam.GameCore.Origin.Parts.Camera.CameraType;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.GameStates.States
{
    public class MenuMode : GameMode
    {
        public override void StartEnter()
        {
            GameSource.Instance.GetCameraManager().Enable(CameraType.Menu);
            PointerManager.SwitchPointerMode(CursorMode);
            StartCoroutine(TrackBlendCompletion(EndEnter));
        }
        
        private void EndEnter()
        {
            UIManager.SwitchMenu(true);
            GameSource.Instance.GetInputManager().Enable(InputType.Menu);
        }

        public override void Exit()
        {
            UIManager.SwitchMenu(false);
            GameSource.Instance.GetInputManager().Disable(InputType.Menu);
        }
    }
}
