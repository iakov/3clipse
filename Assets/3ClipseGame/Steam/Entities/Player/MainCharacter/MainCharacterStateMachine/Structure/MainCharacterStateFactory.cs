using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.ControlAnimal;
using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.Explore;
using _3ClipseGame.Steam.Entities.Player.Scripts;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure
{
    public class MainCharacterStateFactory : StateFactory
    {
        public MainCharacterStateFactory(ExploreDto exploreDto, ControlAnimalDto controlAnimalDto)
        {
            _exploreDto = exploreDto;
            _controlAnimalDto = controlAnimalDto;
        }

        private ControlAnimalDto _controlAnimalDto;
        private ExploreDto _exploreDto;

        public MainCharacterState Explore() => new MainCharacterExploreState(_exploreDto, this);
        public MainCharacterState ControlAnimal() => new MainCharacterControlAnimalState(_controlAnimalDto, this);
    }
}