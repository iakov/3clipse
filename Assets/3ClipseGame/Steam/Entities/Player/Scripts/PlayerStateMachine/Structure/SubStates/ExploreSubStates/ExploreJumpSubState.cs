using _3ClipseGame.Steam.Entities.Player.Scripts.GlobalScripts;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.States;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates.ExploreSubStates
{
    public class ExploreJumpSubState : SubState
    {
        public ExploreJumpSubState(PlayerStateMachine context, SubStateFactory factory) : base(context, factory) =>
            _factory = (ExploreSubStatesFactory) factory;

        private ExploreSubStatesFactory _factory;
        private Vector3 _lastMoveVector;

        public override void OnStateEnter()
        {
            _lastMoveVector = Context.PlayerMover.GetLastMove(MoveType.StateMove);
        }

        public override void OnStateUpdate()
        {
            var jumpMoveVector = _lastMoveVector + Vector3.up * Context.JumpStrength;
            Context.PlayerMover.ChangeMove(MoveType.StateMove, jumpMoveVector, true);
        }

        public override void OnStateExit()
        {
            Context.InputHandler.IsJumpPressed = false;
        }

        public override bool TrySwitchState(out State newState)
        {
            newState = null;

            if (Context.PlayerController.isGrounded) newState = _factory.Idle();
            else if(Context.PlayerMover.GetLastMove(MoveType.StateMove).y + Context.PlayerMover.GetLastMove(MoveType.GravityMove).y < 0) newState = _factory.Fall();

            if(newState != null) Debug.Log("Switching from jump");
            return newState != null;
        }
    }
}
