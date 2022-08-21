using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
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
            _lastMoveVector = Context.PlayerMover.GetLastMove(MoveType.StateMove, true);
            _lastMoveVector.y = 0f;
            var jumpMoveVector = _lastMoveVector + Vector3.up * Context.JumpStrength;
            Context.PlayerMover.ChangeMove(MoveType.StateMove, jumpMoveVector, RotationType.NoRotation);
            
            Context.Stamina.IsRecovering = false;
            Context.Stamina.AddValue(Context.JumpStaminaReduce);
        }

        public override void OnStateExit()
        {
            Context.InputHandler.IsJumpPressed = false;
            Context.Stamina.IsRecovering = true;
        }

        public override bool TrySwitchState(out MainCharacterState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (Context.PlayerController.IsGrounded && StateTimer > 0.1f) newMainCharacterState = _factory.Idle();
            else if(Context.PlayerMover.GetLastMove(MoveType.StateMove, false).y + Context.PlayerMover.GetLastMove(MoveType.GravityMove, false).y < 0) newMainCharacterState = _factory.Fall();

            return newMainCharacterState != null;
        }

        #endregion
    }
}
