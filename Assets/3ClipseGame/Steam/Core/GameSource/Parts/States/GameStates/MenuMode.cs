using _3ClipseGame.Steam.Core.GameSource.Parts.Input;
using CameraType = _3ClipseGame.Steam.Core.GameSource.Parts.Camera.CameraType;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.States.GameStates
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
