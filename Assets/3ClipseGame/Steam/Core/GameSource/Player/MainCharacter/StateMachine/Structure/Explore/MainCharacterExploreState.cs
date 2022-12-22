using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource.Player.MainCharacter.StateMachine.Structure.Explore
{
    public class MainCharacterExploreState : MainCharacterState
    {
        public MainCharacterExploreState(ExploreDto exploreDto, MainCharacterStateFactory factory) : base(factory)
        {
            _exploreDto = exploreDto;
        }

        private ExploreSubStateFactory _subStateFactory;
        private MainCharacterExploreSubState _currentMainCharacterSubState;
        private readonly ExploreDto _exploreDto;

        public override void OnStateEnter()
        {
            _subStateFactory = new ExploreSubStateFactory(_exploreDto);
            _currentMainCharacterSubState = _subStateFactory.Idle();
            _currentMainCharacterSubState.OnStateEnter();
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            
            if (_currentMainCharacterSubState.TrySwitchState(out var nextSubState)) SwitchSubState(nextSubState);
            _currentMainCharacterSubState.OnStateUpdate();
        }

        public override void OnStateExit()
        {
            _currentMainCharacterSubState.OnStateExit();
            _exploreDto.PlayerMover.ChangeMove(MoveType.StateMove, Vector3.zero, RotationType.NoRotation);
        }

        protected override bool TrySwitch(out MainCharacterState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (_exploreDto.InputProcessor.GetIsSwitched()) newMainCharacterState = Factory.ControlAnimal();
            
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
