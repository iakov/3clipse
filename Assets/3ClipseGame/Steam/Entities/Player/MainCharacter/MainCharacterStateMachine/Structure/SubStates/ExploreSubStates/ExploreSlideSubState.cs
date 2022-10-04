using System;
using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.SubStates.ExploreSubStates
{
    public class ExploreSlideSubState : MainCharacterSubState
    {
        #region Initialization

        public ExploreSlideSubState(MainCharacterStateMachine context, MainCharacterSubStateFactory factory) : base(context, factory) =>
            _factory = (ExploreSubStatesFactory) factory;
        
        private ExploreSubStatesFactory _factory;
        private Vector3 _lastMoveVector;
        private float _timeToLowerSpeed;
        
        private float _currentEvaluateTime;

        #endregion
        
        #region SubStateMethods
        
        public override void OnStateEnter()
        {
            _lastMoveVector = Context.PlayerMover.GetLastMove(MoveType.StateMove, true);
            _lastMoveVector.y = 0f;

            _timeToLowerSpeed = Context.SlideModifierCurve.keys[Context.SlideModifierCurve.length - 1].time;

            Context.Stamina.IsRecovering = false;
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();

            _currentEvaluateTime = StateTimer <= _timeToLowerSpeed ? StateTimer : _timeToLowerSpeed;
            var slideMoveVector = _lastMoveVector * Context.SlideModifierCurve.Evaluate(_currentEvaluateTime);
            Context.PlayerMover.ChangeMove(MoveType.StateMove, slideMoveVector, RotationType.NoRotation);
        }

        public override void OnStateExit()
        {
            Context.Stamina.IsRecovering = true;
        }

        public override bool TrySwitchState(out MainCharacterState newMainCharacterState)
        {
            newMainCharacterState = null;
            
            if (Math.Abs(_currentEvaluateTime - _timeToLowerSpeed) == 0) newMainCharacterState = _factory.Crouch();
            else if (Context.InputProcessor.GetIsJumpPressedRecently()) newMainCharacterState = _factory.Jump();
            
            return newMainCharacterState != null;
        }

        #endregion
    }
}