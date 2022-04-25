using _3ClipseGame.Steam.Entities.Player.Scripts.GlobalScripts;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.States;
using Assets._3ClipseGame.Steam.Entities.Player.Scripts.GlobalScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates.ExploreSubStates
{
    public class ExploreIdleSubState : SubState
    {
        #region Initialization

        public ExploreIdleSubState(PlayerStateMachine context, SubStateFactory factory) : base(context, factory) =>
            _factory = (ExploreSubStatesFactory) factory;

        private ExploreSubStatesFactory _factory;
        private Vector3 _lastMoveVector;

        #endregion

        #region MonoBehaviourMethods

        public override void OnStateEnter()
        {
            _lastMoveVector = Context.PlayerMover.GetLastMove(MoveType.StateMove);
        }

        public override void OnStateUpdate()
        {
            AddTime(Time.deltaTime);
            var t = StateTimer <= 1 ? StateTimer : 1f;
            var interpolatedMoveVector = Vector3.Lerp(_lastMoveVector, Vector3.zero, t * Context.SpeedInterpolation);
            Context.PlayerMover.ChangeMove(MoveType.StateMove, interpolatedMoveVector, RotationType.NoRotation);
        }

        public override void OnStateExit()
        {
        }

        public override bool TrySwitchState(out State newState)
        {
            newState = null;

            if (Context.InputHandler.IsJumpPressed) newState = _factory.Jump();
            else if (!Context.PlayerController.isGrounded && !Physics.Raycast(Context.Transform.position, Vector3.down,
                    Context.PlayerController.radius)) newState = _factory.Fall();
            else if (Context.InputHandler.IsCrouchPressed) newState = _factory.Crouch();
            else if (Context.InputHandler.CurrentInput != Vector2.zero) newState = _factory.Walk();

            return newState != null;
        }

        #endregion
    }
}