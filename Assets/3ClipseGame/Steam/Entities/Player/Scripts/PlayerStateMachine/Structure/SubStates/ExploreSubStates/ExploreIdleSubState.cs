using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.States;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates.ExploreSubStates
{
    public class ExploreIdleSubState : SubState
    {
        public ExploreIdleSubState(PlayerStateMachine context, SubStateFactory factory) : base(context, factory) =>
            _factory = (ExploreSubStatesFactory) factory;

        private ExploreSubStatesFactory _factory;

        public override void OnStateEnter() => Context.PlayerMover.ChangeMove(MoveType.StateMove, Vector3.zero);
        
        public override void OnStateUpdate() => AddTime(Time.deltaTime);

        public override void OnStateExit()
        {
        }

        public override bool TrySwitchState(out State newState)
        {
            newState = null;
            
            if (Context.InputHandler.CurrentInput != Vector2.zero) newState = _factory.Walk();

            return newState != null;
        }
    }
}