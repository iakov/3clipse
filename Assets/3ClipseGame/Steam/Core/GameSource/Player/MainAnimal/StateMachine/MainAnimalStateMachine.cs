using _3ClipseGame.Steam.Core.GameSource.Player.MainAnimal.StateMachine.Structure;
using _3ClipseGame.Steam.Core.GameSource.Player.MainAnimal.StateMachine.Structure.AI;
using _3ClipseGame.Steam.Core.GameSource.Player.MainAnimal.StateMachine.Structure.Play;
using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource.Player.MainAnimal.StateMachine
{
    [RequireComponent(typeof(AnimalAIDto))]
    [RequireComponent(typeof(AnimalPlayDto))]
    public class MainAnimalStateMachine : Player.Scripts.StateMachine
    {
        private AnimalState _currentMainAnimalState;
        private AnimalStateFactory _mainAnimalStateFactory;

        private AnimalPlayDto _playDto;
        private AnimalAIDto _aiDto;

        private void Start()
        {
            _aiDto = GetComponent<AnimalAIDto>();
            _playDto = GetComponent<AnimalPlayDto>();

            _mainAnimalStateFactory = new AnimalStateFactory(_playDto, _aiDto);
            _currentMainAnimalState = _mainAnimalStateFactory.AnimalAIState();
            _currentMainAnimalState.OnStateEnter();
        }

        public override void UpdateWork()
        {
            if(_currentMainAnimalState.TrySwitchState(out var newState)) SwitchState(newState);
            _currentMainAnimalState.OnStateUpdate();
        }

        private void SwitchState(AnimalState newState)
        {
            _currentMainAnimalState.OnStateExit();
            _currentMainAnimalState = newState;
            _currentMainAnimalState.OnStateEnter();
        }
    }
}