using _3ClipseGame.Steam.GameCore.Origin.Parts.Input;
using UnityEngine;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.GameStates.States
{
    public class PlayMode : GameMode
    {
        public override void StartEnter()
        {
            GameSource.Instance.GetPlayer().GetCurrentPlayerEntity().TakeControl();
            PointerManager.SwitchPointerMode(CursorMode);
            StartCoroutine(TrackBlendCompletion(EndEnter));
        }
        
        private void EndEnter()
        {
            EndEnterInput();
            UIManager.SwitchHUD(true);
            GameSource.Instance.GetInputManager().Enable(InputType.Movement);
            Time.timeScale = TimeScale;
        }

        private void EndEnterInput()
        {
            var inputAccessor = GameSource.Instance.GetInputManager();
            inputAccessor.Enable(InputType.HUD);
            inputAccessor.Enable(InputType.Camera);
        }

        public override void Exit()
        {
            UIManager.SwitchHUD(false);
            ExitInput();
        }

        private void ExitInput()
        {
            var inputAccessor = GameSource.Instance.GetInputManager();
            GameSource.Instance.GetPlayer().GetCurrentPlayerEntity().LoseControl();
            inputAccessor.Disable(InputType.Movement);
            inputAccessor.Disable(InputType.HUD);
            inputAccessor.Disable(InputType.Camera);
        }
    }
}
