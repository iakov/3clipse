using _3ClipseGame.Steam.Entities.Player.Scripts.GlobalScripts;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.States;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates.ExploreSubStates
{
    public class ExploreRunSubState : SubState
    {
        #region Initialization

        public ExploreRunSubState(PlayerStateMachine context, SubStateFactory factory) : base(context, factory) =>
            _factory = (ExploreSubStatesFactory) factory;

        private ExploreSubStatesFactory _factory;
        private float _timeToMaximumSpeed;


        #endregion

        #region MonoBehaviourMethods

        public override void OnStateEnter()
        {
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

            if (Context.InputHandler.IsJumpPressed) newState = _factory.Jump();
            else if (!Context.PlayerController.isGrounded && !Physics.Raycast(Context.Transform.position, Vector3.down,
                    Context.PlayerController.radius)) newState = _factory.Fall();
            else if (Context.InputHandler.CurrentInput == Vector2.zero) newState = _factory.Stop();
            else if (Context.InputHandler.IsCrouchPressed) newState = _factory.Crouch();
            else if (!Context.InputHandler.IsRunPressed) newState = _factory.Walk();

            return newState != null;
        }

        #endregion
    }
}