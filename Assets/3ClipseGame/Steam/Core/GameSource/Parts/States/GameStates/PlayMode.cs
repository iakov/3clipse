using _3ClipseGame.Steam.Core.GameSource.Parts.Input;
using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.States.GameStates
{
    public class PlayMode : GameMode
    {
        public override void StartEnter()
        {
            GameSource.Instance.GetPlayer().GetCurrentPlayerEntity().TakeControl();
            PointerManager.SwitchPointerMode(CursorLockMode.Locked);
            StartCoroutine(TrackBlendCompletion());
        }
        
        protected override void EndEnter()
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
