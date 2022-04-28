namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.SubStates.ExploreSubStates
{
    public class ExploreSubStatesFactory : MainCharacterSubStateFactory
    {
        #region Initialization

        public ExploreSubStatesFactory(MainCharacterStateMachine context) : base(context){}

        #endregion

        #region Methods

        public MainCharacterSubState Walk() => new ExploreWalkSubState(Context, this);
        public MainCharacterSubState Idle() => new ExploreIdleSubState(Context, this);
        public MainCharacterSubState Run() => new ExploreRunSubState(Context, this);
        public MainCharacterSubState Stop() => new ExploreStopSubState(Context, this);
        public MainCharacterSubState Crouch() => new ExploreCrouchSubState(Context, this);
        public MainCharacterSubState Fall() => new ExploreFallSubState(Context, this);
        public MainCharacterSubState Jump() => new ExploreJumpSubState(Context, this);

        #endregion
    }
}
