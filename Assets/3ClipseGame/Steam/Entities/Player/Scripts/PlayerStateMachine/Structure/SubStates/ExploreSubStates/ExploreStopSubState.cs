using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.States;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates.ExploreSubStates
{
    public class ExploreStopSubState : SubState
    {
        public ExploreStopSubState(PlayerStateMachine context, SubStateFactory factory) : base(context, factory){}

        private ExploreSubStatesFactory _factory;
        private Vector3 _lastMoveVector;

        public override void OnStateEnter()
        {
            _factory = (ExploreSubStatesFactory) Factory;
            _lastMoveVector = Context.PlayerMover.GetLastMove(MoveType.StateMove);
        }

        public override void OnStateUpdate()
        {
            AddTime(Time.deltaTime);
            var t = StateTimer <= 1 ? StateTimer : 1f;
            var interpolatedMoveVector = Vector3.Lerp(_lastMoveVector, Vector3.zero, t * Context.SpeedInterpolation);
            Context.PlayerMover.ChangeMove(MoveType.StateMove, interpolatedMoveVector);
        }

        public override void OnStateExit()
        {
        }

        public override bool TrySwitchState(out State newState)
        {
            newState = null;

            if (Context.PlayerMover.GetLastMove(MoveType.StateMove) == Vector3.zero) newState = _factory.Idle();
            else if (Context.InputHandler.CurrentInput != Vector2.zero) newState = _factory.Walk();

            return newState != null;
        }
    }
}