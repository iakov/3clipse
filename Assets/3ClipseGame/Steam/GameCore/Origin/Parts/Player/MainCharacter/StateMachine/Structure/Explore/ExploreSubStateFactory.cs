using _3ClipseGame.Steam.GameCore.Origin.Parts.Player.MainCharacter.StateMachine.Structure.Explore.SubStates;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Player.MainCharacter.StateMachine.Structure.Explore
{
    public class ExploreSubStateFactory : MainCharacterSubStateFactory
    {
        public ExploreSubStateFactory(ExploreDto exploreDto)
        {
            _exploreDto = exploreDto;
        }

        private readonly ExploreDto _exploreDto;

        public MainCharacterExploreSubState Idle() => new ExploreIdleSubState(_exploreDto, this);
        public MainCharacterExploreSubState Walk() => new ExploreWalkSubState(_exploreDto, this);
    }
}