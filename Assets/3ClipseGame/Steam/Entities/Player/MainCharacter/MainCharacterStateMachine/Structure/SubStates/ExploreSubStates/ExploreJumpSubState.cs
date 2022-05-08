using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.Scripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.SubStates.ExploreSubStates
{
    public class ExploreJumpSubState : MainCharacterSubState
    {
        #region Initialization

        public ExploreJumpSubState(MainCharacterStateMachine context, MainCharacterSubStateFactory factory) : base(context, factory) =>
            _factory = (ExploreSubStatesFactory) factory;

        private ExploreSubStatesFactory _factory;
        private Vector3 _lastMoveVector;

        #endregion

        #region SubStateMethods

        public override void OnStateEnter()
        {
            _lastMoveVector = Context.PlayerMover.GetLastMove(MoveType.StateMove);
        }

        public override void OnStateUpdate()
        {
            AddTime(Time.deltaTime);
            var jumpMoveVector = _lastMoveVector + Vector3.up * Context.JumpStrength;
            Context.PlayerMover.ChangeMove(MoveType.StateMove, jumpMoveVector, RotationType.RotateOnBeginning);
        }

        public override void OnStateExit()
        {
            Context.InputHandler.IsJumpPressed = false;
        }

        public override bool TrySwitchState(out MainCharacterState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (Context.MainCharacter.IsGrounded && StateTimer > 0.1f) newMainCharacterState = _factory.Idle();
            else if(Context.PlayerMover.GetLastMove(MoveType.StateMove).y + Context.PlayerMover.GetLastMove(MoveType.GravityMove).y < 0) newMainCharacterState = _factory.Fall();

            return newMainCharacterState != null;
        }

        #endregion
    }
}
