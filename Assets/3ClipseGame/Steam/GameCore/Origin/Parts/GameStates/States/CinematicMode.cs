namespace _3ClipseGame.Steam.GameCore.Origin.Parts.GameStates.States
{
    public class CinematicMode : GameMode
    {
        public override void StartEnter()
        {
            UIManager.SwitchMenu(false);
            UIManager.SwitchHUD(false);
            
            GameSource.Instance.GetInputManager().DisableAll();
            PointerManager.SwitchPointerMode(CursorMode);
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