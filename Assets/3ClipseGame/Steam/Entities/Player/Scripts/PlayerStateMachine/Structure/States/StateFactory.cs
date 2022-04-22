namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.States
{
    public class StateFactory
    {
        #region Initialization

        public StateFactory(PlayerStateMachine context) => Context = context;
        protected readonly PlayerStateMachine Context;

        #endregion

        #region Methods

        public State ExploreState() => new ExploreState(Context, this);

        #endregion
    }
}
