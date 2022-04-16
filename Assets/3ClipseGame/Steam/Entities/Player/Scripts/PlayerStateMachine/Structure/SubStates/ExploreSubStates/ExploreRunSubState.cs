using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.States;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates.ExploreSubStates
{
    public class ExploreRunSubState : SubState
    {
        public ExploreRunSubState(PlayerStateMachine context, SubStateFactory factory) : base(context, factory){}

        private ExploreSubStatesFactory _factory;
        private float _timeToMaximumSpeed;

        public override void OnStateEnter()
        {
            _factory = (ExploreSubStatesFactory) Factory;
            _timeToMaximumSpeed = Context.RunModifierCurve.keys[Context.WalkSpeedUpCurve.length - 1].time;
        }

        public override void OnStateUpdate()
        {
            AddTime(Time.deltaTime);
            var rawMoveVector = new Vector3(Context.InputHandler.CurrentInput.x, 0f, Context.InputHandler.CurrentInput.y);
            var currentEvaluateTime = StateTimer <= _timeToMaximumSpeed ? StateTimer : _timeToMaximumSpeed;
            var moveVector = rawMoveVector * Context.RunModifierCurve.Evaluate(currentEvaluateTime) * Context.WalkSpeed;
            Context.PlayerMover.ChangeMove(MoveType.StateMove, moveVector);
        }

        public override void OnStateExit()
        {
        }

        public override bool TrySwitchState(out State newState)
        {
            newState = null;
            
            if (Context.InputHandler.CurrentInput == Vector2.zero) newState = _factory.Idle();
            else if (!Context.InputHandler.IsRunPressed) newState = _factory.Walk();

            return newState != null;
        }
    }
}