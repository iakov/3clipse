using _3ClipseGame.Steam.GameCore.Origin.Parts.Input;
using UnityEngine;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.GameStates.States
{
    public class CinematicMode : GameMode
    {
        public override void StartEnter()
        {
            UIManager.SwitchMenu(false);
            UIManager.SwitchHUD(false);
            UIManager.SwitchDialogue(true);
            
            // GameSource.Instance.GetInputManager().DisableAll();
            // PointerManager.SwitchPointerMode(CursorMode);
            PointerManager.SwitchPointerMode(CursorLockMode.None);
            StartCoroutine(TrackBlendCompletion(EndEnter));
        }
        
        private void EndEnter()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}