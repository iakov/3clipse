using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.States.GameStates
{
    public class CinematicMode : GameMode
    {
        public override void StartEnter()
        {
            UIManager.SwitchMenu(false);
            UIManager.SwitchHUD(false);
            
            GameSource.Instance.GetInputManager().DisableAll();
            PointerManager.SwitchPointerMode(CursorLockMode.Locked);
        }
        
        protected override void EndEnter()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}