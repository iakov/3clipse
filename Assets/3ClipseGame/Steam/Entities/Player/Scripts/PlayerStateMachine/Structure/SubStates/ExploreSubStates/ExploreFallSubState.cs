using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.States;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates.ExploreSubStates
{
    public class ExploreFallSubState : SubState
    {
        public ExploreFallSubState(PlayerStateMachine context, SubStateFactory factory) : base(context, factory){}
        
        private ExploreSubStatesFactory _factory;
        private Vector3 _lastMove;
        
        public override void OnStateEnter()
        {
            _factory = (ExploreSubStatesFactory) Factory;
        }

        public override void OnStateUpdate()
        {
        }

        public override void OnStateExit(){}

        public override bool TrySwitchState(out State newState)
        {
            newState = null;

            if (Physics.Raycast(Context.Transform.position, Vector3.down, Context.PlayerController.radius * 2)) newState = _factory.Idle();

            return newState != null;
        }
    }
}