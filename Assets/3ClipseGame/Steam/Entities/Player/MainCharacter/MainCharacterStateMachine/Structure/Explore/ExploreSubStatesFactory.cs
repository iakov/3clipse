using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.Explore.SubStates;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.Explore
{
    public class ExploreSubStatesFactory : MainCharacterSubStateFactory
    {
        public ExploreSubStatesFactory(MainCharacterStateMachine context) : base(context){}
        
        public MainCharacterSubState Walk() => new ExploreWalkSubState(Context, this);
        public MainCharacterSubState Idle() => new ExploreIdleSubState(Context, this);
        public MainCharacterSubState Run() => new ExploreRunSubState(Context, this);
        public MainCharacterSubState Stop() => new ExploreStopSubState(Context, this);
        public MainCharacterSubState Crouch() => new ExploreCrouchSubState(Context, this);
        public MainCharacterSubState Fall() => new ExploreFallSubState(Context, this);
        public MainCharacterSubState Jump() => new ExploreJumpSubState(Context, this);
        public MainCharacterSubState Slide() => new ExploreSlideSubState(Context, this);
    }
}