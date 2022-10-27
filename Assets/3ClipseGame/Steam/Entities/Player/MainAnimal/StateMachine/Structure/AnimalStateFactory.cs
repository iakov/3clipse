using _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.AI;
using _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.Play;
using _3ClipseGame.Steam.Entities.Player.Scripts;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure
{
    public class AnimalStateFactory : StateFactory
    {
        public AnimalStateFactory(AnimalPlayDto playDto, AnimalAIDto aiDto)
        {
            _playDto = playDto;
            _aiDto = aiDto;
        }

        private readonly AnimalPlayDto _playDto;
        private readonly AnimalAIDto _aiDto;

        public AnimalState AnimalPlayState() => new AnimalPlayState(_playDto, this);
        public AnimalState AnimalAIState() => new AnimalAIState(_aiDto, this);
    }
}
