namespace _3ClipseGame.Steam.GameCore.Origin.Parts.GameStates.States
{
    public class IndependentMode : GameMode
    {
        public override void StartEnter()
        {
            StartCoroutine(TrackBlendCompletion(EndEnter));
            //Nothing
        }

        public override void Exit()
        {
            //Nothing
        }

        private void EndEnter()
        {
            //Nothing
        }
    }
}
