using _3ClipseGame.Steam.Core.GameSource.Player.MainCharacter.StateMachine.Structure.ControlAnimal;
using _3ClipseGame.Steam.Core.GameSource.Player.MainCharacter.StateMachine.Structure.Explore;
using _3ClipseGame.Steam.Core.GameSource.Player.Scripts;

namespace _3ClipseGame.Steam.Core.GameSource.Player.MainCharacter.StateMachine.Structure
{
    public class MainCharacterStateFactory : StateFactory
    {
        public MainCharacterStateFactory(ExploreDto exploreDto, ControlAnimalDto controlAnimalDto)
        {
            _exploreDto = exploreDto;
            _controlAnimalDto = controlAnimalDto;
        }

        private readonly ControlAnimalDto _controlAnimalDto;
        private readonly ExploreDto _exploreDto;

        public MainCharacterState Explore() => new MainCharacterExploreState(_exploreDto, this);
        public MainCharacterState ControlAnimal() => new MainCharacterControlAnimalState(_controlAnimalDto, this);
    }
}