namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.Explore
{
    public abstract class MainCharacterExploreSubState : MainCharacterSubState<ExploreSubStateFactory, MainCharacterExploreSubState>
    {
        protected MainCharacterExploreSubState(ExploreDto exploreDto, ExploreSubStateFactory factory) : base(factory)
            => ExploreDto = exploreDto;

        protected ExploreDto ExploreDto;
    }
}