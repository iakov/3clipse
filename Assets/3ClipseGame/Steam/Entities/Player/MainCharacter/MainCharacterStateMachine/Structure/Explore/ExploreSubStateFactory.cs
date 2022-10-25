using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.Explore.SubStates;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.Explore
{
    public class ExploreSubStateFactory : MainCharacterSubStateFactory
    {
        public ExploreSubStateFactory(ExploreDto exploreDto)
        {
            _exploreDto = exploreDto;
        }

        private ExploreDto _exploreDto;

        public MainCharacterExploreSubState Idle() => new ExploreIdleSubState(_exploreDto, this);
        public MainCharacterExploreSubState Walk() => new ExploreWalkSubState(_exploreDto, this);
        public MainCharacterExploreSubState Run() => new ExploreRunSubState(_exploreDto, this);
        public MainCharacterExploreSubState Jump() => new ExploreJumpSubState(_exploreDto, this);
        public MainCharacterExploreSubState Fall() => new ExploreFallSubState(_exploreDto, this);
        public MainCharacterExploreSubState Stop() => new ExploreStopSubState(_exploreDto, this);
        public MainCharacterExploreSubState Slide() => new ExploreSlideSubState(_exploreDto, this);
        public MainCharacterExploreSubState Crouch() => new ExploreCrouchSubState(_exploreDto, this);
    }
}