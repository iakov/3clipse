using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.States;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates.ExploreSubStates
{
    public class ExploreCrouchSubState : SubState
    {
        public ExploreCrouchSubState(PlayerStateMachine context, SubStateFactory factory) : base(context, factory){}
        private ExploreSubStatesFactory _factory;

        public override void OnStateEnter()
        {
            _factory = (ExploreSubStatesFactory) Factory;
        }

        public override void OnStateUpdate()
        {
            var rawInput = new Vector3(Context.InputHandler.CurrentInput.x, 0f, Context.InputHandler.CurrentInput.y);
            var moveVector = rawInput * Context.CrouchSpeedModifier;
            Context.PlayerMover.ChangeMove(MoveType.StateMove, moveVector);
        }

        public override void OnStateExit(){}

        public override bool TrySwitchState(out State newState)
        {
            newState = null;

            if (!Context.InputHandler.IsCrouchPressed) newState = _factory.Idle();
            return newState != null;
        }
    }
}