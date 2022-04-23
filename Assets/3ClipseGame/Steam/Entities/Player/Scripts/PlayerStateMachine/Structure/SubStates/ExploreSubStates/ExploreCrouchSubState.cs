using _3ClipseGame.Steam.Entities.Player.Scripts.GlobalScripts;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.States;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates.ExploreSubStates
{
    public class ExploreCrouchSubState : SubState
    {
        #region Initialization

        public ExploreCrouchSubState(PlayerStateMachine context, SubStateFactory factory) : base(context, factory) =>
            _factory = (ExploreSubStatesFactory) factory;
        
        private ExploreSubStatesFactory _factory;

        #endregion

        #region MonoBehaviourMethods

        public override void OnStateEnter(){}

        public override void OnStateUpdate()
        {
            var rawInput = new Vector3(Context.InputHandler.CurrentInput.x, 0f, Context.InputHandler.CurrentInput.y);
            var moveVector = rawInput * Context.CrouchSpeedModifier;
            Context.PlayerMover.ChangeMove(MoveType.StateMove, moveVector, true);
        }

        public override void OnStateExit(){}

        public override bool TrySwitchState(out State newState)
        {
            newState = null;

            if (!Context.PlayerController.isGrounded && !Physics.Raycast(Context.Transform.position, Vector3.down,
                    Context.PlayerController.radius)) newState = _factory.Fall();
            else if (!Context.InputHandler.IsCrouchPressed) newState = _factory.Idle();
            return newState != null;
        }

        #endregion
    }
}