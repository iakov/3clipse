namespace Assets._3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates.ExploreSubStates
{
    public class ExploreSubStatesFactory : SubStateFactory
    {
        #region Initialization

        public ExploreSubStatesFactory(PlayerStateMachine context) : base(context){}

        #endregion

        #region Methods

        public SubState Walk() => new ExploreWalkSubState(Context, this);
        public SubState Idle() => new ExploreIdleSubState(Context, this);
        public SubState Run() => new ExploreRunSubState(Context, this);
        public SubState Stop() => new ExploreStopSubState(Context, this);
        public SubState Crouch() => new ExploreCrouchSubState(Context, this);

        #endregion
    }
}
