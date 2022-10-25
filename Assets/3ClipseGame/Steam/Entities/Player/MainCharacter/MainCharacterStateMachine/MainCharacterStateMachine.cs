using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure;
using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.ControlAnimal;
using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.Explore;
using _3ClipseGame.Steam.Entities.Player.Scripts;
using UnityEngine;
using CharacterController = _3ClipseGame.Steam.Entities.Scripts.CustomController.CharacterController;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine
{
    [RequireComponent(typeof(CharacterController))]
    public class MainCharacterStateMachine : StateMachine
    {
        private MainCharacterState _currentMainCharacterState;
        private MainCharacterStateFactory _mainCharacterStateFactory;
        
        private ExploreDto _exploreDto;
        private ControlAnimalDto _controlAnimalDto;

        private void Awake()
        {
            _exploreDto = GetComponent<ExploreDto>();
            _controlAnimalDto = GetComponent<ControlAnimalDto>();
            
            _mainCharacterStateFactory = new MainCharacterStateFactory(_exploreDto, _controlAnimalDto);
            _currentMainCharacterState = _mainCharacterStateFactory.Explore();
        }

        private void Start()
        {
            _currentMainCharacterState.OnStateEnter();
        }

        public override void UpdateWork()
        {
            if (_currentMainCharacterState != null)
            {
                if (_currentMainCharacterState.TrySwitchState(out var nextState)) SwitchState(nextState);
                _currentMainCharacterState.OnStateUpdate();
            }
        }

        private void SwitchState(MainCharacterState nextMainCharacterState)
        {
            _currentMainCharacterState.OnStateExit();
            _currentMainCharacterState = nextMainCharacterState;
            _currentMainCharacterState.OnStateEnter();
        }
    }
}
