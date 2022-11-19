namespace _3ClipseGame.Steam.Core.GameSource.Parts.States.GameStates
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
