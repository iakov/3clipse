using _3ClipseGame.Steam.Entities.Player.Scripts.GlobalScripts;
using Assets._3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.States;
using UnityEngine;

namespace Assets._3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates.ExploreSubStates
{
    public class ExploreRunSubState : SubState
    {
        #region Initialization

        public ExploreRunSubState(PlayerStateMachine context, SubStateFactory factory) : base(context, factory){}

        private ExploreSubStatesFactory _factory;
        private float _timeToMaximumSpeed;


        #endregion

        #region MonoBehaviourMethods

        public override void OnStateEnter()
        {
            _factory = (ExploreSubStatesFactory) Factory;
            _timeToMaximumSpeed = Context.RunModifierCurve.keys[Context.RunModifierCurve.length - 1].time;
        }

        public override void OnStateUpdate()
        {
            AddTime(Time.deltaTime);
            var rawMoveVector = new Vector3(Context.InputHandler.CurrentInput.x, 0f, Context.InputHandler.CurrentInput.y);
            var currentEvaluateTime = StateTimer <= _timeToMaximumSpeed ? StateTimer : _timeToMaximumSpeed;
            var moveVector = rawMoveVector * (Context.RunModifierCurve.Evaluate(currentEvaluateTime) * Context.WalkSpeed);
            Context.PlayerMover.ChangeMove(MoveType.StateMove, moveVector, true);
        }

        public override void OnStateExit()
        {
        }

        public override bool TrySwitchState(out State newState)
        {
            newState = null;
            
            if (Context.InputHandler.CurrentInput == Vector2.zero) newState = _factory.Stop();
            else if (Context.InputHandler.IsCrouchPressed) newState = _factory.Crouch();
            else if (!Context.InputHandler.IsRunPressed) newState = _factory.Walk();

            return newState != null;
        }

        #endregion
    }
}