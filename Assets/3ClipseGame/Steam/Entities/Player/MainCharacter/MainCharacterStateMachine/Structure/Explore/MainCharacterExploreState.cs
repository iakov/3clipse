using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.Explore
{
    public class MainCharacterExploreState : MainCharacterState
    {
        public MainCharacterExploreState(ExploreDto exploreDto, MainCharacterStateFactory factory) : base(factory)
            => _exploreDto = exploreDto;

        private ExploreSubStateFactory _subStateFactory;
        private ExploreDto _exploreDto;
        private MainCharacterExploreSubState _currentMainCharacterSubState;
        
        private int _framesFromSwitch;

        public override void OnStateEnter()
        {
            _subStateFactory = new ExploreSubStateFactory(_exploreDto);
            _currentMainCharacterSubState = _subStateFactory.Idle();
            _currentMainCharacterSubState.OnStateEnter();
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();

            _framesFromSwitch++;
            
            if (_currentMainCharacterSubState.TrySwitchState(out var nextSubState)) SwitchSubState(nextSubState);
            _currentMainCharacterSubState.OnStateUpdate();
        }

        public override void OnStateExit()
        {
            _exploreDto.PlayerMover.ChangeMove(MoveType.StateMove, Vector3.zero, RotationType.NoRotation);
        }

        public override bool TrySwitchState(out MainCharacterState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (_exploreDto.InputProcessor.GetIsSwitched() && _framesFromSwitch >= 2) newMainCharacterState = Factory.ControlAnimal();
            
            return newMainCharacterState != null;
        }

        private void SwitchSubState(MainCharacterExploreSubState nextMainCharacterSubState)
        {
            _currentMainCharacterSubState.OnStateExit();
            _currentMainCharacterSubState = nextMainCharacterSubState;
            _currentMainCharacterSubState.OnStateEnter();
        }
    }
}
