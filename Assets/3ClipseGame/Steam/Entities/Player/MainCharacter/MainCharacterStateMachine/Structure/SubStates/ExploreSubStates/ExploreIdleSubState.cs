using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.SubStates.ExploreSubStates
{
    public class ExploreIdleSubState : MainCharacterSubState
    {
        #region Initialization

        public ExploreIdleSubState(MainCharacterStateMachine context, MainCharacterSubStateFactory factory) : base(context, factory) =>
            _factory = (ExploreSubStatesFactory) factory;

        private ExploreSubStatesFactory _factory;
        private Vector3 _lastMoveVector;
        private bool _isJumped;

        #endregion

        #region SubStateMethods

        public override void OnStateEnter()
        {
            _lastMoveVector = Context.PlayerMover.GetLastMove(MoveType.StateMove, true);
            _lastMoveVector.y = 0f;

            Context.InputHandler.JumpPressed += OnJumpPressed;
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();

            var t = StateTimer <= 1 ? StateTimer : 1f;
            var interpolatedMoveVector = Vector3.Lerp(_lastMoveVector, Vector3.zero, t * Context.SpeedInterpolation);
            Context.PlayerMover.ChangeMove(MoveType.StateMove, interpolatedMoveVector, RotationType.NoRotation);
        }

        public override void OnStateExit()
        {
            Context.InputHandler.JumpPressed -= OnJumpPressed;
        }

        public override bool TrySwitchState(out MainCharacterState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (_isJumped) newMainCharacterState = _factory.Jump();
            else if (!Context.PlayerController.IsGrounded) newMainCharacterState = _factory.Fall();
            else if (Context.InputHandler.GetIsCrouchPressed()) newMainCharacterState = _factory.Crouch();
            else if (Context.InputHandler.GetCurrentInput() != Vector2.zero) newMainCharacterState = _factory.Walk();

            return newMainCharacterState != null;
        }

        #endregion

        private void OnJumpPressed() => _isJumped = true;
    }
}