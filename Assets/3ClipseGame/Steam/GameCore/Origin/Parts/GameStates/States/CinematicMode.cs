using UnityEngine;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.GameStates.States
{
    public class CinematicMode : GameMode
    {
        public override void StartEnter()
        {
            UIManager.HideEverything();
            
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