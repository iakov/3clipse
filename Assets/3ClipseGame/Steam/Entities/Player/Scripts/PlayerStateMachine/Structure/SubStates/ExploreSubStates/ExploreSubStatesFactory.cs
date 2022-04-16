using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates.ExploreSubStates
{
    public class ExploreSubStatesFactory : SubStateFactory
    {
        public ExploreSubStatesFactory(PlayerStateMachine context) : base(context){}
        public SubState Walk() => new ExploreWalkSubState(_context, this);
        public SubState Idle() => new ExploreIdleSubState(_context, this);
    }
}
