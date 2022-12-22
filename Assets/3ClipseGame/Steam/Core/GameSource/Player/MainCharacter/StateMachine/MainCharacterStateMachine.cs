using _3ClipseGame.Steam.Core.GameSource.Player.MainCharacter.StateMachine.Structure;
using _3ClipseGame.Steam.Core.GameSource.Player.MainCharacter.StateMachine.Structure.ControlAnimal;
using _3ClipseGame.Steam.Core.GameSource.Player.MainCharacter.StateMachine.Structure.Explore;
using UnityEngine;
using CharacterController = _3ClipseGame.Steam.Entities.Scripts.CustomController.CharacterController;

namespace _3ClipseGame.Steam.Core.GameSource.Player.MainCharacter.StateMachine
{
    [RequireComponent(typeof(CharacterController))]
    public class MainCharacterStateMachine : Scripts.StateMachine
    {
        private MainCharacterState _currentMainCharacterState;
        private MainCharacterStateFactory _mainCharacterStateFactory;
        
        [SerializeField] private ExploreDto _exploreDto;
        [SerializeField] private ControlAnimalDto _controlAnimalDto;

        private void Awake()
        {
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
