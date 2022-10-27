using System;
using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.StateMachine.Structure.Explore.SubStates
{
    public class ExploreSlideSubState : MainCharacterExploreSubState
    {
        public ExploreSlideSubState(ExploreDto exploreDto, ExploreSubStateFactory factory) : base(exploreDto, factory) {}
        
        private Vector3 _lastMoveVector;
        private float _timeToLowerSpeed;
        private float _currentEvaluateTime;

        public override void OnStateEnter()
        {
            _lastMoveVector = ExploreDto.PlayerMover.GetLastMove(MoveType.StateMove, true);
            _lastMoveVector.y = 0f;

            _timeToLowerSpeed = ExploreDto.SlideModifierCurve.keys[ExploreDto.SlideModifierCurve.length - 1].time;

            ExploreDto.Stamina.IsRecovering = false;
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();

            _currentEvaluateTime = StateTimer <= _timeToLowerSpeed ? StateTimer : _timeToLowerSpeed;
            var slideMoveVector = _lastMoveVector * ExploreDto.SlideModifierCurve.Evaluate(_currentEvaluateTime);
            ExploreDto.PlayerMover.ChangeMove(MoveType.StateMove, slideMoveVector, RotationType.NoRotation);
        }

        public override void OnStateExit()
        {
            ExploreDto.Stamina.IsRecovering = true;
        }

        protected override bool TrySwitch(out MainCharacterExploreSubState newMainCharacterState)
        {
            newMainCharacterState = null;
            
            if (Math.Abs(_currentEvaluateTime - _timeToLowerSpeed) == 0) newMainCharacterState = Factory.Crouch();
            else if (ExploreDto.InputProcessor.GetIsCrouchPressed()) newMainCharacterState = Factory.Jump();
            
            return newMainCharacterState != null;
        }
    }
}